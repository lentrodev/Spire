#region

using Spire.Core.Abstractions.Processing.Attributes;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Attributes
{
    public class PositionedEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
    {
        public int Position { get; }

        public PositionedEntityHandlerAttribute(int position, string id, UpdateType entityType) : base(id, entityType)
        {
            Position = position;
        }
    }
}