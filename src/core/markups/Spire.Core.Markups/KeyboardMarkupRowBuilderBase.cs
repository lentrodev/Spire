#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Spire.Core.Markups.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

#endregion

namespace Spire.Core.Markups
{
    /// <summary>
    /// Abstract class. Implementation of <see cref="IKeyboardMarkupRowBuilder{TKeyboardMarkupRowBuilder,TKeyboardMarkup,TKeyboardMarkupButton}"/>.
    /// </summary>
    /// <typeparam name="TKeyboardMarkupRowBuilder">Keyboard markup row builder type.</typeparam>
    /// <typeparam name="TKeyboardMarkup">Keyboard markup type.</typeparam>
    /// <typeparam name="TKeyboardMarkupButton">Keyboard markup button type.</typeparam>
    public abstract class KeyboardMarkupRowBuilderBase<TKeyboardMarkupRowBuilder, TKeyboardMarkup,
            TKeyboardMarkupButton>
        : IKeyboardMarkupRowBuilder<TKeyboardMarkupRowBuilder, TKeyboardMarkup, TKeyboardMarkupButton>
        where TKeyboardMarkup : IReplyMarkup
        where TKeyboardMarkupButton : IKeyboardButton
        where TKeyboardMarkupRowBuilder : KeyboardMarkupRowBuilderBase<TKeyboardMarkupRowBuilder, TKeyboardMarkup,
            TKeyboardMarkupButton>
    {
        private readonly ICollection<TKeyboardMarkupButton> _rowButtons;

        protected KeyboardMarkupRowBuilderBase()
        {
            _rowButtons = new Collection<TKeyboardMarkupButton>();
        }

        /// <summary>
        /// Adds new button to the row.
        /// </summary>
        /// <param name="keyboardMarkupButton">Keyboard markup button.</param>
        /// <returns>Configured keyboard markup row builder.</returns>
        public TKeyboardMarkupRowBuilder WithButton(TKeyboardMarkupButton keyboardMarkupButton)
        {
            _rowButtons.Add(keyboardMarkupButton ?? throw new ArgumentNullException(nameof(keyboardMarkupButton)));

            return (TKeyboardMarkupRowBuilder) this;
        }

        /// <summary>
        /// Builds row.
        /// </summary>
        /// <returns>Built row.</returns>
        public IEnumerable<TKeyboardMarkupButton> Build()
        {
            return _rowButtons;
        }
    }
}