using System;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;

namespace Spire.Core.Commands.Parsing.Parameters
{
    /// <summary>
    /// Default implementation of <see cref="ICommandParameterType"/>.
    /// </summary>
    public class CommandParameterType : ICommandParameterType
    {
        /// <summary>
        /// Command parameter type name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Command parameter type Regular Expression pattern.
        /// </summary>
        public string Pattern { get; }

        /// <summary>
        /// Creates new <see cref="CommandParameterType"/> with specified name and RegEx pattern.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="pattern">RegEx pattern.</param>
        public CommandParameterType(string name, string pattern)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            Pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
        }
    }
}