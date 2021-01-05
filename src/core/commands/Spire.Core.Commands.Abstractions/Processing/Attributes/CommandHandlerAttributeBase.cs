#region

using System;
using Spire.Core.Abstractions;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Commands.Abstractions.Processing.Attributes
{
    /// <summary>
    /// Base command handler attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class CommandHandlerAttributeBase : Attribute, IIdentifiable<string>
    {
        /// <summary>
        /// Processor identifier, this attribute related to.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Entity type.
        /// </summary>
        public UpdateType EntityType { get; }

        protected CommandHandlerAttributeBase(string id, UpdateType entityType)
        {
            Id = id;
            EntityType = entityType;
        }
    }
}