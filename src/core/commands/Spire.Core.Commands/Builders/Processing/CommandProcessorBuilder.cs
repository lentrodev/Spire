#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Default implementation of <see cref="ICommandProcessorBuilder{TEntity,TCommandHandlerAttribute,TCommandHandlerMatcher}"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TCommandHandlerAttribute">Command handler attribute.</typeparam>
    /// <typeparam name="TCommandHandlerMatcher">Command handler matcher type.</typeparam>
    public class
        CommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher> : ICommandProcessorBuilder<
            TEntity,
            TCommandHandlerAttribute, TCommandHandlerMatcher>
        where TCommandHandlerAttribute : CommandHandlerAttributeBase
        where TCommandHandlerMatcher : class, ICommandHandlerMatcher<TEntity, TCommandHandlerAttribute>
    {
        private readonly IContainer _container;
        private readonly ICollection<ICommandHandlerDescriptor<TCommandHandlerAttribute>> _handlerDescriptors;

        private readonly Func<
            IEnumerable<IActivatedCommandHandlerDescriptor<TCommandHandlerAttribute>>,
            TCommandHandlerMatcher> _commandHandlerMatcherResolver;

        private readonly string _processorId;
        private readonly UpdateType _entityType;

        public CommandProcessorBuilder(string processorId, UpdateType entityType,
            TCommandHandlerMatcher commandHandlerMatcher,
            IContainer container) : this(processorId, entityType, _ => commandHandlerMatcher, container)
        {
        }

        /// <summary>
        /// Creates new <see cref="CommandProcessorBuilder{TEntity,TCommandHandlerAttribute,TCommandHandlerMatcher}"/> with specified processor id, entity type, command handler matcher, and Autofac service resolving container.
        /// </summary>
        /// <param name="processorId">Processor id.</param>
        /// <param name="entityType">Entity type.</param>
        /// <param name="commandHandlerMatcherResolver">Command handler matcher resolver.</param>
        /// <param name="container">Autofac service resolving container</param>
        public CommandProcessorBuilder(string processorId, UpdateType entityType,
            Func<IEnumerable<IActivatedCommandHandlerDescriptor<TCommandHandlerAttribute>>,
                TCommandHandlerMatcher> commandHandlerMatcherResolver,
            IContainer container)
        {
            if (string.IsNullOrEmpty(processorId) || string.IsNullOrWhiteSpace(processorId))
            {
                throw new ArgumentNullException(nameof(processorId));
            }

            _processorId = processorId;

            _entityType = entityType;

            _commandHandlerMatcherResolver = commandHandlerMatcherResolver;
            _container = container;
            _handlerDescriptors = new Collection<ICommandHandlerDescriptor<TCommandHandlerAttribute>>();
        }

        public CommandProcessorBuilder(
            string processorId,
            UpdateType entityType,
            IContainer container
        ) : this(processorId, entityType,
            _ => typeof(TCommandHandlerMatcher).TryActivateType(container, out object activatedInstance, _)
                ? (TCommandHandlerMatcher) activatedInstance
                : throw new InvalidOperationException(
                    $"Cannot activate command handler matcher {typeof(TCommandHandlerMatcher)}"), container)
        {
        }

        /// <summary>
        /// Build command processor.
        /// </summary>
        /// <returns>Build command processor.</returns>
        public ICommandProcessor<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher> Build()
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
                        )).ToList();

            return new CommandProcessor<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher>(
                _processorId,
                _entityType,
                _commandHandlerMatcherResolver(activatedCommandHandlerDescriptors),
                activatedCommandHandlerDescriptors);
        }

        /// <summary>
        /// Adds new command handler descriptor.
        /// </summary>
        /// <param name="commandHandlerDescriptor">Command handler descriptor</param>
        /// <returns>Configured command processor builder instance.</returns>
        public ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher> WithCommandDescriptor(
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
        public ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher> WithCommandHandlersFromType(Type type)
        {
            MethodInfo[] methodInfos = type.GetMethods();

            foreach (MethodInfo method in methodInfos)
            {
                if (method
                    .TryCreateDescriptor<ICommandHandlerDescriptor<TCommandHandlerAttribute>, TCommandHandlerAttribute>(
                        new[] {typeof(void), typeof(Task), typeof(ValueTask)},
                        attribute => attribute.Id.Equals(_processorId) && attribute.EntityType == _entityType,
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
        public ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher> WithCommandHandlersFromAssembly(
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