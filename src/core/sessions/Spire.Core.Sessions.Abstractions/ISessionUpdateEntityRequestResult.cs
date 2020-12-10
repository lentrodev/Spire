namespace Spire.Core.Sessions.Abstractions
{
    /// <summary>
    /// Base interface for implementation session update entity request result.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface ISessionUpdateEntityRequestResult<TEntity>
    {
        /// <summary>
        /// Indicates, was the request result successful or not.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Request result entity.
        /// </summary>
        TEntity Entity { get; }

        /// <summary>
        /// Attempts count, took to get the result.
        /// </summary>
        int Attempts { get; }
    }
}