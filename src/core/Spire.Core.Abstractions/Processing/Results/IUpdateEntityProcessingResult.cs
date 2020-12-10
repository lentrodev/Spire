#region

using System;
using Spire.Core.Abstractions.Processing.Contexts;
using Telegram.Bot.Types;

#endregion

namespace Spire.Core.Abstractions.Processing.Results
{
    /// <summary>
    /// Base interface for implementing update entity processing result.
    /// </summary>
    public interface IUpdateEntityProcessingResult : IIdentifiable<Guid>
    {
        /// <summary>
        /// Update.
        /// </summary>
        Update Update { get; }

        /// <summary>
        /// Entity processing time.
        /// </summary>
        TimeSpan Time { get; }
    }

    /// <summary>
    /// Base interface for implementing update entity processing result.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface IUpdateEntityProcessingResult<out TEntity> : IUpdateEntityProcessingResult
    {
        /// <summary>
        /// Handler context.
        /// </summary>
        IHandlerContext<TEntity> Context { get; }
    }
}