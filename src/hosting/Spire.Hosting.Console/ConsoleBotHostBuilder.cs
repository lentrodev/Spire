#region

using CommandLine;
using Telegram.Bot;

#endregion

namespace Spire.Hosting.Console
{
    public class ConsoleBotHost : BotHostBase<ConsoleBotHost, ConsoleBotConfigurationOptions>
    {
        ConsoleBotHost(ConsoleBotConfigurationOptions botConfigurationOptions) : base(botConfigurationOptions)
        {
        }

        public static ConsoleBotHost CreateDefault(ConsoleBotConfigurationOptions botConfigurationOptions)
        {
            return new ConsoleBotHost(botConfigurationOptions);
        }

        protected override void StartReceivingUpdates()
        {
            Bot.BotClient.StartReceiving(this);
        }

        public static ConsoleBotHost CreateDefault(string[] args)
        {
            return ConsoleBotHost.CreateDefault(Parser.Default
                .ParseArguments<ConsoleBotConfigurationOptions>(args).Value);
        }
    }
}