namespace Spire.Core.Commands.Parsing.Abstractions.Parameters.Options
{
    /// <summary>
    /// Base interface for implementing command parameter option handler.
    /// </summary>
    public interface ICommandParameterOptionHandler
    {
        /// <summary>
        /// Command parameter option name this handler related to.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Matches command parameter option.
        /// </summary>
        /// <param name="optionValue">Option value.</param>
        /// <param name="source">Source value, parsed from command text.</param>
        /// <returns>Matching result.</returns>
        bool IsMatch(string optionValue, string source);
    }
}