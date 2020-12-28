namespace Spire.Core.Markups.Abstractions
{
    /// <summary>
    /// Base interface for implementing keyboard markup size limit.
    /// </summary>
    public interface IKeyboardMarkupSizeLimit
    {
        /// <summary>
        /// Max buttons count.
        /// </summary>
        int Buttons { get; }

        /// <summary>
        /// Max columns count.
        /// </summary>
        int Columns { get; }

        /// <summary>
        /// Max rows count.
        /// </summary>
        int Rows { get; }
    }
}