#region

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spire.Core.Abstractions;
using Spire.Hosting.Args;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Hosting
{
    public abstract class BotHostBase<TBotHost, TBotConfigurationOptions> : IUpdateHandler
        where TBotHost : BotHostBase<TBotHost, TBotConfigurationOptions>
        where TBotConfigurationOptions : BotConfigurationOptions
    {
        /// <summary>
        /// Indicates which UpdateTypes are allowed to be received. null means all updates
        /// </summary>
        public UpdateType[] AllowedUpdates { get; private set; }

        /// <summary>
        /// Bot instance.
        /// </summary>
        public IBot Bot { get; private set; }

        /// <summary>
        /// Bot configuration options.
        /// </summary>
        public TBotConfigurationOptions ConfigurationOptions { get; }

        /// <summary>
        /// Indicates bot status.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Triggers, when bot starts.
        /// </summary>
        public event EventHandler<BotDescriptor> OnBotStarted;

        /// <summary>
        /// Triggers, when bot stops.
        /// </summary>
        public event EventHandler<BotDescriptor> OnBotStopped;

        /// <summary>
        /// Triggers, when an update was processed..
        /// </summary>
        public event EventHandler<UpdateProcessedEventArgs> OnUpdateProcessed;

        /// <summary>
        /// Triggers, when an error was occured.
        /// </summary>
        public event EventHandler<ErrorOccuredEventArgs> OnErrorOccured;


        private readonly CancellationTokenSource _cancellationTokenSource;

        protected BotHostBase(TBotConfigurationOptions botConfigurationOptions)
        {
            if (botConfigurationOptions == null)
            {
                throw new ArgumentNullException(nameof(botConfigurationOptions));
            }

            if (string.IsNullOrEmpty(botConfigurationOptions.ApiToken) ||
                string.IsNullOrWhiteSpace(botConfigurationOptions.ApiToken))
            {
                throw new ArgumentException("Telegram Bot Api token is null or empty.");
            }

            if (string.IsNullOrEmpty(botConfigurationOptions.Name) ||
                string.IsNullOrWhiteSpace(botConfigurationOptions.Name))
            {
                throw new ArgumentException("Bot name token is null or empty.");
            }

            ConfigurationOptions = botConfigurationOptions;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public TBotHost WithBot(Func<BotConfigurationOptions, IBot> botBuilder)
        {
            Bot = botBuilder(ConfigurationOptions);

            AllowedUpdates = Bot.UpdateEntityProcessors
                .Select(updateEntityProcessor => updateEntityProcessor.EntityType)
                .ToArray();

            return (TBotHost) this;
        }

        public virtual Task HandleUpdate(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            if (IsRunning)
            {
                Task.Factory.StartNew(async () =>
                {
                    var updateEntityProcessingResult = await Bot.Process(update);

                    OnUpdateProcessed?.Invoke(this,
                        new UpdateProcessedEventArgs(updateEntityProcessingResult));
                }, cancellationToken);
            }

            return Task.CompletedTask;
        }

        public virtual Task HandleError(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            OnErrorOccured?.Invoke(this, new ErrorOccuredEventArgs(exception));

            return Task.CompletedTask;
        }

        public virtual void Stop()
        {
            _cancellationTokenSource.Cancel();
            IsRunning = false;

            StopReceivingUpdates();

            OnBotStopped?.Invoke(this, Bot.Descriptor);
        }

        protected virtual void StopReceivingUpdates()
        {
        }

        protected virtual void StartReceivingUpdates()
        {
        }

        public virtual void Run()
        {
            if (IsRunning)
            {
                throw new InvalidOperationException("Bot has been already started");
            }

            if (Bot == null)
            {
                throw new NullReferenceException("Bot instance is null. Please configure it with WithBot method.");
            }

            IsRunning = true;

            StartReceivingUpdates();

            OnBotStarted?.Invoke(this, Bot.Descriptor);
        }
    }
}