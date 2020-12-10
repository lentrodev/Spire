#region

using System;
using System.Threading.Tasks;
using Spire.Core.Commands.Abstractions.Processing.Contexts;
using Spire.Core.Markups.Inline;
using Spire.Core.Sessions;
using Spire.Core.Sessions.Abstractions;
using Spire.Examples.Shared.Attributes;
using Spire.Examples.Shared.Services;
using Telegram.Bot.Types;

#endregion

namespace Spire.Examples.Shared.Commands
{
    public class RequestMessageCommandsSource
    {
        private readonly IFeedbackService _feedbackService;

        public RequestMessageCommandsSource(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [MessageCommandHandler("feedback", Defaults.DefaultProcessorId)]
        public async Task ProvideFeedbackCommand(ICommandContext<Message> commandContext, 
            Random random, 
            InlineKeyboardMarkupBuilder inlineKeyboardBuilder)
        {
            ISession<Message> session = commandContext.Session();
            
            ISessionUpdateEntityRequestResult<Message> feedbackMessageRequestResult = await session.RequestEntityAsync(
                new SessionUpdateEntityRequestOptions<Message>
                {
                    Action = state =>
                        commandContext.BotClient.SendTextMessageAsync(commandContext.Entity.Chat, "Write a feedback message:"),
                    Matcher = state => state.LastEntity.Text == "123"
                });

            int randomFeedbackIdentifier = random.Next();

            string feedbackText = feedbackMessageRequestResult.Entity.Text;

            _feedbackService.ProvideFeedback(randomFeedbackIdentifier, feedbackText);
        }
    }
}