using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Spire.Core.Commands.Parsing.Abstractions;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;
using Spire.Core.Commands.Parsing.Abstractions.Parameters.Options;
using Spire.Core.Commands.Parsing.Parameters;
using Spire.Core.Commands.Parsing.Parameters.Options;

namespace Spire.Core.Commands.Parsing
{
    /// <summary>
    /// Default implementation of <see cref="ICommandParser"/>.
    /// </summary>
    public class CommandParser : ICommandParser
    {
        #region Public Properties

        /// <summary>
        /// Command parser configuration.
        /// </summary>
        public ICommandParserConfiguration Configuration { get; }
        
        /// <summary>
        /// Collection of command parameter option handlers.
        /// </summary>
        public IEnumerable<ICommandParameterOptionHandler> OptionHandlers { get; }

        /// <summary>
        /// Collection of command parameter types.
        /// </summary>
        public IEnumerable<ICommandParameterType> Types { get; }

        /// <summary>
        /// Command parameter pattern.
        /// </summary>
        public string ParameterPattern { get; }

        /// <summary>
        /// Command parameter option pattern.
        /// </summary>
        public string ParameterOptionPattern { get; }
        
        #endregion
        
        #region RegEx pattern templates 
        
        private readonly string ParameterTemplatePattern
            = "[{0}](?<Name>[A-Za-z0-9_-]+)([{2}](?<Type>\\w+))?([{2}](?<IsOptional>(optional|true)))?([{3}](?<Options>[^{0}{1}{2}{3}]+))?[{1}]";

        private readonly string ParameterOptionTemplatePattern = "(?<Name>[A-Za-z0-9_-]+)[{0}](?<Value>[^{0}{2}{1}]*)";

        private readonly string ParameterRegexTemplatePattern = "(?<{0}>{1})";

        #endregion
        
        #region Public methods and constructors
        
        /// <summary>
        /// Creates new <see cref="CommandParser"/> with specified configuration, types, and option handlers.
        /// </summary>
        /// <param name="commandParserConfiguration">Configuration.</param>
        /// <param name="parameterTypes">Parameter types.</param>
        /// <param name="optionsHandlers">Option handlers.</param>
        /// <exception cref="InvalidOperationException">If parameter types or option handlers contains duplicates.</exception>
        public CommandParser(ICommandParserConfiguration commandParserConfiguration,
            IEnumerable<ICommandParameterType> parameterTypes,
            IEnumerable<ICommandParameterOptionHandler> optionsHandlers)
        {
            Configuration = commandParserConfiguration ?? throw new ArgumentNullException(nameof(commandParserConfiguration));
            OptionHandlers = optionsHandlers ?? throw new ArgumentNullException(nameof(optionsHandlers));
            Types = parameterTypes ?? throw new ArgumentNullException(nameof(parameterTypes));

            IEnumerable<string> optionHandlersDuplicates =
                FindDuplicates(OptionHandlers, handler => handler.Name).ToList();

            if (optionHandlersDuplicates.Any())
            {
                throw new InvalidOperationException($"There are duplicated command parameter option handlers with these names: {string.Join(", ", optionHandlersDuplicates.Select(duplicate => $"'{duplicate}'"))}");
            }
            
            IEnumerable<string> parameterTypesDuplicates =
                FindDuplicates(Types, handler => handler.Name).ToList();

            if (parameterTypesDuplicates.Any())
            {
                throw new InvalidOperationException($"There are duplicated command parameter types with these names: {string.Join(", ", parameterTypesDuplicates.Select(duplicate => $"'{duplicate}'"))}");
            }
            
            
            ParameterPattern = string.Format(
                ParameterTemplatePattern,
                Configuration.ParameterStartToken,
                Configuration.ParameterEndToken,
                Configuration.ParameterSettingsDelimiter,
                Configuration.OptionsStartToken);

            ParameterOptionPattern = string.Format(ParameterOptionTemplatePattern,
                Configuration.OptionNameValueDelimiter,
                Configuration.ParameterStartToken,
                Configuration.OptionsDelimiter
            );
        }

        /// <summary>
        /// Parses <see cref="ICommandFormat"/>.
        /// </summary>
        /// <param name="formatSource">Command format source text.</param>
        /// <returns>Parsed command format.</returns>
        public ICommandFormat ParseCommandFormat(string formatSource)
        {
            Regex parameterRegex = new Regex(ParameterPattern);

            ICollection<ICommandParameter> commandParameters = new Collection<ICommandParameter>();

            foreach (Match parameterMatch in parameterRegex.Matches(formatSource))
            {
                GroupCollection parameterGroups = parameterMatch.Groups;

                string parameterName = parameterGroups["Name"].Value;

                Group typeGroup = parameterGroups["Type"];

                ICommandParameterType parameterType = null;

                if (typeGroup.Success)
                {
                    foreach (ICommandParameterType type in Types)
                    {
                        if (string.Compare(type.Name, typeGroup.Value, StringComparison.Ordinal) == 0)
                        {
                            parameterType = type;
                        }
                    }

                    if (parameterType == null)
                    {
                        throw new InvalidOperationException(
                            $"You have specified invalid command parameter type: {typeGroup.Value}.");
                    }
                }
                else
                {
                    parameterType = Types.First();
                }

                bool isOptional = parameterGroups["IsOptional"].Success;

                IEnumerable<ICommandParameterOption> commandParameterOptions =
                    Enumerable.Empty<ICommandParameterOption>();

                Group optionsGroup = parameterGroups["Options"];
                if (optionsGroup.Success)
                {
                    commandParameterOptions = ParseOptions(optionsGroup.Value);
                }

                ICommandParameter commandParameter = new CommandParameter(
                    parameterMatch.Value,
                    parameterName,
                    parameterType,
                    isOptional,
                    commandParameterOptions);

                commandParameters.Add(commandParameter);
            }

            return new CommandFormat(formatSource, commandParameters);
        }

