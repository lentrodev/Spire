#region

using System;
using System.Reflection;
using Autofac;
using Spire.Core.Abstractions.Processing;
using Spire.Core.Abstractions.Processing.Attributes;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Abstractions.Builders.Processing
{
    /// <summary>
    /// Base interface for implementing update entity processor builder.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TUpdateEntityHandlerAttribute">Update entity handler attribute.</typeparam>
    public interface
        IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> : IBuilder<
            IUpdateEntityProcessor<TEntity, TUpdateEntityHandlerAttribute>>
        where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
    {
        /// <summary>
        /// Autofac service resolving container.
        /// </summary>
        IContainer Container { get; }

        /// <summary>
        /// Processor identifier.
        /// </summary>
        string ProcessorId { get; }

        /// <summary>
        /// Entity type.
        /// </summary>
        UpdateType EntityType { get; }

        /// <summary>
        /// Adds update entity handler descriptor. 
        /// </summary>
        /// <param name="entityHandlerDescriptor">Update entity handler descriptor.</param>
        /// <returns>Configured update entity processor builder instance.</returns>
        IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> WithHandlerDescriptor(
            IUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute> entityHandlerDescriptor);

        /// <summary>
        /// Registers all update entity handlers from specified type.
        /// </summary>
        /// <param name="type">Type to register update entity handlers from.</param>
        /// <returns>Configured update entity processor builder instance.</returns>
        IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute>
            WithUpdateEntityHandlersFromType(Type type);

        /// <summary>
        /// Registers all update entity handlers from types, defined in specified assembly. If no assembly was specified, <see cref="Assembly.GetExecutingAssembly()"/> will be used. 
        /// </summary>
        /// <param name="assembly">Assembly to register update entity handlers from.</param>
        /// <returns>Configured update entity processor builder instance.</returns>
        IUpdateEntityProcessorBuilder<TEntity, TUpdateEntityHandlerAttribute> WithUpdateEntityHandlersFromAssembly(
            Assembly assembly = null);
    }
}