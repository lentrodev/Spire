#region

using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;
using Spire.Core.Processing.Contexts;
using Telegram.Bot;
using Telegram.Bot.Types;

#endregion

namespace Spire.Core.Commands.Processing.Contexts
{
    /// <summary>
    /// Default implementation of <see cref="ICommandContext{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class CommandContext<TEntity> : HandlerContext<TEntity>, ICommandContext<TEntity>
    {
        private readonly IEnumerable<ICommandParameterValue> _commandParameterValues;
        
        public IEnumerable<ICommandParameterValue> GetParameters()
        {
            return _commandParameterValues;
        }

        /// <summary>
        /// Creates new command context with specified arguments and handler context.
        /// </summary>
        /// <param name="commandParameterValues">Command arguments.</param>
        /// <param name="handlerContext">Handler context.</param>
        public CommandContext(IEnumerable<ICommandParameterValue> commandParameterValues, IHandlerContext<TEntity> handlerContext)
            : this(commandParameterValues, handlerContext.Update, handlerContext.Entity, handlerContext.Sender,
                handlerContext.BotClient, handlerContext.Container)
        {
            _commandParameterValues = commandParameterValues;
        }

        /// <summary>
        /// Creates new command context with specified arguments, update, entity, update sender, telegram bot client instance and Autofac servic resolving container.
        /// </summary>
        /// <param name="commandParameterValues">Command arguments.</param>
        /// <param name="update">Update.</param>
        /// <param name="entity">Update entity.</param>
        /// <param name="sender">Update sender.</param>
        /// <param name="botClient">Telegram bot client.</param>
        /// <param name="container">Autofac service resolving container.</param>
        public CommandContext(IEnumerable<ICommandParameterValue> commandParameterValues, Update update, TEntity entity, User sender,
            ITelegramBotClient botClient, IContainer container) : base(update, entity, sender, botClient, container)
        {
            _commandParameterValues = commandParameterValues;
        }

        private ICommandParameterValue GetParameterValue(string parameterName)
        {
            return _commandParameterValues.FirstOrDefault(parameterValue => string.Compare(parameterValue.Value, parameterName, StringComparison.Ordinal) == 0);
        }
        
        /// <summary>
        /// Gets parameter value by the specified argument name.
        /// </summary>
        /// <param name="parameterName">Parameter name.</param>
        /// <returns>Parameter value.</returns>
        public string GetParameter(string parameterName)
        {
            ICommandParameterValue commandParameterValue = GetParameterValue(parameterName);

            if (commandParameterValue != null)
            {
                return commandParameterValue.Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets parameter value by the specified argument name, converted to specified type.
        /// </summary>
        /// <param name="parameterName">Parameter name.</param>
        /// <typeparam name="T">Parameter value type.</typeparam>
        /// <returns>Parameter typed value.</returns>
        public T GetParameter<T>(string parameterName)
        {
            ICommandParameterValue commandParameterValue = GetParameterValue(parameterName);

            if (commandParameterValue != null)
            {
                return commandParameterValue.As<T>();
            }

            return default;
        }
    }
}