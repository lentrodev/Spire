#region

using System.Collections.Generic;
using Spire.Core.Commands.Parsing.Abstractions;

#endregion

namespace Spire.Core.Commands.Parsing
{
    /// <summary>
    /// Default implementation of <see cref="ICommandParserResult"/>.
    /// </summary>
    public class CommandParserResult : ICommandParserResult
    {
        /// <summary>
        /// Parsed variables.
        /// </summary>
        public IReadOnlyDictionary<string, string> Variables { get; }

        /// <summary>
        /// Indicated parse status.
        /// </summary>
        public bool Successful { get; }

        /// <summary>
        /// Command format.
        /// </summary>
        public string CommandFormat { get; }

        /// <summary>
        /// Creates new failed <see cref="ICommandParserResult"/>.
        /// </summary>
        /// <param name="badValues">Variables, which weren't parsed.</param>
        /// <param name="commandFormat">Command format.</param>
        /// <returns>Command parser result.</returns>
        public static ICommandParserResult Failure(IReadOnlyDictionary<string, string> badValues, string commandFormat)
            => new CommandParserResult(commandFormat, false, badValues);

        /// <summary>
        /// Creates new success <see cref="ICommandParserResult"/>.
        /// </summary>
        /// <param name="values">Parsed variables.</param>
        /// <param name="commandFormat">Command format.</param>
        /// <returns>Command parser result.</returns>
        public static ICommandParserResult Success(IReadOnlyDictionary<string, string> values, string commandFormat)
            => new CommandParserResult(commandFormat, true, values);

        private CommandParserResult(
            string commandFormat,
            bool successful = default,
            IReadOnlyDictionary<string, string> variables = default)
        {
            CommandFormat = commandFormat;
            Variables = variables;
            Successful = successful;
        }
    }
}