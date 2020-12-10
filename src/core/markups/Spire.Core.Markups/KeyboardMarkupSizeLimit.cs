using Spire.Core.Markups.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

namespace Spire.Core.Markups
{
    /// <summary>
    /// Default implementation of <see cref="IKeyboardMarkupSizeLimit"/>.
    /// </summary>
    public class KeyboardMarkupSizeLimit : IKeyboardMarkupSizeLimit
    {
        /// <summary>
        /// Max buttons count.
        /// </summary>
        public int Buttons { get; }
        
        /// <summary>
        /// Max columns count.
        /// </summary>
        public int Columns { get; }
        
        /// <summary>
        /// Max rows count. 
        /// </summary>
        public int Rows { get; }

        /// <summary>
        /// Creates new instance of <see cref="KeyboardMarkupSizeLimit"/> with specified buttons, columns, and rows counts.
        /// </summary>
        /// <param name="buttons">Max buttons count.</param>
        /// <param name="columns">Max columns count.</param>
        /// <param name="rows">Max rows count. </param>
        public KeyboardMarkupSizeLimit(int buttons, int columns, int rows)
        {
            Buttons = buttons;
            Columns = columns;
            Rows = rows;
        }

        /// <summary>
        /// Creates new <see cref="IKeyboardMarkupSizeLimit"/> for the <see cref="ReplyKeyboardMarkup"/>.
        /// </summary>
        public static IKeyboardMarkupSizeLimit ReplyKeyboard => new KeyboardMarkupSizeLimit(300, 12, 300);
        
        /// <summary>
        /// Creates new <see cref="IKeyboardMarkupSizeLimit"/> for the <see cref="InlineKeyboardMarkup"/>.
        /// </summary>
        public static IKeyboardMarkupSizeLimit InlineKeyboard => new KeyboardMarkupSizeLimit(100, 8, 100);

    }
}