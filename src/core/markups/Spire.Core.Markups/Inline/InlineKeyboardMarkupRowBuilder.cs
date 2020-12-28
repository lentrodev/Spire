#region

using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

#endregion

namespace Spire.Core.Markups.Inline
{
    /// <summary>
    /// Keyboard markup row builder for <see cref="InlineKeyboardMarkup"/>. Implementation of <see cref="KeyboardMarkupRowBuilderBase{InlineKeyboardMarkupRowBuilder, InlineKeyboardMarkup, InlineKeyboardButton}"/>
    /// </summary>
    public class InlineKeyboardMarkupRowBuilder : KeyboardMarkupRowBuilderBase<InlineKeyboardMarkupRowBuilder,
        InlineKeyboardMarkup, InlineKeyboardButton>
    {
        /// <summary>
        /// Creates an inline keyboard button that opens a HTTP url when pressed and adds it to the row.
        /// </summary>
        /// <param name="text">Label text on the button.</param>
        /// <param name="url">HTTP url to be opened when button is pressed.</param>
        /// <returns>Configured inline keyboard markup row builder.</returns>
        public InlineKeyboardMarkupRowBuilder WithUrlButton(string text, string url) =>
            WithButton(InlineKeyboardButton.WithUrl(text, url));

        /// <summary>
        /// Creates an inline keyboard button that opens a HTTP URL to automatically authorize the user and adds it to the row.
        /// </summary>
        /// <param name="text">Label text on the button.</param>
        /// <param name="loginUrl">An HTTP URL used to automatically authorize the user.</param>
        /// <returns>Configured inline keyboard markup row builder.</returns>
        public InlineKeyboardMarkupRowBuilder WithLoginUrlButton(string text, LoginUrl loginUrl) =>
            WithButton(InlineKeyboardButton.WithLoginUrl(text, loginUrl));

        /// <summary>
        /// Creates an inline keyboard button that sends <see cref="CallbackQuery"/> to bot when pressed and adds it to the row.
        /// </summary>
        /// <param name="textAndCallbackData">Text of the button and data to be sent in a <see cref="CallbackQuery"/> to the bot when button is pressed, 1-64 bytes.</param>
        /// <returns>Configured inline keyboard markup row builder.</returns>
        public InlineKeyboardMarkupRowBuilder WithCallbackDataButton(string textAndCallbackData) =>
            WithButton(InlineKeyboardButton.WithCallbackData(textAndCallbackData));

        /// <summary>
        /// Creates an inline keyboard button that sends <see cref="CallbackQuery"/> to bot when pressed and adds it to the row.
        /// </summary>
        /// <param name="text">Label text on the button.</param>
        /// <param name="callbackData">Data to be sent in a <see cref="CallbackQuery"/> to the bot when button is pressed, 1-64 bytes.</param>
        /// <returns>Configured inline keyboard markup row builder.</returns>
        public InlineKeyboardMarkupRowBuilder WithCallbackDataButton(string text, string callbackData) =>
            WithButton(InlineKeyboardButton.WithCallbackData(text, callbackData));

        /// <summary>
        /// Creates an inline keyboard button and adds it to the row. Pressing the button will prompt the user to select one of their chats, open that chat and insert the bot's username and the specified inline query in the input field.
        /// </summary>
        /// <param name="text">Label text on the button.</param>
        /// <param name="query">Inline query that appears in the input field. Can be empty, in which case just the bot's username will be inserted.</param>
        /// <returns>Configured inline keyboard markup row builder.</returns>
        public InlineKeyboardMarkupRowBuilder WithSwitchInlineQueryButton(string text, string query = "") =>
            WithButton(InlineKeyboardButton.WithSwitchInlineQuery(text, query));

        /// <summary>
        /// Creates an inline keyboard button and adds it to the row. Pressing the button will insert the bot's username and the specified inline query in the current chat's input field.
        /// </summary>
        /// <param name="text">Label text on the button.</param>
        /// <param name="query">Inline query that appears in the input field. Can be empty, in which case just the bot's username will be inserted.</param>
        /// <returns>Configured inline keyboard markup row builder.</returns>
        public InlineKeyboardMarkupRowBuilder WithSwitchInlineQueryCurrentChatButton(string text, string query = "") =>
            WithButton(InlineKeyboardButton.WithSwitchInlineQueryCurrentChat(text, query));

        /// <summary>
        /// Creates an inline keyboard button and adds it to the row. Pressing the button will launch the game.
        /// </summary>
        /// <param name="text">Label text on the button.</param>
        /// <param name="callbackGame">Description of the game that will be launched when the user presses the button.</param>
        /// <returns>Configured inline keyboard markup row builder.</returns>
        public InlineKeyboardMarkupRowBuilder WithCallBackGameButton(string text, CallbackGame callbackGame = null) =>
            WithButton(InlineKeyboardButton.WithCallBackGame(text, callbackGame));

        /// <summary>
        /// Creates an inline keyboard button for a PayButton and adds it to the row.
        /// </summary>
        /// <param name="text">Label text on the button.</param>
        /// <returns>Configured inline keyboard markup row builder.</returns>
        public InlineKeyboardMarkupRowBuilder WithPaymentButton(string text) =>
            WithButton(InlineKeyboardButton.WithPayment(text));
    }
}