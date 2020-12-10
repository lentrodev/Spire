#region

using System;
using System.Collections.Generic;
using Autofac;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Abstractions.Processing.Contexts;
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
        private readonly IReadOnlyDictionary<string, string> _arguments;

        /// <summary>
        /// Creates new command context with specified arguments and handler context.
        /// </summary>
        /// <param name="arguments">Command arguments.</param>
        /// <param name="handlerContext">Handler context.</param>
        public CommandContext(IReadOnlyDictionary<string, string> arguments, IHandlerContext<TEntity> handlerContext)
            : this(arguments, handlerContext.Update, handlerContext.Entity, handlerContext.Sender,
                handlerContext.BotClient, handlerContext.Container)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Creates new command context with specified arguments, update, entity, update sender, telegram bot client instance and Autofac servic resolving container.
        /// </summary>
        /// <param name="arguments">Command arguments.</param>
        /// <param name="update">Update.</param>
        /// <param name="entity">Update entity.</param>
        /// <param name="sender">Update sender.</param>
        /// <param name="botClient">Telegram bot client.</param>
        /// <param name="container">Autofac service resolving container.</param>
        public CommandContext(IReadOnlyDictionary<string, string> arguments, Update update, TEntity entity, User sender,
            ITelegramBotClient botClient, IContainer container) : base(update, entity, sender, botClient, container)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Gets argument value by the specified argument name.
        /// </summary>
        /// <param name="argumentName">Argument name.</param>
        /// <returns>Argument value.</returns>
        public string GetArgument(string argumentName)
        {
            if (_arguments.ContainsKey(argumentName))
            {
                return _arguments[argumentName];
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets argument value by the specified argument name, converted to specified type.
        /// </summary>
        /// <param name="argumentName">Argument name.</param>
        /// <typeparam name="T">Argument value type.</typeparam>
        /// <returns>Argument typed value.</returns>
        public T GetArgument<T>(string argumentName)
        {
            string argumentValue = GetArgument(argumentName);

            try
            {
                return (T) Convert.ChangeType(argumentValue, typeof(T));
            }
            catch
            {
                return default;
            }
        }
    }
}