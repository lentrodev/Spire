#region

using System.Collections.Generic;
using System.Linq;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;

#endregion

namespace Spire.Core.Commands.Parsing.Parameters
{
    /// <summary>
    /// Contains all built-in command parameter types.
    /// </summary>
    public static class CommandParameterTypes
    {
        #region Common

        /// <summary>
        /// String command parameter type.
        /// </summary>
        public static ICommandParameterType String { get; } = new CommandParameterType("string", ".*");

        /// <summary>
        /// Numeric command parameter type.
        /// </summary>
        public static ICommandParameterType Number { get; } =
            new CommandParameterType("number", "([0]{1}|-?[1-9]{1}[0-9]{0,18})");

        /// <summary>
        /// Number with floating point command parameter type.
        /// </summary>
        public static ICommandParameterType Floating { get; } =
            new CommandParameterType("floating", "[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)");

        /// <summary>
        /// Boolean command parameter type.
        /// </summary>
        public static ICommandParameterType Bool { get; } = new CommandParameterType("bool", "(true|false)");

        #endregion

        #region Special

        /// <summary>
        /// Email command parameter type.
        /// </summary>
        public static ICommandParameterType Email { get; } = new CommandParameterType("email",
            @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

        /// <summary>
        /// Url command parameter type.
        /// </summary>
        public static ICommandParameterType Url { get; } = new CommandParameterType("url",
            @"(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+");

        #endregion

        /// <summary>
        /// Common command parameter types.
        /// </summary>
        public static IEnumerable<ICommandParameterType> Common { get; } = new ICommandParameterType[]
        {
            String,
            Number,
            Floating,
            Bool
        };

        /// <summary>
        /// Special command parameter types.
        /// </summary>
        public static IEnumerable<ICommandParameterType> Special { get; } = new ICommandParameterType[]
        {
            Email,
            Url
        };

        /// <summary>
        /// Both <see cref="Common"/> and <see cref="Special"/> command parameter types.
        /// </summary>
        public static IEnumerable<ICommandParameterType> All { get; } = Enumerable.Concat(Common, Special);
    }
}