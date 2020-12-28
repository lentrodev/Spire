#region

using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

#endregion

namespace Spire.Core.Markups.Reply
{
    /// <summary>
    /// Keyboard markup row builder for <see cref="ReplyKeyboardMarkup"/>.
    /// </summary>
    public class ReplyKeyboardMarkupBuilder : KeyboardMarkupBuilderBase<ReplyKeyboardMarkup, KeyboardButton,
        ReplyKeyboardMarkupRowBuilder, ReplyKeyboardMarkupBuildOptions>
    {
        /// <summary>
        /// Creates new <see cref="ReplyKeyboardMarkupBuilder"/>.
        /// </summary>
        public static ReplyKeyboardMarkupBuilder New => new ReplyKeyboardMarkupBuilder();

        /// <summary>
        /// Creates new <see cref="ReplyKeyboardMarkupBuilder"/>.
        /// </summary>
        public ReplyKeyboardMarkupBuilder() : base(KeyboardMarkupSizeLimit.ReplyKeyboard)
        {
        }

        protected override IEnumerable<IEnumerable<KeyboardButton>> DeconstructMarkup(
            ReplyKeyboardMarkup keyboardMarkup)
        {
            return keyboardMarkup.Keyboard;
        }

        protected override ReplyKeyboardMarkup BuildMarkup(IEnumerable<IEnumerable<KeyboardButton>> markupKeyboard,
            ReplyKeyboardMarkupBuildOptions keyboardMarkupBuildOptions = default)
        {
            return new ReplyKeyboardMarkup(markupKeyboard,
                keyboardMarkupBuildOptions?.ResizeKeyboard ?? false,
                keyboardMarkupBuildOptions?.OneTimeKeyboard ?? false);
        }
    }
}