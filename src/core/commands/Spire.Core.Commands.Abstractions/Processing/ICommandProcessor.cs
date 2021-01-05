#region

using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Spire.Core.Abstractions;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Abstractions.Processing.Attributes;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Commands.Abstractions.Processing
{
    /// <summary>
    /// Base interface for implementation command processor.
    /// </summary>
    public interface ICommandProcessor : IIdentifiable<string>
    {
        /// <summary>
        /// Entity type.
        /// </summary>
        UpdateType EntityType { get; }
    }

    /// <summary>
    /// Base interface for implementation command processor.
    /// </summary>
    public interface ICommandProcessor<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher> : ICommandProcessor
        where TCommandHandlerAttribute : CommandHandlerAttributeBase
    where TCommandHandlerMatcher : class, ICommandHandlerMatcher<TEntity, TCommandHandlerAttribute>
    {
        /// <summary>
        /// Collection of activated command descriptors.
        /// </summary>
        IEnumerable<IActivatedCommandHandlerDescriptor<TCommandHandlerAttribute>> Descriptors { get; }

        /// <summary>
        /// Provides command matching.
        /// </summary>
        TCommandHandlerMatcher Matcher { get; }

        /// <summary>
        /// Trying to process command.
        /// </summary>
        /// <param name="commandContext">Command context.</param>
        /// <param name="resolvingContainer">Autofac service resolving container.</param>
        /// <returns>Indicates, was any command processed or not.</returns>
        ValueTask<bool> Process(
            IHandlerContext<TEntity> commandContext,
            IContainer resolvingContainer);
    }
}