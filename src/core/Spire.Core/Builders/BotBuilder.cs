#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Autofac;
using Spire.Core.Abstractions;
using Spire.Core.Abstractions.Builders;
using Spire.Core.Abstractions.Processing;
using Spire.Core.Abstractions.Processing.Attributes;
using Telegram.Bot;

#endregion

namespace Spire.Core.Builders
{
    public class BotBuilder : Builder<IBot>, IBotBuilder
    {
        public IContainer ServiceContainer { get; }
        public ITelegramBotClient BotClient { get; }

        public BotDescriptor Descriptor { get; }

        private readonly ICollection<IUpdateEntityProcessor> _updateEntityProcessors;

        public BotBuilder(string name, ITelegramBotClient telegramBotClient, IContainer container) : this(
            new BotDescriptor(name), telegramBotClient, container)
        {
        }

        public BotBuilder(string name, Guid id, ITelegramBotClient telegramBotClient, IContainer container) : this(
            new BotDescriptor(id == Guid.Empty ? Guid.NewGuid() : id, name), telegramBotClient, container)
        {
        }

        public BotBuilder(BotDescriptor botDescriptor, ITelegramBotClient telegramBotClient, IContainer container)
        {
            Descriptor = botDescriptor;
            BotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));
            ServiceContainer = container ?? throw new ArgumentNullException(nameof(container));
            _updateEntityProcessors = new Collection<IUpdateEntityProcessor>();
        }

        public IBotBuilder WithUpdateEntityProcessor<TEntity, TUpdateEntityHandlerAttribute>(
            IUpdateEntityProcessor<TEntity, TUpdateEntityHandlerAttribute> updateEntityProcessor)
            where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
        {
            _updateEntityProcessors.Add(updateEntityProcessor ??
                                        throw new ArgumentNullException(nameof(updateEntityProcessor)));

            return this;
        }

        /// <summary>
        /// Builds new <see cref="IBot"/>.
        /// </summary>
        /// <returns>Built <see cref="IBot"/>.</returns>
        public override IBot Build()
        {
            return new Bot(Descriptor,
                BotClient,
                ServiceContainer,
                _updateEntityProcessors);
        }
    }
}