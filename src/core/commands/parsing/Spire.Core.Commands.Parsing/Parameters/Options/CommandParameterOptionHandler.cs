#region

using System;
using Spire.Core.Commands.Parsing.Abstractions.Parameters.Options;

#endregion

namespace Spire.Core.Commands.Parsing.Parameters.Options
{
    /// <summary>
    /// Default implementation of <see cref="ICommandParameterOptionHandler"/>.
    /// </summary>
    public class CommandParameterOptionHandler : ICommandParameterOptionHandler
    {
        /// <summary>
        /// Command parameter option name this handler related to.
        /// </summary>
        public string Name { get; }

        private readonly Func<string, string, bool> _optionHandler;

        /// <summary>
        /// Creates new <see cref="CommandParameterOptionHandler"/> with specified name and option handler.
        /// </summary>
        /// <param name="name">Option name this handler related to.</param>
        /// <param name="optionHandler">Option handler func.</param>
        public CommandParameterOptionHandler(string name, Func<string, string, bool> optionHandler)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            _optionHandler = optionHandler ?? throw new ArgumentNullException(nameof(optionHandler));
        }

        /// <summary>
        /// Matches command parameter option.
        /// </summary>
        /// <param name="optionValue">Option value.</param>
        /// <param name="source">Source value, parsed from command text.</param>
        /// <returns>Matching result.</returns>
        public bool IsMatch(string optionValue, string source) => _optionHandler(optionValue, source);
    }

    /// <summary>
    /// Command parameter option handler with automatically converting option value to specified type.
    /// </summary>
    /// <typeparam name="TOption">Option type.</typeparam>
    public class CommandParameterOptionHandler<TOption> : CommandParameterOptionHandler
    {
        /// <summary>
        /// Creates new <see cref="CommandParameterOptionHandler"/> with specified name and option handler.
        /// </summary>
        /// <param name="name">Option name this handler related to.</param>
        /// <param name="optionHandler">Option handler func.</param>
        public CommandParameterOptionHandler(string name, Func<TOption, string, bool> optionHandler)
            : base(name, (optionStr, valueStr)
                =>
            {
                try
                {
                    if (Convert.ChangeType(optionStr, typeof(TOption)) is TOption option)
                    {
                        return optionHandler(option, valueStr);
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            })
        {
        }
    }

    /// <summary>
    /// Command parameter option handler with automatically converting both option and source values to specified types.
    /// </summary>
    /// <typeparam name="TOption">Option type.</typeparam>
    /// <typeparam name="TSourceValue">Source value type.</typeparam> 
    public class CommandParameterOptionHandler<TOption, TSourceValue> : CommandParameterOptionHandler<TOption>
    {
        /// <summary>
        /// Creates new <see cref="CommandParameterOptionHandler"/> with specified name and option handler.
        /// </summary>
        /// <param name="name">Option name this handler related to.</param>
        /// <param name="optionHandler">Option handler func.</param>
        public CommandParameterOptionHandler(string name, Func<TOption, TSourceValue, bool> optionHandler)
            : base(name, (option, valueStr) =>
            {
                try
                {
                    if (Convert.ChangeType(valueStr, typeof(TSourceValue)) is TSourceValue sourceValue)
                    {
                        return optionHandler(option, sourceValue);
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            })
        {
        }
    }
}