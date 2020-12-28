namespace Spire.Core.Sessions.Abstractions.Storage
{
    /// <summary>
    /// Session storage mode.
    /// </summary>
    public enum SessionStorageMode
    {
        /// <summary>
        /// Global storage mode. Same session storage for each session. 
        /// </summary>
        Global,

        /// <summary>
        /// User-based storage mode. Same session storage for each session with the same sender instance.
        /// </summary>
        PerUser,

        /// <summary>
        /// Entity and user-based storage mode. New session storage for each created session with the specified user and entity type.
        /// </summary>
        PerEntityType,
    }
}