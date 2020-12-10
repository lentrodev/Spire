#region

using System;
using System.Reflection;
using Spire.Core.Abstractions.Builders;
using Spire.Core.Commands.Abstractions.Processing;
using Spire.Core.Commands.Abstractions.Processing.Attributes;

#endregion

namespace Spire.Core.Commands.Abstractions.Builders.Processing
{
    /// <summary>
    /// Base interface for implementation command processor builder.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TCommandHandlerAttribute">Command handler attribute.</typeparam>
    public interface
        ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute> : IBuilder<
            ICommandProcessor<TEntity, TCommandHandlerAttribute>>
        where TCommandHandlerAttribute : CommandHandlerAttributeBase
    {
        /// <summary>
        /// Adds new command handler descriptor.
        /// </summary>
        /// <param name="commandHandlerDescriptor">Command handler descriptor</param>
        /// <returns>Configured command processor builder instance.</returns>
        ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute> WithCommandDescriptor(
            ICommandHandlerDescriptor<TCommandHandlerAttribute> commandHandlerDescriptor);

        /// <summary>
        /// Adds command handlers from specified type.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <returns>Configured command processor builder instance.</returns>
        ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute> WithCommandHandlersFromType(Type type);

        /// <summary>
        /// Adds command handlers from specified assembly.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <returns>Configured command processor builder instance.</returns>
        ICommandProcessorBuilder<TEntity, TCommandHandlerAttribute> WithCommandHandlersFromAssembly(
            Assembly assembly = null);
    }
}