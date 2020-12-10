namespace Spire.Core.Abstractions
{
    /// <summary>
    /// Helper interface to make unidentifiable things identifiable.
    /// </summary>
    /// <typeparam name="TId">Identifier type.</typeparam>
    public interface IIdentifiable<out TId>
        where TId : notnull
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        TId Id { get; }
    }
}