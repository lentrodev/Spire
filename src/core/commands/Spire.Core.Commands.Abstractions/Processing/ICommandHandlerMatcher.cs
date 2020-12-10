#region

using System.Threading.Tasks;
using Autofac;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Abstractions.Processing.Attributes;
using Spire.Core.Commands.Abstractions.Processing.Contexts;

#endregion

namespace Spire.Core.Commands.Abstractions.Processing
{
    /// <summary>
    /// Base interface for implementing command handler matching result.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TCommandHandlerAttribute">Command handler attribute.</typeparam>
    public interface ICommandHandlerMatcher<TEntity, TCommandHandlerAttribute>
        where TCommandHandlerAttribute : CommandHandlerAttributeBase
    {
        /// <summary>
        /// Matches, could be command handled or not.
        /// </summary>
        /// <param name="handlerContext">Handler context.</param>
        /// <param name="commandHandlerDescriptor">Command handler descriptor.</param>
        /// <param name="serviceContainer">Autofac service resolving container.</param>
        /// <returns></returns>
        ValueTask<ICommandHandlerMatchingResult<TEntity>> CanHandle(IHandlerContext<TEntity> handlerContext,
            ICommandHandlerDescriptor<TCommandHandlerAttribute> commandHandlerDescriptor, IContainer serviceContainer);
    }

    /// <summary>
    /// Base interface for implementing command handler matching result.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface ICommandHandlerMatchingResult<TEntity>
    {
        /// <summary>
        /// Matching status.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Built command context.
        /// </summary>
        ICommandContext<TEntity> CommandContext { get; }
    }
}