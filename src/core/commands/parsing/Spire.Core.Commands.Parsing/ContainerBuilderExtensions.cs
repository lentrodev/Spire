using System;
using Autofac;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Parsing.Abstractions;

namespace Spire.Core.Commands.Parsing
{
    public static class ContainerBuilderExtensions
    {
        public static ICommandParser CommandParser<TEntity>(this IHandlerContext<TEntity> handlerContext)
        {
            return handlerContext.Container.Resolve<ICommandParser>();
        }
        
        public static ContainerBuilder RegisterCommandParser(this ContainerBuilder containerBuilder,
            Action<IComponentContext, ICommandParserBuilder> configureCommandParserBuilder)
        {
            if (configureCommandParserBuilder == null)
            {
                throw new ArgumentNullException(nameof(configureCommandParserBuilder));
            }

            containerBuilder.Register(ctx =>
            {
                ICommandParserBuilder commandParserBuilder = CommandParserBuilder.New;

                configureCommandParserBuilder(ctx, commandParserBuilder);

                return commandParserBuilder.Build();
            }).As<ICommandParser>();

            return containerBuilder;
        }
    }
}