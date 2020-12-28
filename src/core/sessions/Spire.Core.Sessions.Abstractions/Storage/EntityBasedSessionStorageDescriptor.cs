#region

using System;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Sessions.Abstractions.Storage
{
    /// <summary>
    /// Entity type-based session storage descriptor.
    /// </summary>
    public class EntityBasedSessionStorageDescriptor : UserBasedSessionStorageDescriptor
    {
        /// <summary>
        /// Session entity type.
        /// </summary>
        public UpdateType EntityType { get; }

        /// <summary>
        /// Session entity type (C# <see cref="Type"/>).
        /// </summary>
        public Type EntityClassType { get; }

        /// <summary>
        /// Creates new <see cref="UserBasedSessionStorageDescriptor"/> with specified session owner id.
        /// </summary>
        /// <param name="ownerId">Session owner id.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <param name="entityClassType">Session entity type (C# <see cref="Type"/>).</param>
        public EntityBasedSessionStorageDescriptor(
            int ownerId,
            UpdateType entityType,
            Type entityClassType) : base(ownerId)
        {
            EntityType = entityType;
            EntityClassType = entityClassType ?? throw new ArgumentNullException(nameof(entityClassType));
        }

        public override bool Equals(object obj)
        {
            if (obj is EntityBasedSessionStorageDescriptor descriptor)
            {
                return Equals(descriptor);
            }
            else
            {
                return Equals(this, obj);
            }
        }

        public bool Equals(EntityBasedSessionStorageDescriptor descriptor)
        {
            return descriptor.OwnerId == OwnerId && descriptor.EntityType == EntityType &&
                   descriptor.EntityClassType == EntityClassType;
        }
    }
}