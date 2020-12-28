#region

using System.Threading.Tasks;
using Autofac;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Abstractions.Processing;
using Spire.Core.Commands.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Parsing;
using Spire.Core.Commands.Parsing.Abstractions;
using Spire.Core.Commands.Parsing.Parameters.Options;
using Spire.Core.Commands.Processing;
using Spire.Core.Commands.Processing.Contexts;
using Spire.Examples.Shared.Attributes;
using Telegram.Bot.Types;

#endregion

namespace Spire.Examples.Shared
{
    public class MessageCommandHandlerMatcher : ICommandHandlerMatcher<Message, MessageCommandHandlerAttribute>
    {
        private readonly ICommandParser _commandParser;

        public MessageCommandHandlerMatcher()
        {
            ICommandParserBuilder commandParserBuilder = CommandParserBuilder.New;

            commandParserBuilder.WithDefaults();

            commandParserBuilder.WithParameterOptionHandler(new CommandParameterOptionHandler<int, int>(
                "minValue",
                (min, value) => value >= min));

            commandParserBuilder.WithParameterOptionHandler(new CommandParameterOptionHandler<int, int>(
                "maxValue",
                (max, value) => value <= max));

            _commandParser = commandParserBuilder.Build();
        }

        public ValueTask<ICommandHandlerMatchingResult<Message>> CanHandle(IHandlerContext<Message> handlerContext,
            ICommandHandlerDescriptor<MessageCommandHandlerAttribute> commandHandlerDescriptor,
            IContainer serviceContainer)
        {
            ICommandFormat commandFormat =
                _commandParser.ParseCommandFormat(commandHandlerDescriptor.Attribute.CommandFormat);

            ICommandParserResult commandParserResult = _commandParser.Parse(commandFormat, handlerContext.Entity.Text);

            if (commandParserResult.IsSuccess)
            {
                ICommandContext<Message> messageCommandContext = new CommandContext<Message>(
                    commandParserResult.Values,
                    handlerContext);

                return new ValueTask<ICommandHandlerMatchingResult<Message>>(
                    CommandHandlerMatchingResult<Message>.Success(messageCommandContext));
            }

            return new ValueTask<ICommandHandlerMatchingResult<Message>>(
                CommandHandlerMatchingResult<Message>.Fail());
        }
    }
}