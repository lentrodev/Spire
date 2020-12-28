using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;

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
        public static ICommandParameterType Number { get; } = new CommandParameterType("number", "([0]{1}|-?[1-9]{1}[0-9]{0,18})");
        
        /// <summary>
        /// Number with floating point command parameter type.
        /// </summary>
        public static ICommandParameterType Floating { get; } = new CommandParameterType("floating", "[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)");

        /// <summary>
        /// Boolean command parameter type.
        /// </summary>
        public static ICommandParameterType Bool { get; } = new CommandParameterType("bool", "(true|false)");
        
        #endregion
        
        #region Special
        
        /// <summary>
        /// Email command parameter type.
        /// </summary>
        public static ICommandParameterType Email { get; } = new CommandParameterType("email", @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
        
        /// <summary>
        /// Url command parameter type.
        /// </summary>
        public static ICommandParameterType Url { get; } = new CommandParameterType("url", @"(((([a-z]|\d|-|.||~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'()*+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]).(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]).(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]).(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|.||~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))).)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))).?)(:\d*)?)(/((([a-z]|\d|-|.||~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'()*+,;=]|:|@)+(/(([a-z]|\d|-|.||~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'()*+,;=]|:|@)))?)?(\?((([a-z]|\d|-|.||~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'()*+,;=]|:|@)|[\uE000-\uF8FF]|/|\?)*)?(#((([a-z]|\d|-|.||~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'()*+,;=]|:|@)|/|\?)*)?$");
        
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