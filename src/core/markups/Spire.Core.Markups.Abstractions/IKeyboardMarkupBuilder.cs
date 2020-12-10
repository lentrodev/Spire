using System;
using Telegram.Bot.Types.ReplyMarkups;

namespace Spire.Core.Markups.Abstractions
{
    /// <summary>
    /// Base interface for implementing keyboard markup builder.
    /// </summary>
    /// <typeparam name="TKeyboardMarkup">Keyboard markup type.</typeparam>
    /// <typeparam name="TKeyboardMarkupButton">Keyboard markup button type.</typeparam>
    /// <typeparam name="TKeyboardMarkupRowBuilder">Keyboard markup row builder type.</typeparam>
    /// <typeparam name="TKeyboardMarkupBuildOptions">Keyboard markup build options type.</typeparam> 
    public interface IKeyboardMarkupBuilder<TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder, TKeyboardMarkupBuildOptions> 
        where TKeyboardMarkup : IReplyMarkup
        where  TKeyboardMarkupButton : IKeyboardButton
        where TKeyboardMarkupBuildOptions : IKeyboardMarkupBuildOptions
        where TKeyboardMarkupRowBuilder : IKeyboardMarkupRowBuilder<TKeyboardMarkupRowBuilder, TKeyboardMarkup, TKeyboardMarkupButton>

    {
        /// <summary>
        /// Configures and adds row to the markup builder.
        /// </summary>
        /// <param name="configureRow">Row configurator func.</param>
        /// <returns>Configured keyboard markup builder.</returns>
        IKeyboardMarkupBuilder<TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder, TKeyboardMarkupBuildOptions> 
            WithRow(Action<TKeyboardMarkupRowBuilder> configureRow);

        /// <summary>
        /// Combines an another one <see cref="IKeyboardMarkupBuilder{TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder, TKeyboardMarkupBuildOptions}"/> with the current.
        /// </summary>
        /// <param name="keyboardMarkupBuilder">Keyboard markup builder to combine.</param>
        /// <returns>Configured keyboard markup builder.</returns>
        IKeyboardMarkupBuilder<TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder,
            TKeyboardMarkupBuildOptions> Combine(IKeyboardMarkupBuilder<TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder,
            TKeyboardMarkupBuildOptions> keyboardMarkupBuilder);

        /// <summary>
        /// Adds buttons from <see cref="TKeyboardMarkupButton"/> to the current keyboard markup builder.
        /// </summary>
        /// <param name="keyboardMarkup">Keyboard markup.</param>
        /// <returns>Configured keyboard markup builder.</returns>
        IKeyboardMarkupBuilder<TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder,
            TKeyboardMarkupBuildOptions> Combine(TKeyboardMarkup keyboardMarkup);

        /// <summary>
        /// Clears the keyboard markup builder (deletes all buttons).
        /// </summary>
        /// <returns>Configured keyboard markup builder.</returns>
        IKeyboardMarkupBuilder<TKeyboardMarkup, TKeyboardMarkupButton, TKeyboardMarkupRowBuilder,
            TKeyboardMarkupBuildOptions> Clear();
        
        /// <summary>
        /// Builds keyboard markup.
        /// </summary>
        /// <param name="keyboardMarkupBuildOptions">Keyboard markup build options.</param>
        /// <returns>Built keyboard markup.</returns>
        TKeyboardMarkup Build(TKeyboardMarkupBuildOptions keyboardMarkupBuildOptions = default);
    }
}