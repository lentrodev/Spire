#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Spire.Core.Abstractions;
using Spire.Core.Abstractions.Processing;
using Spire.Core.Abstractions.Processing.Attributes;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Abstractions.Processing.Results;
using Spire.Core.Extensions;
using Spire.Core.Processing.Contexts;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core
{
    /// <summary>
    /// Default implementation of <see cref="IBot"/>.
    /// </summary>
    public class Bot : IBot
    {
        /// <summary>
        /// Common information about the bot.
        /// </summary>
        public BotDescriptor Descriptor { get; }

        /// <summary>
        /// Configured services, built by <see cref="ContainerBuilder.Build"/> represented as <see cref="Autofac"/> <see cref="IContainer"/>.
        /// </summary>
        public IContainer ServiceContainer { get; }

        /// <summary>
        /// Telegram bot client.
        /// </summary>
        public ITelegramBotClient BotClient { get; }

        /// <summary>
        /// Collection of update entity processors.
        /// </summary>
        public IEnumerable<IUpdateEntityProcessor> UpdateEntityProcessors { get; }

        /// <summary>
        /// Collection of update entity processors, grouped by entity type.
        /// </summary>
        private readonly Dictionary<UpdateType, IUpdateEntityProcessor> _keyedEntityProcessors;

        private readonly MethodInfo _processInternalMethodInfo;
        private readonly MethodInfo _processInternalTypedMethodInfo;

        /// <summary>
        /// Creates new <see cref="Bot"/> with specified descriptor, telegram bot client, service container, and update entity processors collection.
        /// </summary>
        /// <param name="descriptor">Common information about the bot.</param>
        /// <param name="telegramBotClient">Telegram bot client.</param>
        /// <param name="serviceContainer">Configured services, built by <see cref="ContainerBuilder.Build"/> represented as <see cref="Autofac"/> <see cref="IContainer"/>.</param>
        /// <param name="updateEntityProcessors">Collection of update entity processors.</param>
        public Bot(
            BotDescriptor descriptor,
            ITelegramBotClient telegramBotClient,
            IContainer serviceContainer,
            IEnumerable<IUpdateEntityProcessor> updateEntityProcessors
        )
        {
            Descriptor = descriptor;
            BotClient = telegramBotClient;
            ServiceContainer = serviceContainer;
            UpdateEntityProcessors = updateEntityProcessors.ToList().AsEnumerable();

            _keyedEntityProcessors = UpdateEntityProcessors.ToDictionary(
                updateEntityProcessor => updateEntityProcessor.EntityType,
                updateEntityProcessor => updateEntityProcessor);

            _processInternalMethodInfo = this.GetType()
                .GetMethod(nameof(ProcessInternal), BindingFlags.NonPublic | BindingFlags.Instance);
            _processInternalTypedMethodInfo = this.GetType()
                .GetMethod(nameof(ProcessInternalTyped), BindingFlags.NonPublic | BindingFlags.Instance);
        }

        /// <summary>
        /// Asynchronously processes an <see cref="Update"/>.
        /// </summary>
        /// <param name="update"><see cref="Update"/> to process.</param>
        /// <returns><see cref="Update"/> processing result.</returns>
        public async ValueTask<IUpdateEntityProcessingResult> Process(Update update)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            object updateEntity = update.GetUpdateEntity();

            Type updateEntityType = updateEntity.GetType();

            IUpdateEntityProcessingResult processingResult = await _processInternalMethodInfo
                .MakeGenericMethod(updateEntityType)
                .InvokeAsyncSafe<IUpdateEntityProcessingResult>(this, update, updateEntity);

            return processingResult;
        }

        private async ValueTask<IUpdateEntityProcessingResult> ProcessInternal<TEntity>(Update update, TEntity entity)
        {
            if (!_keyedEntityProcessors.ContainsKey(update.Type))
            {
                throw new NotSupportedException(
                    $"Update entity processor for {update.Type} update type isn't registered.");
            }

            IUpdateEntityProcessor updateEntityProcessor = _keyedEntityProcessors[update.Type];

            Type attributeType = updateEntityProcessor.GetType().GetGenericArguments().ElementAt(1);

            IUpdateEntityProcessingResult processingResult = await _processInternalTypedMethodInfo
                .MakeGenericMethod(typeof(TEntity), attributeType)
                .InvokeAsyncSafe<IUpdateEntityProcessingResult>(this, update, entity, updateEntityProcessor);

            return processingResult;
        }


        private async ValueTask<IUpdateEntityProcessingResult> ProcessInternalTyped<TEntity,
            TUpdateEntityHandlerAttribute>(Update update, TEntity entity, IUpdateEntityProcessor updateEntityProcessor)
            where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
        {
            if (updateEntityProcessor is IUpdateEntityProcessor<TEntity, TUpdateEntityHandlerAttribute>
                typedUpdateEntityProcessor)
            {
                IHandlerContext<TEntity> handlerContext =
                    new HandlerContext<TEntity>(update, entity, update.GetUpdateEntitySender(), BotClient,
                        ServiceContainer);

                return await typedUpdateEntityProcessor.Process(handlerContext, ServiceContainer);
            }

            return default;
        }
    }
}