#region

using System.Threading.Tasks;
using Autofac;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Abstractions.Processing;
using Spire.Core.Commands.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Parsing;
using Spire.Core.Commands.Parsing.Abstractions;
using Spire.Core.Commands.Processing;
using Spire.Core.Commands.Processing.Contexts;
using Spire.Examples.Shared.Attributes;
using Telegram.Bot.Types;

#endregion

namespace Spire.Examples.Shared
{
    public class MessageCommandHandlerMatcher : ICommandHandlerMatcher<Message, MessageCommandHandlerAttribute>
    {
        private readonly ICommandParserBuilder _commandParserBuilder;

        public MessageCommandHandlerMatcher()
        {
            ICommandParserBuilder commandParserBuilder = new CommandParserBuilder();

            commandParserBuilder.WithDefaults();

            commandParserBuilder.WithOptionHandler("minValue",
                (argumentValue, optionValue) =>
                {
                    var res = int.TryParse(optionValue, out int minLength) &&
                              int.TryParse(argumentValue, out int value) &&
                              value >= minLength;
                    return res;
                });

            commandParserBuilder.WithOptionHandler("maxValue",
                (argumentValue, optionValue) =>
                {
                    var res = int.TryParse(optionValue, out int maxLength) &&
                              int.TryParse(argumentValue, out int value) &&
                              value <= maxLength;
                    return res;
                });

            _commandParserBuilder = commandParserBuilder;
        }

        public ValueTask<ICommandHandlerMatchingResult<Message>> CanHandle(IHandlerContext<Message> handlerContext,
            ICommandHandlerDescriptor<MessageCommandHandlerAttribute> commandHandlerDescriptor,
            IContainer serviceContainer)
        {
            ICommandParser commandParser = _commandParserBuilder
                .WithCommandFormat(commandHandlerDescriptor.Attribute.CommandFormat)
                .Build();

            ICommandParserResult commandParserResult = commandParser.ParseCommand(handlerContext.Entity.Text);

            if (commandParserResult.Successful)
            {
                ICommandContext<Message> messageCommandContext = new CommandContext<Message>(
                    commandParserResult.Variables,
                    handlerContext);

                return new ValueTask<ICommandHandlerMatchingResult<Message>>(
                    CommandHandlerMatchingResult<Message>.Success(messageCommandContext));
            }
            else
                return new ValueTask<ICommandHandlerMatchingResult<Message>>(
                    CommandHandlerMatchingResult<Message>.Fail());
        }
    }
}