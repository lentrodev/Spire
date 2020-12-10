using System.Collections.Generic;
using Spire.Core.Markups.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

namespace Spire.Core.Markups.Inline
{
    /// <summary>
    /// Keyboard markup row builder for <see cref="InlineKeyboardMarkup"/>.
    /// </summary>
    public class InlineKeyboardMarkupBuilder : KeyboardMarkupBuilderBase<InlineKeyboardMarkup, InlineKeyboardButton, InlineKeyboardMarkupRowBuilder, InlineKeyboardMarkupBuildOptions>
    {
        /// <summary>
        /// Creates new <see cref="InlineKeyboardMarkupBuilder"/>.
        /// </summary>
        public static InlineKeyboardMarkupBuilder New => new InlineKeyboardMarkupBuilder();
        
        /// <summary>
        /// Creates new <see cref="InlineKeyboardMarkupBuilder"/>.
        /// </summary>
        public InlineKeyboardMarkupBuilder() : base(KeyboardMarkupSizeLimit.InlineKeyboard) { }

        protected override IEnumerable<IEnumerable<InlineKeyboardButton>> DeconstructMarkup(InlineKeyboardMarkup keyboardMarkup)
        {
            return keyboardMarkup.InlineKeyboard;
        }

        protected override InlineKeyboardMarkup BuildMarkup(IEnumerable<IEnumerable<InlineKeyboardButton>> markupKeyboard,
            InlineKeyboardMarkupBuildOptions keyboardMarkupBuildOptions = default)
        {
            return new InlineKeyboardMarkup(markupKeyboard);
        }
    }
}