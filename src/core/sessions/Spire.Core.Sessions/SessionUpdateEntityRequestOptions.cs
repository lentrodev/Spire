#region

using System;
using Spire.Core.Sessions.Abstractions;

#endregion

namespace Spire.Core.Sessions
{
    /// <summary>
    /// Base implementation of<see cref="ISessionUpdateEntityRequestOptions{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public class SessionUpdateEntityRequestOptions<TEntity> : ISessionUpdateEntityRequestOptions<TEntity>
    {
        /// <summary>
        /// Action to be invoked on every request step. 
        /// </summary>
        public Action<ISessionUpdateEntityRequestState<TEntity>> Action { get; set; }

        /// <summary>
        /// Matcher function, matches received entity on every request step.
        /// </summary>
        public Func<ISessionUpdateEntityRequestState<TEntity>, bool> Matcher { get; set; }

        /// <summary>
        /// Maximum count of attempts can be taken.
        /// </summary>
        public int Attempts { get; set; } = -1;
    }
}