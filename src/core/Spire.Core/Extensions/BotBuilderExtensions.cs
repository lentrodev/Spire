#region

using System;
using Spire.Core.Abstractions.Builders;
using Spire.Core.Abstractions.Builders.Processing;
using Spire.Core.Abstractions.Processing;
using Spire.Core.Abstractions.Processing.Attributes;
using Spire.Core.Builders.Processing;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Extensions
{
    public static class BotBuilderExtensions
    {
        public static IBotBuilder WithUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute>(
            this IBotBuilder builder,
            string id, UpdateType entityType,
            IEntityHandlersOrchestrator<TUpdateEntityHandlerAttribute> entityHandlerOrchestrator,
            Action<IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute>>
                configureUpdateEntityProcessorBuilder)
            where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
        {
            IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> updateEntityProcessorBuilder =
                new UpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute>(
                    id, entityType, builder.ServiceContainer, entityHandlerOrchestrator);

            configureUpdateEntityProcessorBuilder?.Invoke(updateEntityProcessorBuilder);

            return builder.WithUpdateEntityProcessor(updateEntityProcessorBuilder.Build());
        }
    }
}