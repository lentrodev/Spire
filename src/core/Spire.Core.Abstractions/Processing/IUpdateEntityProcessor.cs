#region

using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Spire.Core.Abstractions.Processing.Attributes;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Abstractions.Processing.Results;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Abstractions.Processing
{
    /// <summary>
    /// Base interface for implementing update entity processor.
    /// </summary>
    public interface IUpdateEntityProcessor : IIdentifiable<string>
    {
        /// <summary>
        /// Update entity type.
        /// </summary>
        UpdateType EntityType { get; }
    }

    /// <summary>
    /// Base interface for implementing update entity processor.
    /// </summary>
    /// <typeparam name="TEntity">Update entity type. E.g <see cref="Message"/>, <see cref="CallbackQuery"/>>, etc.</typeparam>
    /// <typeparam name="TUpdateEntityHandlerAttribute">Update entity handler attribute.</typeparam>
    public interface IUpdateEntityProcessor<TEntity, TUpdateEntityHandlerAttribute> : IUpdateEntityProcessor
        where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
    {
        /// <summary>
        /// Update entity handlers descriptors.
        /// </summary>
        IEnumerable<IActivatedUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>> Descriptors { get; }

        /// <summary>
        /// Asynchronously processes update entity.
        /// </summary>
        /// <param name="handlerContext">Handler context.</param>
        /// <param name="resolvingContainer">Autofac service resolving container.</param>
        /// <returns>Update entity processing result.</returns>
        ValueTask<IUpdateEntityProcessingResult<TEntity>> Process(
            IHandlerContext<TEntity> handlerContext,
            IContainer resolvingContainer);
    }
}