#region

using System;
using System.Collections.Generic;
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
        public static IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> WithCommandHandler
        <
            TEntity,
            TUpdateEntityHandlerAttribute,
            TCommandHandlerAttribute,
            TCommandHandlerMatcher>
        (
            this IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> updateEntityProcessorBuilder,
            TUpdateEntityHandlerAttribute entityHandlerAttribute,
            Action<ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher>>
                configureCommandProcessorBuilder
        )
            where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
            where TCommandHandlerAttribute : CommandHandlerAttributeBase
            where TCommandHandlerMatcher : class, ICommandHandlerMatcher<TEntity, TCommandHandlerAttribute>
        {
            ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher> commandProcessorBuilder
                = new CommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher>(
                    updateEntityProcessorBuilder.ProcessorId,
                    updateEntityProcessorBuilder.EntityType,
                    updateEntityProcessorBuilder.Container);

            if (configureCommandProcessorBuilder == null)
            {
                throw new ArgumentNullException(nameof(configureCommandProcessorBuilder));
            }

            configureCommandProcessorBuilder(commandProcessorBuilder);

            return WithCommandHandler(updateEntityProcessorBuilder,
                entityHandlerAttribute,
                commandProcessorBuilder);
        }
        
        
        public static IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> WithCommandHandler
        <
            TEntity,
            TUpdateEntityHandlerAttribute,
            TCommandHandlerAttribute,
            TCommandHandlerMatcher>
        (
            this IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> updateEntityProcessorBuilder,
            TUpdateEntityHandlerAttribute entityHandlerAttribute,
            TCommandHandlerMatcher commandHandlerMatcher,
            Action<ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher>>
                configureCommandProcessorBuilder
        )
            where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
            where TCommandHandlerAttribute : CommandHandlerAttributeBase
            where TCommandHandlerMatcher : class, ICommandHandlerMatcher<TEntity, TCommandHandlerAttribute>
        {
            ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher> commandProcessorBuilder
                = new CommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher>(
                    updateEntityProcessorBuilder.ProcessorId,
                    updateEntityProcessorBuilder.EntityType,
                    commandHandlerMatcher,
                    updateEntityProcessorBuilder.Container);

            if (configureCommandProcessorBuilder == null)
            {
                throw new ArgumentNullException(nameof(configureCommandProcessorBuilder));
            }

            configureCommandProcessorBuilder(commandProcessorBuilder);
            
            return WithCommandHandler(updateEntityProcessorBuilder,
                entityHandlerAttribute,
                commandProcessorBuilder);
        }
        
        public static IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> WithCommandHandler
        <
            TEntity,
            TUpdateEntityHandlerAttribute,
            TCommandHandlerAttribute,
            TCommandHandlerMatcher>
        (
            this IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> updateEntityProcessorBuilder,
            TUpdateEntityHandlerAttribute entityHandlerAttribute,
            Func<IEnumerable<IActivatedCommandHandlerDescriptor<TCommandHandlerAttribute>>, TCommandHandlerMatcher> commandHandlerMatcherResolver,
            Action<ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher>>
                configureCommandProcessorBuilder
        )
            where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
            where TCommandHandlerAttribute : CommandHandlerAttributeBase
            where TCommandHandlerMatcher : class, ICommandHandlerMatcher<TEntity, TCommandHandlerAttribute>
        {
            ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher> commandProcessorBuilder
                = new CommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher>(
                    updateEntityProcessorBuilder.ProcessorId,
                    updateEntityProcessorBuilder.EntityType,
                    commandHandlerMatcherResolver,
                    updateEntityProcessorBuilder.Container);

            if (configureCommandProcessorBuilder == null)
            {
                throw new ArgumentNullException(nameof(configureCommandProcessorBuilder));
            }

            configureCommandProcessorBuilder(commandProcessorBuilder);
            
            return WithCommandHandler(updateEntityProcessorBuilder,
                entityHandlerAttribute,
                commandProcessorBuilder);
        }
        
        
        public static IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> WithCommandHandler
        <
            TEntity,
            TUpdateEntityHandlerAttribute,
            TCommandHandlerAttribute,
            TCommandHandlerMatcher>
        (
            this IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> updateEntityProcessorBuilder,
            TUpdateEntityHandlerAttribute entityHandlerAttribute,
            ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher> commandProcessorBuilder
        )
            where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
            where TCommandHandlerAttribute : CommandHandlerAttributeBase
            where TCommandHandlerMatcher : class, ICommandHandlerMatcher<TEntity, TCommandHandlerAttribute>
        {
            ICommandProcessor<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher> commandProcessor = commandProcessorBuilder.Build();

            Func<IHandlerContext<TEntity>, ValueTask<bool>> commandProcessorInvocationFunc =
                async handlerContext
                    => !await commandProcessor.Process(handlerContext, updateEntityProcessorBuilder.Container);

            return updateEntityProcessorBuilder.WithHandlerDescriptor(
                new FuncUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>(
                    Guid.NewGuid(),
                    commandProcessorInvocationFunc,
                    entityHandlerAttribute));
        }
    }
}


// ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute> commandProcessorBuilder =
//     new CommandProcessorBuilder<TEntity, TCommandHandlerAttribute, TCommandHandlerMatcher>(updateEntityProcessorBuilder.ProcessorId,
//         updateEntityProcessorBuilder.EntityType, 
//         commandHandlerMatcherResolver,
//         updateEntityProcessorBuilder.Container);
//
// configureCommandProcessor(commandProcessorBuilder);
//
// ICommandProcessor<TEntity, TCommandHandlerAttribute> commandProcessor = commandProcessorBuilder.Build();
//
// Func<IHandlerContext<TEntity>, ValueTask<bool>> commandProcessorInvocationFunc =
//     async handlerContext 
//         => !await commandProcessor.Process(handlerContext, updateEntityProcessorBuilder.Container);
//
// return updateEntityProcessorBuilder.WithHandlerDescriptor(
//     new FuncUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>(
//         Guid.NewGuid(),
//         commandProcessorInvocationFunc,
//         entityHandlerAttribute