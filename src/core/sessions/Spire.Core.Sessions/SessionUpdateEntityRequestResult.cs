#region

using Spire.Core.Sessions.Abstractions;

#endregion

namespace Spire.Core.Sessions
{
    /// <summary>
    /// Base implementation of <see cref="ISessionUpdateEntityRequestResult{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public class SessionUpdateEntityRequestResult<TEntity> : ISessionUpdateEntityRequestResult<TEntity>
    {
        /// <summary>
        /// Indicates, was the request result successful or not.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Request result entity.
        /// </summary>
        public TEntity Entity { get; }

        /// <summary>
        /// Attempts count, took to get the result.
        /// </summary>
        public int Attempts { get; }

        private SessionUpdateEntityRequestResult(bool isSuccess, TEntity entity, int attempts)
        {
            IsSuccess = isSuccess;
            Entity = entity;
            Attempts = attempts;
        }

        /// <summary>
        /// Creates new successful session update entity request result.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="attempts">Attempts count.</param>
        /// <returns></returns>
        public static ISessionUpdateEntityRequestResult<TEntity> Success(TEntity entity, int attempts)
        {
            return new SessionUpdateEntityRequestResult<TEntity>(true, entity, attempts);
        }

        /// <summary>
        /// Creates new failed session update entity request result.
        /// </summary>
        /// <param name="attempts">Attempts count.</param>
        /// <returns>Failed session update entity request result.</returns>
        public static ISessionUpdateEntityRequestResult<TEntity> Fail(int attempts)
        {
            return new SessionUpdateEntityRequestResult<TEntity>(false, default, attempts);
        }
    }
}