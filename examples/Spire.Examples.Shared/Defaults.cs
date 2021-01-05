#region

using System;
using Autofac;
using Spire.Attributes;
using Spire.Core.Abstractions;
using Spire.Core.Abstractions.Builders;
using Spire.Core.Abstractions.Builders.Processing;
using Spire.Core.Builders;
using Spire.Core.Commands;
using Spire.Core.Commands.Abstractions.Builders.Processing;
using Spire.Core.Markups.Inline;
using Spire.Core.Sessions;
using Spire.Examples.Shared.Attributes;
using Spire.Examples.Shared.Commands;
using Spire.Examples.Shared.Handlers;
using Spire.Examples.Shared.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Examples.Shared
{
    public static class Defaults
    {
        public static IBot BuildDefaultBot(string telegramBotClientApiToken)
        {
            ITelegramBotClient telegramBotClient = new TelegramBotClient(telegramBotClientApiToken);

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterSessionManager();
            containerBuilder.RegisterInstance(new Random());
            containerBuilder.RegisterInstance(new FeedbackService()).As<IFeedbackService>();

            InlineKeyboardMarkupBuilder inlineKeyboardMarkupBuilder = InlineKeyboardMarkupBuilder.New;

            containerBuilder.Register((ctx) =>
            {
                inlineKeyboardMarkupBuilder.Clear();
                return inlineKeyboardMarkupBuilder;
            }).InstancePerDependency();

            IContainer container = containerBuilder.Build();

            IBotBuilder botBuilder = new BotBuilder("Spire Test Bot", telegramBotClient, container);

            ConfigureDefaults(botBuilder);

            IBot bot = botBuilder.Build();

            return bot;
        }

        public const string DefaultProcessorId = "MessageProcessor";

        public static IBotBuilder ConfigureDefaults(IBotBuilder botBuilder)
        {
            return botBuilder
                .WithPositionedUpdateEntityProcessorBuilder<Message>(
                    DefaultProcessorId,
                    UpdateType.Message,
                    ConfigureMessageEntityProcessorBuilder);
        }

        private static void ConfigureMessageEntityProcessorBuilder(
            IUpdateEntityProcessorBuilder<Message, PositionedEntityHandlerAttribute> updateEntityProcessorBuilder)
        {
            updateEntityProcessorBuilder
                .WithSessionRequestUpdateEntityHandler(new MessageUpdateEntityHandlerAttribute(1, DefaultProcessorId));

            updateEntityProcessorBuilder
                .WithUpdateEntityHandlersFromType(typeof(SimpleHandlersSource));

            updateEntityProcessorBuilder
                .WithCommandHandler<Message, PositionedEntityHandlerAttribute, MessageCommandHandlerAttribute, MessageCommandHandlerMatcher>(new MessageUpdateEntityHandlerAttribute(
                        5,
                        DefaultProcessorId),
                    ConfigureMessageCommandProcessorBuilder);
        }

        private static void ConfigureMessageCommandProcessorBuilder(
            ICommandProcessorBuilder<Message, MessageCommandHandlerAttribute, MessageCommandHandlerMatcher> messageCommandProcessorBuilder)
        {
            messageCommandProcessorBuilder
                .WithCommandHandlersFromType(typeof(SimpleCommandsSource))
                .WithCommandHandlersFromType(typeof(RequestMessageCommandsSource));
        }
    }
}