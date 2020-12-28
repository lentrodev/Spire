#region

using System;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;

#endregion

namespace Spire.Core.Commands.Parsing.Parameters
{
    /// <summary>
    /// Default implementation of <see cref="ICommandParameterValue"/>.
    /// </summary>
    public class CommandParameterValue : ICommandParameterValue
    {
        /// <summary>
        /// Command parameter, this value related to.
        /// </summary>
        public ICommandParameter Parameter { get; }

        /// <summary>
        /// Command parameter value related to <see cref="Parameter"/>.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates new <see cref="CommandParameterValue"/> with specified parameter and value.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        public CommandParameterValue(ICommandParameter parameter, string value)
        {
            Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Tries to convert <see cref="Value"/> to specified type. Returns <see langword="null"/> if converting was unsuccessfull.
        /// </summary>
        /// <typeparam name="T">Type, you want convert <see cref="Value"/> to.</typeparam>
        /// <returns>Converted parameter value.</returns>
        public T As<T>()
        {
            try
            {
                if (Convert.ChangeType(Value, typeof(T)) is T converted)
                {
                    return converted;
                }

                return default;
            }
            catch
            {
                return default;
            }
        }
    }
}