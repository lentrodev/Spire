#region

using System;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Abstractions.Processing.Results;
using Telegram.Bot.Types;

#endregion

namespace Spire.Core.Processing.Results
{
    /// <summary>
    /// Default implementation for <see cref=""/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class UpdateEntityProcessingResult<TEntity> : Identifiable<Guid>, IUpdateEntityProcessingResult<TEntity>
    {
        /// <summary>
        /// Update.
        /// </summary>
        public Update Update { get; }

        /// <summary>
        /// Handler context.
        /// </summary>
        public IHandlerContext<TEntity> Context { get; }

        /// <summary>
        /// Entity processing time.
        /// </summary>
        public TimeSpan Time { get; }

        /// <summary>
        /// Creates new <see cref="UpdateEntityProcessingResult{TEntity}"/> with specified id, handler context and processing time.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="context">Handler context.</param>
        /// <param name="processingTime">Entity processing time.</param>
        public UpdateEntityProcessingResult(Guid id, IHandlerContext<TEntity> context, TimeSpan processingTime) :
            base(id)
        {
            Context = context;
            Update = context.Update;
            Time = processingTime;
        }
    }
}