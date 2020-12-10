#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Spire.Core.Commands.Abstractions.Builders.Processing;
using Spire.Core.Commands.Abstractions.Processing;
using Spire.Core.Commands.Abstractions.Processing.Attributes;
using Spire.Core.Commands.Processing;
using Spire.Core.Extensions;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Commands.Builders.Processing
{
    /// <summary>
    /// Default implementation of <see cref="ICommandProcessorBuilder{TEntity,TCommandHandlerAttribute}"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TCommandHandlerAttribute">Command handler attribute.</typeparam>
    public class
        CommandProcessorBuilder<TEntity, TCommandHandlerAttribute> : ICommandProcessorBuilder<TEntity,
            TCommandHandlerAttribute>
        where TCommandHandlerAttribute : CommandHandlerAttributeBase
    {
        private readonly IContainer _container;
        private readonly ICollection<ICommandHandlerDescriptor<TCommandHandlerAttribute>> _handlerDescriptors;

        private readonly ICommandHandlerMatcher<TEntity, TCommandHandlerAttribute> _commandHandlerMatcher;

        private readonly string _processorId;
        private readonly UpdateType _entityType;

        /// <summary>
        /// Creates new <see cref="CommandProcessorBuilder{TEntity,TCommandHandlerAttribute}"/> with specified processor id, entity type, command handler matcher, and Autofac service resolving container.
        /// </summary>
        /// <param name="processorId">Processor id.</param>
        /// <param name="entityType">Entity type.</param>
        /// <param name="commandHandlerMatcher">Command handler matcher.</param>
        /// <param name="container">Autofac service resolving container</param>
        public CommandProcessorBuilder(string processorId, UpdateType entityType,
            ICommandHandlerMatcher<TEntity, TCommandHandlerAttribute> commandHandlerMatcher,
            IContainer container)
        {
            _processorId = processorId;
            _entityType = entityType;
            _commandHandlerMatcher = commandHandlerMatcher;
            _container = container;
            _handlerDescriptors = new Collection<ICommandHandlerDescriptor<TCommandHandlerAttribute>>();
        }

        /// <summary>
        /// Build command processor.
        /// </summary>
        /// <returns>Build command processor.</returns>
        public ICommandProcessor<TEntity, TCommandHandlerAttribute> Build()
        {
            IEnumerable<IActivatedCommandHandlerDescriptor<TCommandHandlerAttribute>>
                activatedCommandHandlerDescriptors = _handlerDescriptors
                    .ActivateDescriptors<ICommandHandlerDescriptor<TCommandHandlerAttribute>,
                        IActivatedCommandHandlerDescriptor<TCommandHandlerAttribute>, TCommandHandlerAttribute>
                    (_container, (descriptor, typeInstance) =>
                        new ActivatedCommandHandlerDescriptor<TCommandHandlerAttribute>(descriptor.Id,
                            descriptor.Method,
                            typeInstance,
                            descriptor.DeclaringType,
                            descriptor.Attribute
                        ));

            return new CommandProcessor<TEntity, TCommandHandlerAttribute>(
                _processorId,
                _entityType,
                _commandHandlerMatcher,
                activatedCommandHandlerDescriptors);
        }

        /// <summary>
        /// Adds new command handler descriptor.
        /// </summary>
        /// <param name="commandHandlerDescriptor">Command handler descriptor</param>
        /// <returns>Configured command processor builder instance.</returns>
        public ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute> WithCommandDescriptor(
            ICommandHandlerDescriptor<TCommandHandlerAttribute> commandHandlerDescriptor)
        {
            _handlerDescriptors.Add(commandHandlerDescriptor ??
                                    throw new ArgumentNullException(nameof(commandHandlerDescriptor)));

            return this;
        }

        /// <summary>
        /// Adds command handlers from specified type.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <returns>Configured command processor builder instance.</returns>
        public ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute> WithCommandHandlersFromType(Type type)
        {
            MethodInfo[] methodInfos = type.GetMethods();

            foreach (MethodInfo method in methodInfos)
            {
                if (method
                    .TryCreateDescriptor<ICommandHandlerDescriptor<TCommandHandlerAttribute>, TCommandHandlerAttribute>(
                        new[] {typeof(void), typeof(Task), typeof(ValueTask)},
                        attribute => attribute.Id.Equals(_processorId) && attribute.EntityType.Equals(_entityType),
                        (method, attribute)
                            => new CommandHandlerDescriptor<TCommandHandlerAttribute>(Guid.NewGuid(), method,
                                method.DeclaringType,
                                attribute),
                        out ICommandHandlerDescriptor<TCommandHandlerAttribute> descriptor))
                {
                    _handlerDescriptors.Add(descriptor);
                }
            }

            return this;
        }

        /// <summary>
        /// Adds command handlers from specified assembly.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <returns>Configured command processor builder instance.</returns>
        public ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute> WithCommandHandlersFromAssembly(
            Assembly assembly = null)
        {
            Type[] exportedTypes = (assembly ?? Assembly.GetExecutingAssembly()).GetExportedTypes();

            foreach (Type exportedType in exportedTypes)
            {
                WithCommandHandlersFromType(exportedType);
            }

            return this;
        }
    }
}