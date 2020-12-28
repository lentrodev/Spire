namespace Spire.Core.Commands.Parsing.Abstractions
{
    /// <summary>
    /// Base interface for implementing command parser configuration.
    /// </summary>
    public interface ICommandParserConfiguration
    {
        /// <summary>
        /// Parameter start token. Defaults to '{'.
        /// </summary>
        string ParameterStartToken { get; }

        /// <summary>
        /// Parameter end token. Defaults to '}'.
        /// </summary>
        string ParameterEndToken { get; }

        /// <summary>
        /// Parameter settings delimiter. Defaults to ':'.
        /// </summary>
        string ParameterSettingsDelimiter { get; }

        /// <summary>
        /// Token, used for delimiting parameter settings and parameter options. Defaults to '?'.
        /// </summary>
        string OptionsStartToken { get; }

        /// <summary>
        /// Delimiter, used to delimit parameter options. Defaults to '&'.
        /// </summary>
        string OptionsDelimiter { get; }

        /// <summary>
        /// Delimiter, used for delimit option name and value. Defaults to '='. 
        /// </summary>
        string OptionNameValueDelimiter { get; }

        /// <summary>
        /// Indicates, should be all whitespaces replaced by \s+ regular expression pattern. This allows parameters to be delimited with unlimited whitespaces. Defaults to <see langword="true"/>
        /// </summary>
        bool ReplaceWhitespaceWithPattern { get; }
    }
}