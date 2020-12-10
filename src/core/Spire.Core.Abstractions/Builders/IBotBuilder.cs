#region

using Autofac;
using Spire.Core.Abstractions.Processing;
using Spire.Core.Abstractions.Processing.Attributes;
using Telegram.Bot;

#endregion

namespace Spire.Core.Abstractions.Builders
{
    /// <summary>
    /// Base interface for implementing bot builder.
    /// </summary>
    public interface IBotBuilder : IBuilder<IBot>
    {
        /// <summary>
        /// Autofac service resolving container.
        /// </summary>
        IContainer ServiceContainer { get; }

        /// <summary>
        /// Telegram bot client.
        /// </summary>
        ITelegramBotClient BotClient { get; }

        /// <summary>
        /// Information about the bot.
        /// </summary>
        BotDescriptor Descriptor { get; }

        /// <summary>
        /// Adds new update entity processor to bot.
        /// </summary>
        /// <param name="updateEntityProcessor">Update entity processor.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TUpdateEntityHandlerAttribute">Update entity handler attribute.</typeparam>
        /// <returns>Configured bot builder instance.</returns>
        IBotBuilder WithUpdateEntityProcessor<TEntity, TUpdateEntityHandlerAttribute>(
            IUpdateEntityProcessor<TEntity, TUpdateEntityHandlerAttribute> updateEntityProcessor)
            where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase;
    }
}