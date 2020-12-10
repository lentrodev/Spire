#region

using System;
using Autofac;
using Telegram.Bot;
using Telegram.Bot.Types;

#endregion

namespace Spire.Core.Abstractions.Processing.Contexts
{
    /// <summary>
    /// Context for update entity handler.
    /// </summary>
    /// <typeparam name="TEntity"><inheritdoc cref="Entity"/>> type.</typeparam>
    public interface IHandlerContext<out TEntity> : IIdentifiable<Guid>
    {
        /// <summary>
        /// Update.
        /// </summary>
        Update Update { get; }

        /// <summary>
        /// Update value entity. E.g. Message, CallbackQuery, InlineQuery etc. 
        /// </summary>
        TEntity Entity { get; }

        /// <summary>
        /// Update sender. Can be null, if update doesn't contains sender (e.g update with <see cref="Poll"/>>).
        /// </summary>
        User Sender { get; }

        /// <summary>
        /// Telegram bot client.
        /// </summary>
        ITelegramBotClient BotClient { get; }

        /// <summary>
        /// Autofac service resolving container.
        /// </summary>
        IContainer Container { get; }
    }
}