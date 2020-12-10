#region

using Spire.Core.Abstractions.Builders;

#endregion

namespace Spire.Core.Builders
{
    /// <summary>
    /// Default implementation of <see cref="IBuilder{T}"/>
    /// </summary>
    /// <typeparam name="T">Type to build.</typeparam>
    public abstract class Builder<T> : IBuilder<T>
    {
        /// <summary>
        /// Build instance of <see cref="T"/>.
        /// </summary>
        /// <returns>Built <see cref="T"/> instance.</returns>
        public abstract T Build();
    }

    /// <summary>
    /// Default implementation of <see cref="IBuilder{T, TBuildOptions}"/>
    /// </summary>
    /// <typeparam name="T">Type to build.</typeparam>
    /// <typeparam name="TBuildOptions">Builder options type.</typeparam>
    public abstract class Builder<T, TBuildOptions> : IBuilder<T, TBuildOptions>
    {
        /// <summary>
        /// Build instance of <see cref="T"/> with specified build options.
        /// </summary>
        /// <returns>Built <see cref="T"/> instance.</returns>
        public abstract T Build(TBuildOptions buildOptions = default);
    }
}