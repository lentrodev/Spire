#region

using System;

#endregion

namespace Spire.Core.Abstractions
{
    /// <summary>
    /// Represents common information about bot instance.
    /// </summary>
    public readonly struct BotDescriptor : IIdentifiable<Guid>
    {
        /// <summary>
        /// Bot unique identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Bot name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Creates new <see cref="BotDescriptor"/> with specified bot name.
        /// </summary>
        /// <param name="botName">Bot name.</param>
        public BotDescriptor(string botName) : this(Guid.NewGuid(), botName)
        {
        }

        /// <summary>
        /// Creates new <see cref="BotDescriptor"/> with specified bot id and bot name.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="botName">Bot name.</param>
        public BotDescriptor(Guid botId, string botName)
        {
            Id = botId;
            Name = botName;
        }
    }
}