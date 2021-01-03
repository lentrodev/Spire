#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Spire.Core.Abstractions.Builders.Processing;
using Spire.Core.Abstractions.Processing;
using Spire.Core.Abstractions.Processing.Attributes;
using Spire.Core.Extensions;
using Spire.Core.Processing;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Builders.Processing
{
    /// <summary>
    /// Default implementation of <see cref="IUpdateEntityProcessorBuilder{TEntity,TUpdateEntityHandlerAttribute}"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TUpdateEntityHandlerAttribute">Update entity handler attribute.</typeparam>
    public class
        UpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> : IUpdateEntityProcessorBuilder<TEntity,
            TUpdateEntityHandlerAttribute>
        where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
    {
        /// <summary>
        /// Autofac service resolving container.
        /// </summary>
        public IContainer Container { get; }

        /// <summary>
        /// Processor identifier.
        /// </summary>
        public string ProcessorId { get; }

        /// <summary>
        /// Entity type.
        /// </summary>
        public UpdateType EntityType { get; }

        private readonly IEntityHandlersOrchestrator<TUpdateEntityHandlerAttribute> _entityHandlerOrchestrator;
        private readonly ICollection<IUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>> _handlerDescriptors;

        /// <summary>
        /// Creates new <see cref="UpdateEntityProcessorBuilder{TEntity,TUpdateEntityHandlerAttribute}"/> with specified processor id, entity type, service resolving container and update entity handlers descriptors orchestrator.
        /// </summary>
        /// <param name="processorId">Processor identifier.</param>
        /// <param name="entityType">Entity type.</param>
        /// <param name="container">Autofac service resolving container.</param>
        /// <param name="entityHandlerOrchestrator">Update entity handler descriptors orchestrator.</param>
        public UpdateEntityProcessorBuilder(string processorId, UpdateType entityType, IContainer container,
            IEntityHandlersOrchestrator<TUpdateEntityHandlerAttribute> entityHandlerOrchestrator)
        {
            ProcessorId = processorId;
            EntityType = entityType;
            Container = container;
            _entityHandlerOrchestrator = entityHandlerOrchestrator;
            _handlerDescriptors = new Collection<IUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>>();
        }

        /// <summary>
        /// Adds update entity handler descriptor. 
        /// </summary>
        /// <param name="entityHandlerDescriptor">Update entity handler descriptor.</param>
        /// <returns>Configured update entity processor builder instance.</returns>
        public IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> WithHandlerDescriptor(
            IUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute> entityHandlerDescriptor)
        {
            _handlerDescriptors.Add(entityHandlerDescriptor ??
                                    throw new ArgumentNullException(nameof(entityHandlerDescriptor)));

            return this;
        }

        /// <summary>
        /// Registers all update entity handlers from specified type.
        /// </summary>
        /// <param name="type">Type to register update entity handlers from.</param>
        /// <returns>Configured update entity processor builder instance.</returns>
        public IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute>
            WithUpdateEntityHandlersFromType(Type type)
        {
            MethodInfo[] methodInfos = type.GetMethods();

            foreach (MethodInfo method in methodInfos)
            {
                if (method
                    .TryCreateDescriptor<IUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>,
                        TUpdateEntityHandlerAttribute>(
                        new[] {typeof(bool), typeof(Task<bool>), typeof(ValueTask<bool>)},
                        attribute => attribute.Id.Equals(ProcessorId) && attribute.EntityType == EntityType,
                        (descriptorMethod, attribute)
                            => new UpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>(Guid.NewGuid(),
                                descriptorMethod, method.DeclaringType, attribute),
                        out IUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute> descriptor))
                {
                    WithHandlerDescriptor(descriptor);
                }
            }

            return this;
        }

        /// <summary>
        /// Registers all update entity handlers from types, defined in specified assembly. If no assembly was specified, <see cref="Assembly.GetExecutingAssembly()"/> will be used. 
        /// </summary>
        /// <param name="assembly">Assembly to register update entity handlers from.</param>
        /// <returns>Configured update entity processor builder instance.</returns>
        public IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute>
            WithUpdateEntityHandlersFromAssembly(Assembly assembly = null)
        {
            Assembly specifiedAssembly = assembly ?? Assembly.GetExecutingAssembly();

            Type[] exportedTypes = specifiedAssembly.GetExportedTypes();

            foreach (Type exportedType in exportedTypes)
            {
                WithUpdateEntityHandlersFromType(exportedType);
            }

            return this;
        }

        public IUpdateEntityProcessor<TEntity, TUpdateEntityHandlerAttribute> Build()
        {
            IEnumerable<IActivatedUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>>
                activatedEntityHandlerDescriptors = _handlerDescriptors.ActivateDescriptors<IUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>, IActivatedUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>, TUpdateEntityHandlerAttribute>(Container, (descriptor, typeInstance) =>
                    new ActivatedUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>(descriptor.Id,
                        descriptor.Method,
                        typeInstance,
                        descriptor.DeclaringType,
                        descriptor.Attribute
                    ));

            IEnumerable<IActivatedUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>>
                activatedAndOrchestratedEntityHandlerDescriptors =
                    _entityHandlerOrchestrator.Orchestrate(activatedEntityHandlerDescriptors).ToList();

            return new UpdateEntityProcessor<TEntity, TUpdateEntityHandlerAttribute>(ProcessorId, EntityType,
                activatedAndOrchestratedEntityHandlerDescriptors);
        }
    }
}