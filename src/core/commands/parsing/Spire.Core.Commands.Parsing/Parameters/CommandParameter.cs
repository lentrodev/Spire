using System;
using System.Collections.Generic;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;
using Spire.Core.Commands.Parsing.Abstractions.Parameters.Options;

namespace Spire.Core.Commands.Parsing.Parameters
{
    public class CommandParameter : ICommandParameter
    {
        /// <summary>
        /// Command parameter format.
        /// </summary>
        public string Format { get; }

        /// <summary>
        /// Command parameter name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Command parameter type.
        /// </summary>
        public ICommandParameterType Type { get; }

        /// <summary>
        /// Indicates parameter optionality.
        /// </summary>
        public bool IsOptional { get; }

        /// <summary>
        /// Command parameter options.
        /// </summary>
        public IEnumerable<ICommandParameterOption> Options { get; }

        /// <summary>
        /// Creates new <see cref="CommandParameter"/> with specified format, name, type, optionality flag, and options.
        /// </summary>
        /// <param name="format">Format.</param>
        /// <param name="name">Name.</param>
        /// <param name="type">Type.</param>
        /// <param name="isOptional">Optionality flag.</param>
        /// <param name="options">Options.</param>
        public CommandParameter(string format, string name, ICommandParameterType type, bool isOptional,
            IEnumerable<ICommandParameterOption> options)
        {
            Format = format ?? throw new ArgumentNullException(nameof(format));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            IsOptional = isOptional;
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }
    }
}