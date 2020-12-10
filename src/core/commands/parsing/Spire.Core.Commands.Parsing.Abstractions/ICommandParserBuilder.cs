#region

using System;
using Spire.Core.Abstractions.Builders;

#endregion

namespace Spire.Core.Commands.Parsing.Abstractions
{
    /// <summary>
    /// Base interface for implementing command parser builder.
    /// </summary>
    public interface ICommandParserBuilder : IBuilder<ICommandParser>
    {
        /// <summary>
        /// Sets default variable type.
        /// </summary>
        /// <typeparam name="TVariableType">Variable type.</typeparam>
        /// <returns>Configured command parser builder instance.</returns>
        ICommandParserBuilder WithDefaultType<TVariableType>() where TVariableType : IVariableType, new();

        /// <summary>
        /// Sets default variable type. 
        /// </summary>
        /// <param name="variableType"></param>
        /// <returns>Configured command parser builder instance.</returns>
        ICommandParserBuilder WithDefaultType(IVariableType variableType);

        /// <summary>
        /// Sets default variable type.
        /// </summary>
        /// <param name="name">Variable type name.</param>
        /// <returns>Configured command parser builder instance.</returns>
        ICommandParserBuilder WithDefaultType(string name);

        /// <summary>
        /// Add new variable type.
        /// </summary>
        /// <typeparam name="TVariableType"></typeparam>
        /// <returns>Configured command parser builder instance.</returns>
        ICommandParserBuilder WithVariableType<TVariableType>()
            where TVariableType : IVariableType, new();

        /// <summary>
        /// Add new variable type. 
        /// </summary>
        /// <param name="variableType"></param>
        /// <returns>Configured command parser builder instance.</returns>
        ICommandParserBuilder WithVariableType(IVariableType variableType);

        /// <summary>
        /// Sets variable delimiters.
        /// </summary>
        /// <param name="start">Start delimiter.</param>
        /// <param name="end">End delimiter.</param>
        /// <returns>Configured command parser builder instance.</returns>
        ICommandParserBuilder WithVariableDelimiters(string start, string end);

        /// <summary>
        /// Sets default values for the builder.
        /// </summary>
        /// <returns>Configured command parser builder instance.</returns>
        ICommandParserBuilder WithDefaults();

        /// <summary>
        /// Sets command format.
        /// </summary>
        /// <param name="commandFormat">Command format.</param>
        /// <returns>Configured command parser builder instance.</returns>
        ICommandParserBuilder WithCommandFormat(string commandFormat);

        /// <summary>
        /// Adds variable option handler.
        /// </summary>
        /// <param name="optionName">Option name.</param>
        /// <param name="optionHandler">Option handler func.</param>
        /// <returns>Configured command parser builder instance.</returns>
        ICommandParserBuilder WithOptionHandler(string optionName, Func<string, string, bool> optionHandler);
    }
}