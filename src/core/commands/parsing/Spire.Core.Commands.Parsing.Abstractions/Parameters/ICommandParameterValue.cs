using System;

namespace Spire.Core.Commands.Parsing.Abstractions.Parameters
{
    /// <summary>
    /// Base interface for implementing command parameter value.
    /// </summary>
    public interface ICommandParameterValue
    {
        /// <summary>
        /// Command parameter, this value related to.
        /// </summary>
        ICommandParameter Parameter { get; }
        
        /// <summary>
        /// Command parameter value related to <see cref="Parameter"/>.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Tries to convert <see cref="Value"/> to specified type. Returns <see langword="null"/> if converting was unsuccessfull.
        /// </summary>
        /// <typeparam name="T">Type, you want convert <see cref="Value"/> to.</typeparam>
        /// <returns>Converted parameter value.</returns>
        T As<T>();
    }
}