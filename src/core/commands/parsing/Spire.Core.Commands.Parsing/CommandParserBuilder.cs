using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Spire.Core.Commands.Parsing.Abstractions;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;
using Spire.Core.Commands.Parsing.Abstractions.Parameters.Options;
using Spire.Core.Commands.Parsing.Parameters;

namespace Spire.Core.Commands.Parsing
{
    /// <summary>
    /// Default implementation of <see cref="ICommandParserBuilder"/>.
    /// </summary>
    public class CommandParserBuilder : ICommandParserBuilder
    {
        /// <summary>
        /// Creates new <see cref="ICommandParserBuilder"/>.
        /// </summary>
        public static ICommandParserBuilder New => new CommandParserBuilder(); 
        
        private readonly ICollection<ICommandParameterType> _commandParameterTypes;
        private readonly ICollection<ICommandParameterOptionHandler> _commandParameterOptionHandlers;
        private ICommandParserConfiguration _commandParserConfiguration;
        
        private CommandParserBuilder()
        {
            _commandParameterTypes = new Collection<ICommandParameterType>();
            _commandParameterOptionHandlers = new Collection<ICommandParameterOptionHandler>();
        }
        
        /// <summary>
        /// Sets default command parser settings.
        /// </summary>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        public ICommandParserBuilder WithDefaults()
        {
            _commandParserConfiguration = CommandParserConfiguration.Default;
            
            _commandParameterTypes.Clear();
            
            foreach (ICommandParameterType commandParameterType in CommandParameterTypes.All)
            {
                _commandParameterTypes.Add(commandParameterType);
            }
            
            return this;
        }

        /// <summary>
        /// Overrides default settings.
        /// </summary>
        /// <param name="defaultsOverriderConfigurator">Overrider configurator.</param>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        public ICommandParserBuilder WithOverridenDefaults(Action<ICommandParserConfiguration> defaultsOverriderConfigurator)
        {
            _commandParserConfiguration ??= CommandParserConfiguration.Default;

            if (defaultsOverriderConfigurator == null)
            {
                throw new ArgumentNullException(nameof(defaultsOverriderConfigurator));
            }
            
            defaultsOverriderConfigurator(_commandParserConfiguration);

            return this;
        }

        /// <summary>
        /// Sets command parser configuration. 
        /// </summary>
        /// <param name="commandParserConfiguration">Command parser configuration.</param>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        public ICommandParserBuilder WithConfiguration(ICommandParserConfiguration commandParserConfiguration)
        {
            _commandParserConfiguration = commandParserConfiguration ?? throw new ArgumentNullException(nameof(commandParserConfiguration));

            return this;
        }

        /// <summary>
        /// Adds parameter type.
        /// </summary>
        /// <param name="commandParameterType">Parameter type.</param>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        public ICommandParserBuilder WithParameterType(ICommandParameterType commandParameterType)
        {
            if (commandParameterType == null)
            {
                throw new ArgumentNullException(nameof(commandParameterType));
            }

            if (_commandParameterTypes.Any(existingCommandParameterType =>
                string.Compare(existingCommandParameterType.Name, commandParameterType.Name, StringComparison.Ordinal) == 0))
            {
                throw new ArgumentException(
                    $"Specified command parameter type '{commandParameterType.Name}' already exists.", nameof(commandParameterType));
            }
            
            _commandParameterTypes.Add(commandParameterType);

            return this;
        }

        /// <summary>
        /// Adds parameter type. 
        /// </summary>
        /// <param name="commandParameterType">Parameter type instance.</param>
        /// <typeparam name="TCommandParameterType">Parameter type.</typeparam>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        public ICommandParserBuilder WithParameterType<TCommandParameterType>(TCommandParameterType commandParameterType) where TCommandParameterType : class, ICommandParameterType
        {
            return WithParameterType(commandParameterType as ICommandParameterType);
        }

        /// <summary>
        /// Adds parameter type.
        /// </summary>
        /// <typeparam name="TCommandParameterType">Parameter type.</typeparam>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        public ICommandParserBuilder WithParameterType<TCommandParameterType>() where TCommandParameterType : class, ICommandParameterType, new()
        {
            return WithParameterType(new TCommandParameterType());
        }

        /// <summary>
        /// Adds parameter option handler.
        /// </summary>
        /// <param name="commandParameterOptionHandler">Command parameter option handler.</param>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        public ICommandParserBuilder WithParameterOptionHandler(ICommandParameterOptionHandler commandParameterOptionHandler)
        {
            if (commandParameterOptionHandler == null)
            {
                throw new ArgumentNullException(nameof(commandParameterOptionHandler));
            }
            
            if(_commandParameterOptionHandlers.Any(existingCommandParameterHandler => string.Compare(existingCommandParameterHandler.Name, commandParameterOptionHandler.Name, StringComparison.Ordinal) == 0))
            {
                throw new ArgumentException(
                    $"Specified command parameter option handler '{commandParameterOptionHandler.Name}' already exists.", nameof(commandParameterOptionHandler));
            }
            
            _commandParameterOptionHandlers.Add(commandParameterOptionHandler);

            return this;
        }

        /// <summary>
        /// Adds parameter option handler.
        /// </summary>
        /// <param name="commandParameterOptionHandler">Command parameter option handler.</param>
        /// <typeparam name="TCommandParameterOptionHandler">Command parameter option handler type.</typeparam>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        public ICommandParserBuilder WithParameterOptionHandler<TCommandParameterOptionHandler>(
            TCommandParameterOptionHandler commandParameterOptionHandler) where TCommandParameterOptionHandler : class, ICommandParameterOptionHandler
        {
            return WithParameterOptionHandler(commandParameterOptionHandler as ICommandParameterOptionHandler);
        }

        /// <summary>
        /// Adds parameter option handler.
        /// </summary>
        /// <typeparam name="TCommandParameterOptionHandler">Command parameter option handler type.</typeparam>
        /// <returns>Configured <see cref="ICommandParserBuilder"/> instance.</returns>
        public ICommandParserBuilder WithParameterOptionHandler<TCommandParameterOptionHandler>() where TCommandParameterOptionHandler : class, ICommandParameterOptionHandler, new()
        {
            return WithParameterOptionHandler(new TCommandParameterOptionHandler());
        }

        /// <summary>
        /// Builds <see cref="ICommandParser"/> with specified settings.
        /// </summary>
        /// <returns>Built <see cref="ICommandParser"/> instance.</returns>
        public ICommandParser Build()
        {
            if (_commandParserConfiguration == null && !_commandParameterTypes.Any())
            {
                WithDefaults();
            }
            
            return new CommandParser(_commandParserConfiguration, _commandParameterTypes,
                _commandParameterOptionHandlers);
        }
    }
}