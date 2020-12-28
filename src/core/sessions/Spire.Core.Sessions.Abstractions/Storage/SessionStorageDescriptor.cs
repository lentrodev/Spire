namespace Spire.Core.Sessions.Abstractions.Storage
{
    /// <summary>
    /// Global session storage descriptor.
    /// </summary>
    public class SessionStorageDescriptor
    {
        /// <summary>
        /// Shared global instance.
        /// </summary>
        public static SessionStorageDescriptor Shared { get; } = new SessionStorageDescriptor();
    }
}