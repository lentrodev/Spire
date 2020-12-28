#region

using System.Collections.Generic;
using Spire.Core.Commands.Parsing.Abstractions.Parameters.Options;

#endregion

namespace Spire.Core.Commands.Parsing.Abstractions.Parameters
{
    /// <summary>
    /// Base interface for implementing command parameter type.
    /// </summary>
    public interface ICommandParameter
    {
        /// <summary>
        /// Command parameter format.
        /// </summary>
        string Format { get; }

        /// <summary>
        /// Command parameter name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Command parameter type.
        /// </summary>
        ICommandParameterType Type { get; }

        /// <summary>
        /// Indicates parameter optionality.
        /// </summary>
        bool IsOptional { get; }

        /// <summary>
        /// Command parameter options.
        /// </summary>
        IEnumerable<ICommandParameterOption> Options { get; }
    }
}