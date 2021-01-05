#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Abstractions.Processing;
using Spire.Core.Commands.Abstractions.Processing.Attributes;
using Spire.Core.Extensions;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Commands.Processing
{
    /// <summary>
    /// Default implementation of <see cref="ICommandProcessor{TEntity,TCommandHandlerAttribute,TCommandHandlerMatcher}"/>
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TCommandHandlerAttribute">Command handler attribute.</typeparam>
    /// <typeparam name="TCommandHandlerMatcher">Command handler matcher type.</typeparam>
    public class CommandProcessor<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher> : Identifiable<string>,
        ICommandProcessor<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher>
        where TCommandHandlerAttribute : CommandHandlerAttributeBase
    where TCommandHandlerMatcher : class, ICommandHandlerMatcher<TEntity, TCommandHandlerAttribute>
    {
        /// <summary>
        /// Entity type.
        /// </summary>
        public UpdateType EntityType { get; }

        /// <summary>
        /// Provides command matching.
        /// </summary>
        public TCommandHandlerMatcher Matcher { get; }

        /// <summary>
        /// Collection of activated command descriptors.
        /// </summary>
        public IEnumerable<IActivatedCommandHandlerDescriptor<TCommandHandlerAttribute>> Descriptors { get; }

        /// <summary>
        /// Creates new <see cref="CommandProcessor{TEntity,TCommandHandlerAttribute,TCommandHandlerMatcher}"/> with specified id, entity type, command handler matcher and command descriptors.
        /// </summary>
        /// <param name="id">Command processor id.</param>
        /// <param name="entityType">Entity type.</param>
        /// <param name="matcher">Command handler matcher.</param>
        /// <param name="descriptors">Collection of command handler descriptors.</param>
        public CommandProcessor(string id, UpdateType entityType,
            TCommandHandlerMatcher matcher,
            IEnumerable<IActivatedCommandHandlerDescriptor<TCommandHandlerAttribute>> descriptors) : base(id)
        {
            EntityType = entityType;
            Matcher = matcher;
            Descriptors = descriptors;
        }

        /// <summary>
        /// Trying to process command.
        /// </summary>
        /// <param name="commandContext">Command context.</param>
        /// <param name="resolvingContainer">Autofac service resolving container.</param>
        /// <returns>Indicates, was any command processed or not.</returns>
        public async ValueTask<bool> Process(IHandlerContext<TEntity> commandContext, IContainer resolvingContainer)
        {
            foreach (var descriptor in Descriptors)
            {
                ICommandHandlerMatchingResult<TEntity> result = await Matcher.CanHandle(commandContext, descriptor,
                    resolvingContainer);

                if (result?.IsSuccess ?? false)
                {
                    await descriptor.InvokeHandlerSafe(resolvingContainer, Enumerable.Empty<Type>(),
                        result.CommandContext);
                    return true;
                }
            }

            return false;
        }
    }
}