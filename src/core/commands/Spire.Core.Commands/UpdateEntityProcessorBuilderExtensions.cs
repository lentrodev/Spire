#region

using System;
using System.Threading.Tasks;
using Spire.Core.Abstractions.Builders.Processing;
using Spire.Core.Abstractions.Processing.Attributes;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Abstractions.Builders.Processing;
using Spire.Core.Commands.Abstractions.Processing;
using Spire.Core.Commands.Abstractions.Processing.Attributes;
using Spire.Core.Commands.Builders.Processing;
using Spire.Core.Processing;

#endregion

namespace Spire.Core.Commands
{
    public static class UpdateEntityProcessorBuilderExtensions
    {
        public static IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> WithCommandHandler<TEntity,
            TUpdateEntityHandlerAttribute, TCommandHandlerAttribute>(
            this IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> updateEntityProcessorBuilder,
            ICommandHandlerMatcher<TEntity, TCommandHandlerAttribute> commandHandlerMatcher,
            TUpdateEntityHandlerAttribute entityHandlerAttribute,
            Action<ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute>> configureCommandProcessor
        )
            where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
            where TCommandHandlerAttribute : CommandHandlerAttributeBase
        {
            ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute> commandProcessorBuilder =
                new CommandProcessorBuilder<TEntity, TCommandHandlerAttribute>(updateEntityProcessorBuilder.ProcessorId,
                    updateEntityProcessorBuilder.EntityType, commandHandlerMatcher,
                    updateEntityProcessorBuilder.Container);

            configureCommandProcessor(commandProcessorBuilder);

            ICommandProcessor<TEntity, TCommandHandlerAttribute> commandProcessor = commandProcessorBuilder.Build();

            Func<IHandlerContext<TEntity>, ValueTask<bool>> commandProcessorInvocationFunc =
                new Func<IHandlerContext<TEntity>, ValueTask<bool>>(
                    async (handlerContext) =>
                        !await commandProcessor.Process(handlerContext, updateEntityProcessorBuilder.Container));


            return updateEntityProcessorBuilder.WithHandlerDescriptor(
                new FuncUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>(
                    Guid.NewGuid(),
                    commandProcessorInvocationFunc,
                    entityHandlerAttribute));
        }
    }
}