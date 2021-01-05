#region

using System;
using System.Collections.Generic;
using System.Linq;
using Spire.Core.Commands.Parsing.Abstractions;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;
using Spire.Core.Commands.Parsing.Parameters;

#endregion

namespace Spire.Core.Commands.Parsing
{
    /// <summary>
    /// Default implementation of <see cref="ICommandParserResult"/>.
    /// </summary>
    public class CommandParserResult : ICommandParserResult
    {
        /// <summary>
        /// Indicates, was the parsing successfully completed or not.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Command format, used for parsing parameter values.
        /// </summary>
        public ICommandFormat CommandFormat { get; }

        /// <summary>
        /// Source command text, used for parsing command parameter values, according to <see cref="CommandFormat"/>.
        /// </summary>
        public string CommandText { get; }

        /// <summary>
        /// Parsed parameters values. If <see cref="IsSuccess"/> is <see langword="false"/>, contains collection of parameters which were unable to parse.
        /// </summary>
        public IEnumerable<ICommandParameterValue> Values { get; }

        private CommandParserResult(
            bool isSuccess,
            ICommandFormat commandFormat,
            string commandText,
            IEnumerable<ICommandParameterValue> values)
        {
            IsSuccess = isSuccess;
            CommandFormat = commandFormat ?? throw new ArgumentNullException(nameof(commandFormat));

            if (string.IsNullOrEmpty(commandText) || string.IsNullOrWhiteSpace(commandText))
            {
                throw new ArgumentNullException(nameof(commandText));
            }
            
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        /// <summary>
        /// Creates successful <see cref="ICommandParserResult"/>.
        /// </summary>
        /// <param name="commandFormat">Command format.</param>
        /// <param name="commandText">Command text.</param>
        /// <param name="values">Successfully parsed command parameter values.</param>
        /// <returns>Successful <see cref="ICommandParserResult"/>.</returns>
        public static CommandParserResult Success(
            ICommandFormat commandFormat,
            string commandText,
            IEnumerable<ICommandParameterValue> values)
            => new CommandParserResult(true, commandFormat, commandText, values);

        /// <summary>
        /// Creates failed <see cref="ICommandParserResult"/>.
        /// </summary>
        /// <param name="commandFormat">Command format.</param>
        /// <param name="commandText">Command text.</param>
        /// <param name="parameters">Command parameters, which were unable to parse.</param>
        /// <returns>Failed <see cref="ICommandParserResult"/>.</returns>
        public static CommandParserResult Fail(
            ICommandFormat commandFormat,
            string commandText,
            IEnumerable<ICommandParameter> parameters)
            => new CommandParserResult(false, commandFormat, commandText,
                parameters.Select(wrongParameter => new CommandParameterValue(wrongParameter, null)));
    }
}