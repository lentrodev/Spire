#region

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spire.Core.Abstractions;
using Spire.Core.Abstractions.Processing.Results;
using Spire.Hosting.Args;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Hosting
{
    public abstract class BotHostBuilderBase<TBotHostBuilder> : IUpdateHandler
        where TBotHostBuilder : BotHostBuilderBase<TBotHostBuilder>
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
        /// Indicates bot status.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Triggers, when bot starts.
        /// </summary>
        public event EventHandler OnBotStarted;

        /// <summary>
        /// Triggers, when bot stops.
        /// </summary>
        public event EventHandler OnBotStopped;

        /// <summary>
        /// Triggers, when an update was processed..
        /// </summary>
        public event EventHandler<UpdateProcessedEventArgs> OnUpdateProcessed;

        /// <summary>
        /// Triggers, when an error was occured.
        /// </summary>
        public event EventHandler<ErrorOccuredEventArgs> OnErrorOccured;


        private readonly BotConfigurationOptions _botConfigurationOptions;

        private readonly CancellationTokenSource _cancellationTokenSource;

        protected BotHostBuilderBase(BotConfigurationOptions botConfigurationOptions)
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

            _botConfigurationOptions = botConfigurationOptions;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public TBotHostBuilder WithBot(Func<BotConfigurationOptions, IBot> botBuilder)
        {
            Bot = botBuilder(_botConfigurationOptions);

            AllowedUpdates = Bot.UpdateEntityProcessors
                .Select(updateEntityProcessor => updateEntityProcessor.EntityType)
                .ToArray();

            return (TBotHostBuilder) this;
        }

        public virtual Task HandleUpdate(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
            => Bot.Process(update).AsTask().ContinueWith(
                updateEntityProcessingResult => OnUpdateProcessed?.Invoke(this,
                    new UpdateProcessedEventArgs(updateEntityProcessingResult.Result)), cancellationToken);

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

            OnBotStopped?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Run()
        {
            if (IsRunning)
            {
                throw new InvalidOperationException("Bot has been already started");
            }

            IsRunning = true;

            Bot.BotClient.StartReceiving(this, _cancellationTokenSource.Token);

            OnBotStarted?.Invoke(this, EventArgs.Empty);
        }
    }
}