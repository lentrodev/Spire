using System;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;
using Spire.Core.Commands.Parsing.Abstractions.Parameters.Options;

namespace Spire.Core.Commands.Parsing.Abstractions
{
    /// <summary>
    /// Base interface for implementing command parser builder.
    /// </summary>
    public interface ICommandParserBuilder
    {
        /// <summary>
        /// Sets default command parser settings.
        /// </summary>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        ICommandParserBuilder WithDefaults();

        /// <summary>
        /// Overrides default settings.
        /// </summary>
        /// <param name="defaultsOverriderConfigurator">Overrider configurator.</param>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        ICommandParserBuilder WithOverridenDefaults(Action<ICommandParserConfiguration> defaultsOverriderConfigurator);

        /// <summary>
        /// Sets command parser configuration. 
        /// </summary>
        /// <param name="commandParserConfiguration">Command parser configuration.</param>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        ICommandParserBuilder WithConfiguration(ICommandParserConfiguration commandParserConfiguration);

        /// <summary>
        /// Adds parameter type.
        /// </summary>
        /// <param name="commandParameterType">Parameter type.</param>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        ICommandParserBuilder WithParameterType(ICommandParameterType commandParameterType);

        /// <summary>
        /// Adds parameter type. 
        /// </summary>
        /// <param name="commandParameterType">Parameter type instance.</param>
        /// <typeparam name="TCommandParameterType">Parameter type.</typeparam>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        ICommandParserBuilder WithParameterType<TCommandParameterType>(TCommandParameterType commandParameterType)
            where TCommandParameterType : class, ICommandParameterType;

        /// <summary>
        /// Adds parameter type.
        /// </summary>
        /// <typeparam name="TCommandParameterType">Parameter type.</typeparam>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        ICommandParserBuilder WithParameterType<TCommandParameterType>()
            where TCommandParameterType : class, ICommandParameterType, new();

        /// <summary>
        /// Adds parameter option handler.
        /// </summary>
        /// <param name="commandParameterOptionHandler">Command parameter option handler.</param>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        ICommandParserBuilder WithParameterOptionHandler(ICommandParameterOptionHandler commandParameterOptionHandler);

        /// <summary>
        /// Adds parameter option handler.
        /// </summary>
        /// <param name="commandParameterOptionHandler">Command parameter option handler.</param>
        /// <typeparam name="TCommandParameterOptionHandler">Command parameter option handler type.</typeparam>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        ICommandParserBuilder WithParameterOptionHandler<TCommandParameterOptionHandler>(
            TCommandParameterOptionHandler commandParameterOptionHandler)
            where TCommandParameterOptionHandler : class, ICommandParameterOptionHandler;

        /// <summary>
        /// Adds parameter option handler.
        /// </summary>
        /// <typeparam name="TCommandParameterOptionHandler">Command parameter option handler type.</typeparam>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        ICommandParserBuilder WithParameterOptionHandler<TCommandParameterOptionHandler>()
            where TCommandParameterOptionHandler : class, ICommandParameterOptionHandler, new();

        /// <summary>
        /// Builds <see cref="ICommandParser"/> with specified settings.
        /// </summary>
        /// <returns>Built <see cref="ICommandParser"/> instance.</returns>
        ICommandParser Build();
    }
}