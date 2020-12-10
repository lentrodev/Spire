#region

using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Extensions
{
    /// <summary>
    /// Update extensions.
    /// </summary>
    public static class UpdateExtensions
    {
        /// <summary>
        /// Gets update value (entity). 
        /// </summary>
        /// <param name="update">Update.</param>
        /// <returns>Update value (entity).</returns>
        public static object GetUpdateEntity(this Update update) => update.Type switch
        {
            UpdateType.Message => update.Message,
            UpdateType.EditedMessage => update.EditedMessage,

            UpdateType.ChannelPost => update.ChannelPost,
            UpdateType.EditedChannelPost => update.EditedChannelPost,

            UpdateType.CallbackQuery => update.CallbackQuery,

            UpdateType.InlineQuery => update.InlineQuery,
            UpdateType.ChosenInlineResult => update.ChosenInlineResult,

            UpdateType.Poll => update.Poll,
            UpdateType.PollAnswer => update.PollAnswer,

            UpdateType.PreCheckoutQuery => update.PreCheckoutQuery,
            UpdateType.ShippingQuery => update.ShippingQuery,

            UpdateType.Unknown => default,
            _ => default
        };

        /// <summary>
        /// Gets update sender <see cref="User"/>.
        /// </summary>
        /// <param name="update">Update.</param>
        /// <returns>Update sender.</returns>
        public static User GetUpdateEntitySender(this Update update) => update.Type switch
        {
            UpdateType.Message => update.Message.From,
            UpdateType.EditedMessage => update.EditedMessage.From,

            UpdateType.ChannelPost => update.ChannelPost.From,
            UpdateType.EditedChannelPost => update.EditedChannelPost.From,

            UpdateType.CallbackQuery => update.CallbackQuery.From,

            UpdateType.InlineQuery => update.InlineQuery.From,
            UpdateType.ChosenInlineResult => update.ChosenInlineResult.From,

            UpdateType.Poll => default,
            UpdateType.PollAnswer => update.PollAnswer.User,

            UpdateType.PreCheckoutQuery => update.PreCheckoutQuery.From,
            UpdateType.ShippingQuery => update.ShippingQuery.From,

            UpdateType.Unknown => default,
            _ => default
        };
    }
}