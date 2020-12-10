#region

using System.Collections.Generic;

#endregion

namespace Spire.Core.Commands.Parsing.Abstractions
{
    /// <summary>
    /// Base interface for implementing command parser.
    /// </summary>
    public interface ICommandParser
    {
        /// <summary>
        /// Parser command format.
        /// </summary>
        string CommandFormat { get; }

        /// <summary>
        /// Default variable type.
        /// </summary>
        IVariableType DefaultVariableType { get; }

        /// <summary>
        /// Collection of variable types could be used.
        /// </summary>
        IReadOnlyDictionary<string, IVariableType> VariableTypes { get; }

        /// <summary>
        /// This is the character that denotes the beginning of a variable name.
        /// </summary>
        string VariableStartChar { get; }

        /// <summary>
        /// This is the character that denotes the end of a variable name.
        /// </summary>
        string VariableEndChar { get; }

        /// <summary>
        /// A collection of all variable names parsed from the <see cref="CommandFormat"/>.
        /// </summary>
        IReadOnlyList<IVariable> Variables { get; }

        /// <summary>
        /// Parses command.
        /// </summary>
        /// <param name="source">Command parsing source.</param>
        /// <returns>Command parser result.</returns>
        ICommandParserResult ParseCommand(string source);
    }
}