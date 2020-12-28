using System;
using Spire.Core.Commands.Parsing.Abstractions.Parameters.Options;

namespace Spire.Core.Commands.Parsing.Parameters.Options
{
    /// <summary>
    /// Default implementation of <see cref="ICommandParameterOption"/>.
    /// </summary>
    public class CommandParameterOption : ICommandParameterOption
    {
        /// <summary>
        /// Command parameter option name.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Command parameter option value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates new <see cref="CommandParameterOption"/> with specified name and value.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="value">Value.</param>
        public CommandParameterOption(string name, string value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}