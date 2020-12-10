#region

using System.Collections.Generic;

#endregion

namespace Spire.Core.Commands.Parsing.Abstractions
{
    /// <summary>
    /// Base interface for implementing command parser result.
    /// </summary>
    public interface ICommandParserResult
    {
        /// <summary>
        /// Parsed variables.
        /// </summary>
        IReadOnlyDictionary<string, string> Variables { get; }

        /// <summary>
        /// Indicated parse status.
        /// </summary>
        bool Successful { get; }

        /// <summary>
        /// Command format.
        /// </summary>
        string CommandFormat { get; }
    }
}