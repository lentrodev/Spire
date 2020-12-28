using System.Collections.Generic;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;

namespace Spire.Core.Commands.Parsing.Abstractions
{
    /// <summary>
    /// Base interface for implementing command format.
    /// </summary>
    public interface ICommandFormat
    {
        /// <summary>
        /// Command format.
        /// </summary>
        string Format { get; }
        
        /// <summary>
        /// Command parameters, parsed from <see cref="Format"/>.
        /// </summary>
        IEnumerable<ICommandParameter> Parameters { get; }
    }
}