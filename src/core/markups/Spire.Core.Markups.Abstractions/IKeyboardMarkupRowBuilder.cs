using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Spire.Core.Markups.Abstractions
{
    /// <summary>
    /// Base interface for implementing keyboard markup row builder.
    /// </summary>
    /// <typeparam name="TKeyboardMarkupRowBuilder">Keyboard markup row builder type.</typeparam>
    /// <typeparam name="TKeyboardMarkup">Keyboard markup type.</typeparam>
    /// <typeparam name="TKeyboardMarkupButton">Keyboard markup button type.</typeparam>
    public interface IKeyboardMarkupRowBuilder<out TKeyboardMarkupRowBuilder, TKeyboardMarkup, TKeyboardMarkupButton>
        where TKeyboardMarkup : IReplyMarkup
        where TKeyboardMarkupButton : IKeyboardButton
    where TKeyboardMarkupRowBuilder : IKeyboardMarkupRowBuilder<TKeyboardMarkupRowBuilder, TKeyboardMarkup, TKeyboardMarkupButton>
    {
        /// <summary>
        /// Adds new button to the row.
        /// </summary>
        /// <param name="keyboardMarkupButton">Keyboard markup button.</param>
        /// <returns>Configured keyboard markup row builder.</returns>
        TKeyboardMarkupRowBuilder WithButton(
            TKeyboardMarkupButton keyboardMarkupButton);

        /// <summary>
        /// Builds row.
        /// </summary>
        /// <returns>Built row.</returns>
        IEnumerable<TKeyboardMarkupButton> Build();
    }
}