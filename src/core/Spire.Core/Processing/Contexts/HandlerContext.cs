#region

using System;
using Autofac;
using Spire.Core.Abstractions.Processing.Contexts;
using Telegram.Bot;
using Telegram.Bot.Types;

#endregion

namespace Spire.Core.Processing.Contexts
{
    /// <summary>
    /// Default implementation of <see cref="IHandlerContext{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class HandlerContext<TEntity> : Identifiable<Guid>, IHandlerContext<TEntity>
    {
        /// <summary>
        /// Update.
        /// </summary>
        public Update Update { get; }

        /// <summary>
        /// Update value entity. E.g. Message, CallbackQuery, InlineQuery etc. 
        /// </summary>
        public TEntity Entity { get; }

        /// <summary>
        /// Update sender. Can be null, if update doesn't contains sender (e.g update with <see cref="Poll"/>>).
        /// </summary>
        public User Sender { get; }

        /// <summary>
        /// Telegram bot client.
        /// </summary>
        public ITelegramBotClient BotClient { get; }

        /// <summary>
        /// Autofac service resolving container.
        /// </summary>
        public IContainer Container { get; }

        /// <summary>
        /// Creates new <see cref="HandlerContext{TEntity}"/> with specified update, update entity, update sender, telegram bot client, and resolving container.
        /// </summary>
        /// <param name="update">Update.</param>
        /// <param name="entity">Update value entity. E.g. Message, CallbackQuery, InlineQuery etc. </param>
        /// <param name="sender">Update sender. Can be null, if update doesn't contains sender (e.g update with <see cref="Poll"/>>).</param>
        /// <param name="botClient">Telegram bot client.</param>
        /// <param name="container">Autofac service resolving container.</param>
        public HandlerContext(Update update, TEntity entity, User sender, ITelegramBotClient botClient,
            IContainer container) : base(Guid.NewGuid())
        {
            Update = update;
            Entity = entity;
            Sender = sender;
            BotClient = botClient;
            Container = container;
        }
    }
}