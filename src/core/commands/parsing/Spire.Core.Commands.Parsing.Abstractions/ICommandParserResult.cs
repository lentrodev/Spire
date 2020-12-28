using System.Collections.Generic;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;

namespace Spire.Core.Commands.Parsing.Abstractions
{
    /// <summary>
    /// Base interface for implementing command parser result.
    /// </summary>
    public interface ICommandParserResult
    {
        /// <summary>
        /// Indicates, was the parsing successfully completed or not.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Command format, used for parsing parameter values.
        /// </summary>
        ICommandFormat CommandFormat { get; }   
        
        /// <summary>
        /// Source command text, used for parsing command parameter values, according to <see cref="CommandFormat"/>.
        /// </summary>
        string CommandText { get; }
        
        /// <summary>
        /// Parsed parameters values. If <see cref="IsSuccess"/> is <see langword="false"/>, contains collection of parameters which were unable to parse.
        /// </summary>
        IEnumerable<ICommandParameterValue> Values { get; }
    }
}