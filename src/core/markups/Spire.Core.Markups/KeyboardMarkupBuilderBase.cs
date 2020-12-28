#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Spire.Core.Markups.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

#endregion

namespace Spire.Core.Markups
{
    /// <summary>
    /// Abstract class. Default implementation of <see cref="IKeyboardMarkupBuilder{TKeyboardMarkup,TKeyboardMarkupButton,TKeyboardMarkupRowBuilder,TKeyboardMarkupBuildOptions}"/>.
    /// </summary>
    /// <typeparam name="TKeyboardMarkup">Keyboard markup type.</typeparam>
    /// <typeparam name="TKeyboardMarkupButton">Keyboard markup button type.</typeparam>
    /// <typeparam name="TKeyboardMarkupRowBuilder">Keyboard markup row builder type.</typeparam>
    /// <typeparam name="TKeyboardMarkupBuildOptions">Keyboard markup build options type.</typeparam> 
    public abstract class KeyboardMarkupBuilderBase<TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder,
        TKeyboardMarkupBuildOptions> : IKeyboardMarkupBuilder<TKeyboardMarkup, TKeyboardMarkupButton,
        TKeyboardMarkupRowBuilder, TKeyboardMarkupBuildOptions>
        where TKeyboardMarkup : IReplyMarkup
        where TKeyboardMarkupButton : IKeyboardButton
        where TKeyboardMarkupBuildOptions : IKeyboardMarkupBuildOptions
        where TKeyboardMarkupRowBuilder :
        IKeyboardMarkupRowBuilder<TKeyboardMarkupRowBuilder, TKeyboardMarkup, TKeyboardMarkupButton>, new()
    {
        private readonly ICollection<IEnumerable<TKeyboardMarkupButton>> _keyboardMarkupButtons;

        private readonly IKeyboardMarkupSizeLimit _keyboardMarkupSizeLimit;

        protected KeyboardMarkupBuilderBase(IKeyboardMarkupSizeLimit keyboardMarkupSizeLimit)
        {
            _keyboardMarkupButtons = new Collection<IEnumerable<TKeyboardMarkupButton>>();
            _keyboardMarkupSizeLimit = keyboardMarkupSizeLimit;
        }

        /// <summary>
        /// Clears the keyboard markup builder (deletes all buttons).
        /// </summary>
        /// <returns>Configured keyboard markup builder.</returns>
        public IKeyboardMarkupBuilder<TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder,
            TKeyboardMarkupBuildOptions> Clear()
        {
            _keyboardMarkupButtons.Clear();

            return this;
        }

        /// <summary>
        /// Configures and adds row to the markup builder.
        /// </summary>
        /// <param name="configureRow">Row configurator func.</param>
        /// <returns>Configured keyboard markup builder.</returns>
        public IKeyboardMarkupBuilder<TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder,
            TKeyboardMarkupBuildOptions> WithRow(Action<TKeyboardMarkupRowBuilder> configureRow)
        {
            TKeyboardMarkupRowBuilder keyboardMarkupRowBuilder = new TKeyboardMarkupRowBuilder();

            configureRow?.Invoke(keyboardMarkupRowBuilder);

            IEnumerable<TKeyboardMarkupButton> builtRow = keyboardMarkupRowBuilder.Build();

            _keyboardMarkupButtons.Add(builtRow);

            return this;
        }

        /// <summary>
        /// Combines an another one <see cref="IKeyboardMarkupBuilder{TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder, TKeyboardMarkupBuildOptions}"/> with the current.
        /// </summary>
        /// <param name="keyboardMarkupBuilder">Keyboard markup builder to combine.</param>
        /// <returns>Configured keyboard markup builder.</returns>
        public
            IKeyboardMarkupBuilder<TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder,
                TKeyboardMarkupBuildOptions> Combine(
                IKeyboardMarkupBuilder<TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder,
                    TKeyboardMarkupBuildOptions> keyboardMarkupBuilder)
        {
            if (keyboardMarkupBuilder == null)
            {
                throw new ArgumentNullException(nameof(keyboardMarkupBuilder));
            }

            return Combine(keyboardMarkupBuilder.Build());
        }

        /// <summary>
        /// Adds buttons from <see cref="TKeyboardMarkupButton"/> to the current keyboard markup builder.
        /// </summary>
        /// <param name="keyboardMarkup">Keyboard markup.</param>
        /// <returns>Configured keyboard markup builder.</returns>
        public
            IKeyboardMarkupBuilder<TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder,
                TKeyboardMarkupBuildOptions> Combine(TKeyboardMarkup keyboardMarkup)
        {
            foreach (IEnumerable<TKeyboardMarkupButton> row in DeconstructMarkup(keyboardMarkup))
            {
                _keyboardMarkupButtons.Add(row);
            }

            return this;
        }

        protected abstract IEnumerable<IEnumerable<TKeyboardMarkupButton>> DeconstructMarkup(
            TKeyboardMarkup keyboardMarkup);

        protected abstract TKeyboardMarkup BuildMarkup(IEnumerable<IEnumerable<TKeyboardMarkupButton>> markupKeyboard,
            TKeyboardMarkupBuildOptions keyboardMarkupBuildOptions = default);

        /// <summary>
        /// Builds keyboard markup.
        /// </summary>
        /// <param name="keyboardMarkupBuildOptions">Keyboard markup build options.</param>
        /// <exception cref="InvalidOperationException">When keyboard markup exceeds it's limit.</exception>
        /// <returns>Built keyboard markup.</returns>
        public TKeyboardMarkup Build(TKeyboardMarkupBuildOptions keyboardMarkupBuildOptions = default)
        {
            if (_keyboardMarkupButtons.SelectMany(x => x).Count() > _keyboardMarkupSizeLimit.Buttons)
            {
                throw new InvalidOperationException(
                    $"You can't new button(s) to the keyboard markup. Buttons count can't exceed {_keyboardMarkupSizeLimit.Buttons}.");
            }

            if (_keyboardMarkupButtons.Count > _keyboardMarkupSizeLimit.Rows)
            {
                throw new InvalidOperationException(
                    $"You can't add a new row to the keyboard markup. Rows count can't exceed {_keyboardMarkupSizeLimit.Rows}.");
            }

            if (_keyboardMarkupButtons.Max(row => row.Count()) > _keyboardMarkupSizeLimit.Columns)
            {
                throw new InvalidOperationException(
                    $"You can't add a new column to the keyboard markup. Columns count can't exceed {_keyboardMarkupSizeLimit.Columns}.");
            }

            return BuildMarkup(_keyboardMarkupButtons, keyboardMarkupBuildOptions);
        }
    }
}