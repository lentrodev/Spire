#region

using Spire.Core.Markups.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

#endregion

namespace Spire.Core.Markups.Reply
{
    /// <summary>
    /// Keyboard markup build options for <see cref="ReplyKeyboardMarkup"/>.
    /// </summary>
    public class ReplyKeyboardMarkupBuildOptions : IKeyboardMarkupBuildOptions
    {
        /// <summary>
        /// Requests clients to resize the keyboard vertically for optimal fit (e.g., make the keyboard smaller if there are just two rows of <see cref="KeyboardButton"/>). Defaults to <c>false</c>, in which case the custom keyboard is always of the same height as the app's standard keyboard.
        /// </summary>
        public bool ResizeKeyboard { get; set; }

        /// <summary>
        /// Optional. Requests clients to hide the keyboard as soon as it's been used. Defaults to <c>false</c>.
        /// </summary>
        public bool OneTimeKeyboard { get; set; }

        /// <summary>
        /// Creates new <see cref="ReplyKeyboardMarkupBuildOptions"/> with specified options.
        /// </summary>
        /// <param name="resizeKeyboard"></param>
        /// <param name="oneTimeKeyboard"></param>
        public ReplyKeyboardMarkupBuildOptions(
            bool resizeKeyboard = false,
            bool oneTimeKeyboard = false)
        {
            ResizeKeyboard = resizeKeyboard;
            OneTimeKeyboard = oneTimeKeyboard;
        }
    }
}