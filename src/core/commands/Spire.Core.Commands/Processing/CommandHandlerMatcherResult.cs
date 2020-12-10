#region

using Spire.Core.Commands.Abstractions.Processing;
using Spire.Core.Commands.Abstractions.Processing.Contexts;

#endregion

namespace Spire.Core.Commands.Processing
{
    /// <summary>
    /// Default implementation of <see cref="ICommandHandlerMatchingResult{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class CommandHandlerMatchingResult<TEntity> : ICommandHandlerMatchingResult<TEntity>
    {
        /// <summary>
        /// Matching status.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Built command context.
        /// </summary>
        public ICommandContext<TEntity> CommandContext { get; }

        private CommandHandlerMatchingResult(ICommandContext<TEntity> commandContext)
        {
            CommandContext = commandContext;
            IsSuccess = commandContext != null;
        }

        /// <summary>
        /// Creates new <see cref="ICommandHandlerMatchingResult{TEntity}"/> with success result.
        /// </summary>
        /// <param name="commandContext">Successfully built command context.</param>
        /// <returns>Command handler matching result.</returns>
        public static ICommandHandlerMatchingResult<TEntity> Success(ICommandContext<TEntity> commandContext) =>
            new CommandHandlerMatchingResult<TEntity>(commandContext);

        /// <summary>
        /// Creates new <see cref="ICommandHandlerMatchingResult{TEntity}"/> with failed result.
        /// </summary>
        /// <returns>Command handler matching result.</returns>
        public static ICommandHandlerMatchingResult<TEntity> Fail() => new CommandHandlerMatchingResult<TEntity>(null);
    }
}