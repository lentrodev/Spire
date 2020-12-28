using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spire.Core.Commands.Parsing.Abstractions.Parameters;
using Spire.Core.Commands.Parsing.Abstractions.Parameters.Options;

namespace Spire.Core.Commands.Parsing.Abstractions
{
    /// <summary>
    /// Base interface for implementing command parser.
    /// </summary>
    public interface ICommandParser
    {
        /// <summary>
        /// Command parser configuration.
        /// </summary>
        ICommandParserConfiguration Configuration { get; }
        
        /// <summary>
        /// Collection of command parameter option handlers.
        /// </summary>
        IEnumerable<ICommandParameterOptionHandler> OptionHandlers { get; }
        
        /// <summary>
        /// Collection of command parameter types.
        /// </summary>
        IEnumerable<ICommandParameterType> Types { get; }
        
        /// <summary>
        /// Command parameter pattern.
        /// </summary>
        string ParameterPattern { get; }
        
        /// <summary>
        /// Command parameter option pattern.
        /// </summary>
        string ParameterOptionPattern { get; }
        
        /// <summary>
        /// Parses <see cref="ICommandFormat"/>.
        /// </summary>
        /// <param name="formatSource">Command format source text.</param>
        /// <returns>Parsed command format.</returns>
        ICommandFormat ParseCommandFormat(string formatSource);
        
        /// <summary>
        /// Parses command according to provided <paramref name="commandFormat"></paramref>.
        /// </summary>
        /// <param name="commandFormat">Command format.</param>
        /// <param name="source">Command text.</param>
        /// <returns>Command parser result.</returns>
        ICommandParserResult Parse(ICommandFormat commandFormat, string source);
    }
}