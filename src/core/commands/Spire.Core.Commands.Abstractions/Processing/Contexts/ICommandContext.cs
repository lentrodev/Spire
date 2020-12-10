#region

using Spire.Core.Abstractions.Processing.Contexts;

#endregion

namespace Spire.Core.Commands.Abstractions.Processing.Contexts
{
    /// <summary>
    /// Base interface for implementation command context.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface ICommandContext<TEntity> : IHandlerContext<TEntity>
    {
        /// <summary>
        /// Gets argument value by the specified argument name.
        /// </summary>
        /// <param name="argumentName">Argument name.</param>
        /// <returns>Argument value.</returns>
        string GetArgument(string argumentName);

        /// <summary>
        /// Gets argument value by the specified argument name, converted to specified type.
        /// </summary>
        /// <param name="argumentName">Argument name.</param>
        /// <typeparam name="T">Argument value type.</typeparam>
        /// <returns>Argument typed value.</returns>
        T GetArgument<T>(string argumentName);
    }
}