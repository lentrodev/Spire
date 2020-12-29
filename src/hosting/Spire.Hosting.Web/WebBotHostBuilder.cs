#region

using System;

#endregion

namespace Spire.Hosting.Web
{
    public class WebBotHost : BotHostBase<WebBotHost, WebBotConfigurationOptions>
    {
        public WebBotHost(WebBotConfigurationOptions webBotConfigurationOptions) : base(webBotConfigurationOptions)
        {
            if (string.IsNullOrEmpty(webBotConfigurationOptions.WebhookUrl) ||
                string.IsNullOrWhiteSpace(webBotConfigurationOptions.WebhookUrl))
            {
                throw new ArgumentException("Telegram Bot Api endpoint path is null or empty.");
            }

            if (webBotConfigurationOptions.MaxConnections < 1 || webBotConfigurationOptions.MaxConnections > 100)
            {
                throw new ArgumentException("Max connections should be in range from 1 to 100");
            }
        }

        protected override void StopReceivingUpdates()
        {
            Bot.BotClient.DeleteWebhookAsync().GetAwaiter().GetResult();
        }
    }
}