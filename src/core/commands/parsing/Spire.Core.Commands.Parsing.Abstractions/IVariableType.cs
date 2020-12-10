namespace Spire.Core.Commands.Parsing.Abstractions
{
    /// <summary>
    /// Base interface for implementing variable type.
    /// </summary>
    public interface IVariableType
    {
        /// <summary>
        /// Regular expression pattern.
        /// </summary>
        string Pattern { get; }

        /// <summary>
        /// Short variable name.
        /// </summary>
        string Name { get; }
    }
}