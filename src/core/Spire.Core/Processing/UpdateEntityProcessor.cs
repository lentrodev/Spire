#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using Spire.Core.Abstractions.Processing;
using Spire.Core.Abstractions.Processing.Attributes;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Abstractions.Processing.Results;
using Spire.Core.Extensions;
using Spire.Core.Processing.Results;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Processing
{
    /// <summary>
    /// Default implementation of <see cref="IUpdateEntityProcessor{TEntity,TUpdateEntityHandlerAttribute}"/>
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TUpdateEntityHandlerAttribute">Update entity handler attribute.</typeparam>
    public class UpdateEntityProcessor<TEntity, TUpdateEntityHandlerAttribute> : Identifiable<string>,
        IUpdateEntityProcessor<TEntity, TUpdateEntityHandlerAttribute>
        where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
    {
        /// <summary>
        /// Entity type.
        /// </summary>
        public UpdateType EntityType { get; }

        /// <summary>
        /// Update entity handlers descriptors.
        /// </summary>
        public IEnumerable<IActivatedUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>> Descriptors { get; }

        /// <summary>
        /// Creates new <see cref="UpdateEntityProcessor{TEntity,TUpdateEntityHandlerAttribute}"/> with specified id, entity type, and activated entity handlers descriptors collection.
        /// </summary>
        /// <param name="id">Processor identifier.</param>
        /// <param name="entityType">Entity type.</param>
        /// <param name="activatedEntityHandlerDescriptors">Update entity handlers descriptors.</param>
        public UpdateEntityProcessor(string id, UpdateType entityType,
            IEnumerable<IActivatedUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>>
                activatedEntityHandlerDescriptors) : base(id)
        {
            EntityType = entityType;

            Descriptors = activatedEntityHandlerDescriptors;
        }

        /// <summary>
        /// Asynchronously processes update entity.
        /// </summary>
        /// <param name="handlerContext">Handler context.</param>
        /// <param name="resolvingContainer">Autofac service resolving container.</param>
        /// <returns>Update entity processing result.</returns>
        public ValueTask<IUpdateEntityProcessingResult<TEntity>> Process(IHandlerContext<TEntity> handlerContext,
            IContainer resolvingContainer)
        {
            return new ValueTask<IUpdateEntityProcessingResult<TEntity>>(Task.Factory.StartNew(() =>
            {
                bool continueProcessing = true;

                Stopwatch processingTimeWatcher = Stopwatch.StartNew();

                foreach (var descriptor in Descriptors)
                {
                    if (continueProcessing)
                        continueProcessing = descriptor.InvokeHandlerSafe(resolvingContainer,
                                new[] {typeof(bool), typeof(Task<bool>), typeof(ValueTask<bool>)}, handlerContext)
                            .GetAwaiter().GetResult();
                }

                processingTimeWatcher.Stop();

                return new UpdateEntityProcessingResult<TEntity>(Guid.NewGuid(), handlerContext,
                    processingTimeWatcher.Elapsed) as IUpdateEntityProcessingResult<TEntity>;
            }));
        }
    }
}