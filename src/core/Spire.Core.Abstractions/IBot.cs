#region

using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Spire.Core.Abstractions.Processing;
using Spire.Core.Abstractions.Processing.Results;
using Telegram.Bot;
using Telegram.Bot.Types;

#endregion

namespace Spire.Core.Abstractions
{
    /// <summary>
    /// Base interface for implementing Telegram Bot.
    /// </summary>
    public interface IBot
    {
        /// <summary>
        /// Common information about the bot.
        /// </summary>
        BotDescriptor Descriptor { get; }

        /// <summary>
        /// Configured services, built by <see cref="ContainerBuilder.Build"/> represented as <see cref="Autofac"/> <see cref="IContainer"/>.
        /// </summary>
        IContainer ServiceContainer { get; }

        /// <summary>
        /// Telegram bot client.
        /// </summary>
        ITelegramBotClient BotClient { get; }

        /// <summary>
        /// Collection of update entity processors.
        /// </summary>
        IEnumerable<IUpdateEntityProcessor> UpdateEntityProcessors { get; }

        /// <summary>
        /// Asynchronously processes an <see cref="Update"/>.
        /// </summary>
        /// <param name="update"><see cref="Update"/> to process.</param>
        /// <returns><see cref="Update"/> processing result.</returns>
        ValueTask<IUpdateEntityProcessingResult> Process(Update update);
    }
}