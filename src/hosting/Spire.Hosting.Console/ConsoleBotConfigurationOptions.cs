#region

using CommandLine;

#endregion

namespace Spire.Hosting.Console
{
    public class ConsoleBotConfigurationOptions : BotConfigurationOptions
    {
        [Option("apiToken", Required = true, HelpText = "Telegram Bot Api Token.")]
        public override string ApiToken { get; set; }

        [Option("botName", Required = true, HelpText = "Name of the bot.")]
        public override string Name { get; set; }
    }
}