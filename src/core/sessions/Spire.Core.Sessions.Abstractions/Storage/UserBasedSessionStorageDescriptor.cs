namespace Spire.Core.Sessions.Abstractions.Storage
{
    /// <summary>
    /// User-based session storage descriptor.
    /// </summary>
    public class UserBasedSessionStorageDescriptor : SessionStorageDescriptor
    {
        /// <summary>
        /// Session owner id this descriptor related to.
        /// </summary>
        public int OwnerId { get; }

        /// <summary>
        /// Creates new <see cref="UserBasedSessionStorageDescriptor"/> with specified session owner id.
        /// </summary>
        /// <param name="ownerId">Session owner id.</param>
        public UserBasedSessionStorageDescriptor(int ownerId)
        {
            OwnerId = ownerId;
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
            return descriptor.OwnerId == OwnerId;
        }
    }
}