using System;
using System.Collections.Generic;
using Spire.Core.Commands.Parsing.Abstractions;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;

namespace Spire.Core.Commands.Parsing
{
    /// <summary>
    /// Default implementation of <see cref="ICommandFormat"/>.
    /// </summary>
    public class CommandFormat : ICommandFormat
    {
        /// <summary>
        /// Command format.
        /// </summary>
        public string Format { get; }
        
        /// <summary>
        /// Command parameters, parsed from <see cref="Format"/>.
        /// </summary>
        public IEnumerable<ICommandParameter> Parameters { get; }

        /// <summary>
        /// Creates new <see cref="CommandFormat"/> with specified format and parameters.
        /// </summary>
        /// <param name="format">Command format.</param>
        /// <param name="parameters">Command parameters.</param>
        public CommandFormat(string format, IEnumerable<ICommandParameter> parameters)
        {
            Format = format ?? throw new ArgumentNullException(nameof(format));
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));;
        }
    }
}