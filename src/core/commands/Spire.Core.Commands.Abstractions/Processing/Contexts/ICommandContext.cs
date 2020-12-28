#region

using System.Collections.Generic;
using Spire.Core.Abstractions.Processing.Contexts;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;

#endregion

namespace Spire.Core.Commands.Abstractions.Processing.Contexts
{
    /// <summary>
    /// Base interface for implementation command context.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface ICommandContext<TEntity> : IHandlerContext<TEntity>
    {
        IEnumerable<ICommandParameterValue> GetParameters();

        /// <summary>
        /// Gets parameter value by the specified argument name.
        /// </summary>
        /// <param name="parameterName">Parameter name.</param>
        /// <returns>Parameter value.</returns>
        string GetParameter(string parameterName);

        /// <summary>
        /// Gets parameter value by the specified argument name, converted to specified type.
        /// </summary>
        /// <param name="parameterName">Parameter name.</param>
        /// <typeparam name="T">Parameter value type.</typeparam>
        /// <returns>Parameter typed value.</returns>
        T GetParameter<T>(string parameterName);
    }
}