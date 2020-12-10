#region

using System;
using System.Collections.Generic;
using Spire.Core.Commands.Parsing.Abstractions;
using Spire.Core.Commands.Parsing.VariableTypes;

#endregion

namespace Spire.Core.Commands.Parsing
{
    /// <summary>
    /// Default implementation of <see cref="ICommandParserBuilder"/>.
    /// </summary>
    public class CommandParserBuilder : ICommandParserBuilder
    {
        private readonly IDictionary<string, IVariableType> _variableTypes =
            new Dictionary<string, IVariableType>();

        private string _variableStartChar;
        private string _variableEndChar;
        private string _commandFormat;

        private readonly Dictionary<string, Func<string, string, bool>> _optionsHandlers;

        private IVariableType _defaultVariableType;

        /// <summary>
        /// Creates new command parser builder.
        /// </summary>
        public CommandParserBuilder()
        {
            _optionsHandlers = new Dictionary<string, Func<string, string, bool>>();
        }

        /// <summary>
        /// Sets default variable type.
        /// </summary>
        /// <typeparam name="TVariableType">Variable type.</typeparam>
        /// <returns>Configured command parser builder instance.</returns>
        public ICommandParserBuilder WithDefaultType<TVariableType>()
            where TVariableType : IVariableType, new()
        {
            var variableType = Activator.CreateInstance<TVariableType>();
            return WithDefaultType(variableType);
        }

        /// <summary>
        /// Sets default variable type. 
        /// </summary>
        /// <param name="variableType"></param>
        /// <returns>Configured command parser builder instance.</returns>
        public ICommandParserBuilder WithDefaultType(IVariableType variableType)
        {
            _defaultVariableType = variableType ?? throw new ArgumentNullException(nameof(variableType));
            return this;
        }

        /// <summary>
        /// Sets default variable type.
        /// </summary>
        /// <param name="name">Variable type name.</param>
        /// <returns>Configured command parser builder instance.</returns>
        public ICommandParserBuilder WithDefaultType(string name)
        {
            if (!_variableTypes.ContainsKey(name))
            {
                throw new ArgumentOutOfRangeException(nameof(name), name, $"No variable type named '{name}'");
            }

            _defaultVariableType = _variableTypes[name];
            return this;
        }

        /// <summary>
        /// Add new variable type.
        /// </summary>
        /// <typeparam name="TVariableType"></typeparam>
        /// <returns>Configured command parser builder instance.</returns>
        public ICommandParserBuilder WithVariableType<TVariableType>()
            where TVariableType : IVariableType, new()
        {
            var variableType = Activator.CreateInstance<TVariableType>();
            return WithVariableType(variableType);
        }

        /// <summary>
        /// Add new variable type. 
        /// </summary>
        /// <param name="variableType"></param>
        /// <returns>Configured command parser builder instance.</returns>
        public ICommandParserBuilder WithVariableType(IVariableType variableType)
        {
            if (variableType == null)
            {
                throw new ArgumentNullException(nameof(variableType));
            }

            _variableTypes[variableType.Name] = variableType;
            return this;
        }

        /// <summary>
        /// Sets variable delimiters.
        /// </summary>
        /// <param name="start">Start delimiter.</param>
        /// <param name="end">End delimiter.</param>
        /// <returns>Configured command parser builder instance.</returns>
        public ICommandParserBuilder WithVariableDelimiters(string start, string end)
        {
            _variableStartChar = start;
            _variableEndChar = end;
            return this;
        }

        /// <summary>
        /// Sets default values for the builder.
        /// </summary>
        /// <returns>Configured command parser builder instance.</returns>
        public ICommandParserBuilder WithDefaults()
        {
            return WithVariableDelimiters("{", "}")
                .WithVariableType<StringVariableType>()
                .WithVariableType<NumberVariableType>()
                .WithVariableType<DoubleVariableType>()
                .WithVariableType<BoolVariableType>()
                .WithDefaultType("string");
        }

        /// <summary>
        /// Sets command format.
        /// </summary>
        /// <param name="commandFormat">Command format.</param>
        /// <returns>Configured command parser builder instance.</returns>
        public ICommandParserBuilder WithCommandFormat(string commandFormat)
        {
            if (string.IsNullOrWhiteSpace(commandFormat))
            {
                throw new ArgumentNullException(nameof(commandFormat));
            }

            _commandFormat = commandFormat;
            return this;
        }

        /// <summary>
        /// Adds variable option handler.
        /// </summary>
        /// <param name="optionName">Option name.</param>
        /// <param name="optionHandler">Option handler func.</param>
        /// <returns>Configured command parser builder instance.</returns>
        public ICommandParserBuilder WithOptionHandler(string optionName, Func<string, string, bool> optionHandler)
        {
            _optionsHandlers.Add(optionName, optionHandler);

            return this;
        }

        /// <summary>
        /// Builds new <see cref="CommandParser"/>.
        /// </summary>
        /// <returns>Built <see cref="CommandParser"/>.</returns>
        /// <exception cref="InvalidOperationException">Some required values aren't set.</exception>
        public ICommandParser Build()
        {
            if (string.IsNullOrWhiteSpace(_commandFormat))
            {
                throw new InvalidOperationException("Command format is not set");
            }

            if (string.IsNullOrWhiteSpace(_variableStartChar))
            {
                throw new InvalidOperationException("Start variable character is not set");
            }

            if (string.IsNullOrWhiteSpace(_variableStartChar))
            {
                throw new InvalidOperationException("End variable character is not set");
            }

            if (_variableTypes.Count == 0)
            {
                throw new InvalidOperationException("Variable types is empty");
            }

            if (_defaultVariableType == null)
            {
                throw new InvalidOperationException("Default variable type is not set");
            }

            var variableTypes = new Dictionary<string, IVariableType>();

            foreach (var variableType in _variableTypes)
            {
                if (variableType.Value == null)
                {
                    throw new InvalidOperationException($"'{variableType}' variable type is not set");
                }

                variableTypes[variableType.Key] = variableType.Value;
            }

            return new CommandParser(
                _commandFormat,
                _variableStartChar,
                _variableEndChar,
                variableTypes,
                _optionsHandlers,
                _defaultVariableType
            );
        }
    }
}