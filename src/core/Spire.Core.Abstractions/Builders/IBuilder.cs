namespace Spire.Core.Abstractions.Builders
{
    /// <summary>
    /// Helper interface for all builders.
    /// </summary>
    /// <typeparam name="T">Type to build.</typeparam>
    public interface IBuilder<T>
    {
        /// <summary>
        /// Build instance of <see cref="T"/>.
        /// </summary>
        /// <returns>Built <see cref="T"/> instance.</returns>
        T Build();
    }

    /// <summary>
    /// Helper interface for all builders.
    /// </summary>
    /// <typeparam name="T">Type to build.</typeparam>
    /// <typeparam name="TBuildOptions">Builder options type.</typeparam>
    public interface IBuilder<T, TBuildOptions>
    {
        /// <summary>
        /// Build instance of <see cref="T"/> with specified build options.
        /// </summary>
        /// <returns>Built <see cref="T"/> instance.</returns>
        T Build(TBuildOptions buildOptions = default);
    }
}