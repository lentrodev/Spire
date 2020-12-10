#region

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Spire.Core.Commands.Parsing.Abstractions;

#endregion

namespace Spire.Core.Commands.Parsing
{
    /// <summary>
    /// Default implementation of <see cref="ICommandParser"/>.
    /// </summary>
    public class CommandParser : ICommandParser
    {
        private Regex _regex;

        /// <summary>
        /// Creates new <see cref="CommandParser"/> with specified parameters.
        /// </summary>
        /// <param name="commandFormat">Command format.</param>
        /// <param name="variableStartChar">Variable start char.</param>
        /// <param name="variableEndChar">Variable end char.</param>
        /// <param name="variableTypes">Allowed variable types.</param>
        /// <param name="optionsHandlers">Options handlers.</param>
        /// <param name="defaultVariableType">Default variable type.</param>
        /// <exception cref="ArgumentNullException">Throws, if some parameters is null or empty.</exception>
        /// <exception cref="ArgumentException">Throws, if some parameters is null or empty.</exception>
        public CommandParser(
            string commandFormat,
            string variableStartChar,
            string variableEndChar,
            IReadOnlyDictionary<string, IVariableType> variableTypes,
            IReadOnlyDictionary<string, Func<string, string, bool>> optionsHandlers,
            IVariableType defaultVariableType)
        {
            if (string.IsNullOrWhiteSpace(commandFormat))
            {
                throw new ArgumentNullException(nameof(commandFormat));
            }

            if (string.IsNullOrWhiteSpace(variableStartChar))
            {
                throw new ArgumentNullException(nameof(variableStartChar));
            }

            if (string.IsNullOrWhiteSpace(variableEndChar))
            {
                throw new ArgumentNullException(nameof(variableStartChar));
            }

            if (variableTypes.Count == 0)
            {
                throw new ArgumentException(nameof(variableTypes), "Variable types collection is empty");
            }

            if (defaultVariableType == default)
            {
                throw new ArgumentNullException(nameof(defaultVariableType));
            }

            CommandFormat = commandFormat;
            VariableTypes = variableTypes;
            DefaultVariableType = defaultVariableType;
            VariableStartChar = variableStartChar;
            VariableEndChar = variableEndChar;
            OptionsHandlers = optionsHandlers;
            ParseCommandFormat();
        }

        // the 0 and 1 are used by the string.Format function, they are the start and end characters.
        private static readonly string CommandTokenPattern =
            @"[{0}](?<variable>[a-zA-Z0-9_]+?)(:(?<type>[a-z]+?))?(:(?<optional>[a-z]+?))?(:(?<options>[^{0}]+))?[{1}]";

        // the <>'s denote the group name; this is used for reference for the variables later.
        private static readonly string VariableTokenPattern = @"(?<{0}>{1})";

        /// <summary>
        /// Parser command format.
        /// </summary>
        public string CommandFormat { get; }

        /// <summary>
        /// Default variable type.
        /// </summary>
        public IVariableType DefaultVariableType { get; }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyDictionary<string, Func<string, string, bool>> OptionsHandlers { get; }

        /// <summary>
        /// Collection of variable types could be used.
        /// </summary>
        public IReadOnlyDictionary<string, IVariableType> VariableTypes { get; }

        /// <summary>
        /// This is the character that denotes the beginning of a variable name.
        /// </summary>
        public string VariableStartChar { get; }

        /// <summary>
        /// This is the character that denotes the end of a variable name.
        /// </summary>
        public string VariableEndChar { get; }

        /// <summary>
        /// A hash set of all variable names parsed from the <see cref="CommandFormat"/>
        /// </summary>
        public IReadOnlyList<IVariable> Variables { get; private set; }

        /// <summary>
        /// Parses command.
        /// </summary>
        /// <param name="source">Command parsing source.</param>
        /// <returns>Command parser result.</returns>
        public ICommandParserResult ParseCommand(string source)
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();

            string commandExpression = CommandFormat.Replace(" ", @"\s*");

            foreach (var variable in Variables)
            {
                commandExpression = commandExpression.Replace(variable.OriginalPattern,
                    $"(?<{variable.Name}>" + variable.Type.Pattern + ")" + (variable.IsOptional ? "?" : ""));
            }

            var smatch = Regex.Match(source, commandExpression, RegexOptions.IgnoreCase);

            bool successParsing = smatch.Success;

            Dictionary<string, string> unparsedVariables = new Dictionary<string, string>();

            foreach (var variable in Variables)
            {
                string variableValue = smatch.Groups?[variable.Name]?.Value ?? "";

                foreach (KeyValuePair<string, string> option in variable.Options)
                {
                    if (OptionsHandlers.ContainsKey(option.Key))
                    {
                        successParsing = OptionsHandlers[option.Key].Invoke(variableValue, option.Value);
                    }
                }

                if (successParsing)
                    variables.Add(variable.Name, variableValue);
                else unparsedVariables.Add(variable.Name, variableValue);
            }

            return successParsing
                ? CommandParserResult.Success(variables, CommandFormat)
                : CommandParserResult.Failure(unparsedVariables, CommandFormat);
        }

        private void ParseCommandFormat()
        {
            var variableList = new List<IVariable>();
            var matchCollection = Regex.Matches(
                CommandFormat,
                string.Format(CommandTokenPattern, VariableStartChar, VariableEndChar),
                RegexOptions.IgnoreCase);

            foreach (Match match in matchCollection)
            {
                var variable = CreateVariable(match);

                if (variableList.Contains(variable))
                {
                    throw new InvalidOperationException($"Variable name '{match}' is used more than once");
                }

                variableList.Add(variable);
            }

            Variables = variableList.AsReadOnly();

            var format = CommandFormat;

            foreach (var variable in Variables)
            {
                format = format.Replace(
                    variable.OriginalPattern,
                    string.Format(
                        VariableTokenPattern,
                        variable.Name,
                        variable.Type.Pattern
                    )
                );
            }

            _regex = new Regex($"^{format}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Extract variable values from a given instance of the command you're trying to parse.
        /// </summary>
        /// <param name="match">Variable match.</param>
        /// <returns>An instance of <see cref="CommandParserResult"/> indicating success or failure with a dictionary of Variable names mapped to values if success.</returns>
        private Variable CreateVariable(Match match)
        {
            if (!match.Groups["variable"].Success)
            {
                throw new InvalidOperationException();
            }

            IVariableType variableType;

            var variableTypeName = match.Groups["type"].Success
                ? match.Groups["type"].Value
                : null;
            bool autoType = false;
            if (string.IsNullOrWhiteSpace(variableTypeName))
            {
                variableType = DefaultVariableType;
                autoType = true;
            }
            else if (VariableTypes.ContainsKey(variableTypeName))
            {
                variableType = VariableTypes[variableTypeName];
            }
            else
            {
                throw new InvalidOperationException($"Invalid variable type '{variableTypeName}'");
            }

            var variableName = match.Groups["variable"].Value;

            bool isOptional = false;
            if (match.Groups["optional"] != null)
            {
                bool.TryParse(match.Groups["optional"].Value, out isOptional);
            }

            string optionsString = match.Groups["options"].Value;

            IDictionary<string, string> optionsDictionary = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(optionsString))
            {
                string[] options = optionsString.Split(';', StringSplitOptions.RemoveEmptyEntries);

                foreach (string option in options)
                {
                    string[] optionParts = option.Split("=");
                    optionsDictionary.Add(optionParts[0], optionParts[1]);
                }
            }

            return new Variable(variableName, match.Value, variableType, isOptional, autoType, optionsDictionary);
        }
    }
}