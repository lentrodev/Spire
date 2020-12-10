#region

using Spire.Core.Commands.Abstractions.Processing.Attributes;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Examples.Shared.Attributes
{
    public class MessageCommandHandlerAttribute : CommandHandlerAttributeBase
    {
        public string CommandFormat { get; }

        public MessageCommandHandlerAttribute(string commandFormat, string id) : base(id, UpdateType.Message)
        {
            CommandFormat = commandFormat;
        }
    }
}