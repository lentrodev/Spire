#region

using System;

#endregion

namespace Spire.Core.Sessions.Abstractions
{
    /// <summary>
    /// Base interface for implementation session update entity request options.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface ISessionUpdateEntityRequestOptions<TEntity>
    {
        /// <summary>
        /// Action to be invoked on every request step. 
        /// </summary>
        Action<ISessionUpdateEntityRequestState<TEntity>> Action { get; }

        /// <summary>
        /// Matcher function, matches received entity on every request step.
        /// </summary>
        Func<ISessionUpdateEntityRequestState<TEntity>, bool> Matcher { get; }

        /// <summary>
        /// Maximum count of attempts can be taken.
        /// </summary>
        int Attempts { get; }
    }
}