#region

using System.Threading.Tasks;
using Spire.Core.Commands.Abstractions.Processing.Contexts;
using Spire.Examples.Shared.Attributes;
using Telegram.Bot.Types;

#endregion

namespace Spire.Examples.Shared.Commands
{
    public class SimpleCommandsSource
    {
        [MessageCommandHandler("ping", Defaults.DefaultProcessorId)]
        public async Task Ping(ICommandContext<Message> commandContext)
        {
            await commandContext.BotClient.SendTextMessageAsync(commandContext.Entity.Chat, "Pong!");
        }

        [MessageCommandHandler("priority {Value:number:minValue=0;maxValue=5} {Task}", Defaults.DefaultProcessorId)]
        public async Task Priority(ICommandContext<Message> commandContext)
        {
            int priority = commandContext.GetParameter<int>("Value");
            string task = commandContext.GetParameter<string>("Task");

            await commandContext.BotClient.SendTextMessageAsync(commandContext.Entity.Chat,
                $"You have set priority level {priority} for task '{task}'.");
        }
    }
}