using Spire.Core.Commands.Parsing.Abstractions;

namespace Spire.Core.Commands.Parsing
{
    /// <summary>
    /// Default implementation of <see cref="ICommandParserConfiguration"/>.
    /// </summary>
    public class CommandParserConfiguration : ICommandParserConfiguration
    {
        /// <summary>
        /// Parameter start token. Defaults to '{'.
        /// </summary>
        public string ParameterStartToken { get; set; }
        
        /// <summary>
        /// Parameter end token. Defaults to '}'.
        /// </summary>
        public string ParameterEndToken { get; set; }
        
        /// <summary>
        /// Parameter settings delimiter. Defaults to ':'.
        /// </summary>
        public string ParameterSettingsDelimiter { get; set; }
        
        /// <summary>
        /// Token, used for delimiting parameter settings and parameter options. Defaults to '?'.
        /// </summary>
        public string OptionsStartToken { get; set; }
        
        /// <summary>
        /// Delimiter, used to delimit parameter options. Defaults to '&'.
        /// </summary>
        public string OptionsDelimiter { get; set; }
        
        /// <summary>
        /// Delimiter, used for delimit option name and value. Defaults to '='. 
        /// </summary>
        public string OptionNameValueDelimiter { get; set; }
        
        /// <summary>
        /// Indicates, should be all whitespaces replaced by \s+ regular expression pattern. This allows parameters to be delimited with unlimited whitespaces. Defaults to <see langword="true"/>
        /// </summary>
        public bool ReplaceWhitespaceWithPattern { get; set; }
        
        /// <summary>
        /// Creates default <see cref="ICommandParserConfiguration"/>.
        /// </summary>
        public static ICommandParserConfiguration Default { get; } = new CommandParserConfiguration()
        {
            ParameterStartToken = "{",
            ParameterEndToken = "}",
            ParameterSettingsDelimiter = ":",
            OptionsStartToken = "?",
            OptionsDelimiter = "&",
            OptionNameValueDelimiter = "=",
            ReplaceWhitespaceWithPattern = true
        };
    }
}