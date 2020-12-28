namespace Spire.Core.Commands.Parsing.Abstractions.Parameters.Options
{
    /// <summary>
    /// Base interface for implementation command parameter option.
    /// </summary>
    public interface ICommandParameterOption
    {
        /// <summary>
        /// Command parameter option name.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Command parameter option value.
        /// </summary>
        string Value { get; }
    }
}