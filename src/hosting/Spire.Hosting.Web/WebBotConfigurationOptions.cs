#region

using Telegram.Bot.Types.InputFiles;

#endregion

namespace Spire.Hosting.Web
{
    public class WebBotConfigurationOptions : BotConfigurationOptions
    {
        public string WebhookUrl { get; set; }

        public string EndpointPath { get; set; }

        public InputFileStream Certificate { get; set; }

        public int MaxConnections { get; set; } = 40;
    }
}