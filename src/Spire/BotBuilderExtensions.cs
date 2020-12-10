#region

using System;
using Spire.Attributes;
using Spire.Core.Abstractions.Builders;
using Spire.Core.Abstractions.Builders.Processing;
using Spire.Core.Builders.Processing;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire
{
    public static class BotBuilderExtensions
    {
        public static IBotBuilder WithPositionedUpdateEntityProcessorBuilder<TEntity>(this IBotBuilder botBuilder,
            string id, UpdateType entityType,
            Action<IUpdateEntityProcessorBuilder<TEntity, PositionedEntityHandlerAttribute>>
                configurePositionedUpdateEntityProcessorBuilder)
        {
            IUpdateEntityProcessorBuilder<TEntity, PositionedEntityHandlerAttribute> updateEntityProcessorBuilder
                = new UpdateEntityProcessorBuilder<TEntity, PositionedEntityHandlerAttribute>(id, entityType,
                    botBuilder.ServiceContainer, new PositionedEntityHandlersDescriptorsOrchestrator());

            configurePositionedUpdateEntityProcessorBuilder?.Invoke(updateEntityProcessorBuilder);

            return botBuilder.WithUpdateEntityProcessor(updateEntityProcessorBuilder.Build());
        }
    }
}