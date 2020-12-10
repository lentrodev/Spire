#region

using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Sessions.Abstractions
{
    /// <summary>
    /// Base interface for implementing session manager.
    /// </summary>
    public interface ISessionManager
    {
        /// <summary>
        /// Enumerates all the sessions, related to the current session manager.
        /// </summary>
        /// <returns>Sessions collection.</returns>
        IEnumerable<ISession> GetSessions();

        /// <summary>
        /// Adds a new session.
        /// </summary>
        /// <param name="session">Session to add.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        void AddSession<TEntity>(ISession<TEntity> session);

        /// <summary>
        /// Creates a new session without adding it to the session manager.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>Created session</returns>
        ISession<TEntity> CreateSession<TEntity>(User owner, UpdateType entityType);

        /// <summary>
        /// Checks a session for existence.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>Session existence.</returns>
        bool IsSessionExists<TEntity>(User owner, UpdateType entityType);

        /// <summary>
        /// Gets a session. Returns <see langword="null"/>, if session doesn't exists.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>A session.</returns>
        ISession<TEntity> GetSession<TEntity>(User owner, UpdateType entityType);
    }
}