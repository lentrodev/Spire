#region

using System;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Abstractions.Processing.Attributes
{
    /// <summary>
    /// Base interface for implementing update entity handler attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class UpdateEntityHandlerAttributeBase : Attribute, IIdentifiable<string>
    {
        /// <summary>
        /// Identifier of update entity processor this handler related to.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Entity type.
        /// </summary>
        public UpdateType EntityType { get; }

        /// <summary>
        /// Creates new instance of <see cref="UpdateEntityHandlerAttributeBase"/> with specifier id and entity type.
        /// </summary>
        /// <param name="id">Identifier of update entity processor this handler related to.</param>
        /// <param name="entityType">Entity type.</param>
        protected UpdateEntityHandlerAttributeBase(string id, UpdateType entityType)
        {
            Id = id;

            EntityType = entityType;
        }
    }
}