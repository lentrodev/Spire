namespace Spire.Core.Commands.Parsing.Abstractions.Parameters
{
    /// <summary>
    /// Base interface for implementing command parameter type.
    /// </summary>
    public interface ICommandParameterType
    {
        /// <summary>
        /// Command parameter type name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Command parameter type Regular Expression pattern.
        /// </summary>
        string Pattern { get; }
    }
}