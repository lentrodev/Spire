#region

using Spire.Attributes;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Examples.Shared.Attributes
{
    public class MessageUpdateEntityHandlerAttribute : PositionedEntityHandlerAttribute
    {
        public MessageUpdateEntityHandlerAttribute(int position, string id) : base(position, id, UpdateType.Message)
        {
        }
    }
}