        /// <summary>
        /// Parses command according to provided <paramref name="commandFormat"></paramref>.
        /// </summary>
        /// <param name="commandFormat">Command format.</param>
        /// <param name="source">Command text.</param>
        /// <returns>Command parser result.</returns>
        public ICommandParserResult Parse(ICommandFormat commandFormat, string source)
        {
            string commandPattern = GetCommandPattern(commandFormat);

            Regex commandRegex = new Regex(commandPattern);

            ICollection<ICommandParameterValue> values = new Collection<ICommandParameterValue>();
            ICollection<ICommandParameter> wrongParameters = new Collection<ICommandParameter>();

            Match commandMatch = commandRegex.Match(source);

            GroupCollection commandGroups = commandMatch.Groups;

            bool commandMatchResult = commandMatch.Success;

            foreach (ICommandParameter commandParameter in commandFormat.Parameters)
            {
                Group commandParameterGroup = commandGroups[commandParameter.Name];

                if (!commandParameterGroup.Success && !commandParameter.IsOptional)
                {
                    commandMatchResult = false;
                    wrongParameters.Add(commandParameter);
                    continue;
                }

                string commandParameterValue = commandParameterGroup.Value;

                bool optionsMatchResult = true;

                foreach (ICommandParameterOption commandParameterOption in commandParameter.Options)
                {
                    foreach (ICommandParameterOptionHandler commandParameterOptionHandler in OptionHandlers)
                    {
                        if (string.Compare(commandParameterOption.Name, commandParameterOptionHandler.Name,
                            StringComparison.Ordinal) == 0)
                        {
                            if (!(optionsMatchResult =
                                commandParameterOptionHandler.IsMatch(commandParameterOption.Value,
                                    commandParameterValue)))
                            {
                                break;
                            }
                        }
                    }

                    if (!optionsMatchResult)
                    {
                        break;
                    }
                }

                if (optionsMatchResult)
                {
                    values.Add(new CommandParameterValue(
                        commandParameter,
                        commandParameterGroup.Value));
                }
                else if(!commandParameter.IsOptional)
                {
                    commandMatchResult = false;
                    wrongParameters.Add(commandParameter);
                }
            }

            if (commandMatchResult)
            {
                return CommandParserResult.Success(commandFormat, source, values);
            }

            return CommandParserResult.Fail(commandFormat, source, wrongParameters);
        }

        #endregion
        
        #region Private methods
        
        private IEnumerable<ICommandParameterOption> ParseOptions(string optionsSource)
        {
            Regex optionRegex = new Regex(ParameterOptionPattern);

            string[] options =
                optionsSource.Split(Configuration.OptionsDelimiter, StringSplitOptions.RemoveEmptyEntries);

            foreach (string option in options)
            {
                Match optionMatch = optionRegex.Match(option);

                if (optionMatch.Success)
                {
                    GroupCollection optionGroups = optionMatch.Groups;

                    string name = optionGroups["Name"].Value;
                    string value = optionGroups["Value"].Value;

                    if (!OptionHandlers.Any(optionHandler => string.Compare(optionHandler.Name, name, StringComparison.Ordinal) != 0))
                    {
                        throw new InvalidOperationException($"There is no option handler for '{name}' option.");
                    }
                    
                    yield return new CommandParameterOption(name, value);
                }
                else
                {
                    throw new InvalidOperationException($"Invalid command parameter option format: {option}.");
                }
            }
        }   
        
        private string GetCommandPattern(ICommandFormat commandFormat)
        {
            string commandRegex = commandFormat.Format;

            foreach (ICommandParameter commandParameter in commandFormat.Parameters)
            {
                commandRegex = commandRegex.Replace(commandParameter.Format,
                    string.Format(ParameterRegexTemplatePattern, commandParameter.Name, commandParameter.Type.Pattern) + "?");
            }

            string whitespacePattern = "\\s*";

            if (Configuration.ReplaceWhitespaceWithPattern)
            {
                commandRegex = commandRegex.Replace(" ", whitespacePattern);
            }

            commandRegex = commandRegex + "(.*)";

            commandRegex = $"^{commandRegex}$";
            
            return commandRegex;
        }
        
        private IEnumerable<TKey> FindDuplicates<T, TKey>(IEnumerable<T> enumeration, Func<T, TKey> keySelector)
        {
            return enumeration
                .GroupBy(keySelector)
                .Where(grouping => grouping.Count() > 1)
                .Select(duplicate => duplicate.Key);
        }
        
        #endregion
    }
}