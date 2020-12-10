using Spire.Core.Markups.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

namespace Spire.Core.Markups.Reply
{
    /// <summary>
    /// Keyboard markup row builder for <see cref="ReplyKeyboardMarkup"/>. Implementation of <see cref="KeyboardMarkupBuilderBase{TKeyboardMarkup,TKeyboardMarkupButton,TKeyboardMarkupRowBuilder,TKeyboardMarkupBuildOptions}"/>.
    /// </summary>
    public class ReplyKeyboardMarkupRowBuilder : KeyboardMarkupRowBuilderBase<ReplyKeyboardMarkupRowBuilder, ReplyKeyboardMarkup, KeyboardButton>
    {
        /// <summary>
        /// Generate a keyboard button to request for contact and adds it to the row.
        /// </summary>
        /// <param name="text">Button's text.</param>
        /// <returns>Configured reply keyboard markup row builder.</returns>
        public ReplyKeyboardMarkupRowBuilder WithRequestContactButton(string text) =>
            WithButton(KeyboardButton.WithRequestContact(text));

        /// <summary>
        /// Creates a keyboard button to request for location and adds it to the row.
        /// </summary>
        /// <param name="text">Button's text.</param>
        /// <returns>Configured reply keyboard markup row builder.</returns>
        public ReplyKeyboardMarkupRowBuilder WithRequestLocationButton(string text) =>
            WithButton(KeyboardButton.WithRequestLocation(text));

        /// <summary>
        /// Generate a keyboard button to request a poll and adds it to the row.
        /// </summary>
        /// <param name="text">Button's text.</param>
        /// <param name="type">Poll's type.</param>
        /// <returns>Configured reply keyboard markup row builder.</returns>
        public ReplyKeyboardMarkupRowBuilder WithRequestPollButton(string text, string type = default) =>
            WithButton(KeyboardButton.WithRequestPoll(text, type));
    }
}