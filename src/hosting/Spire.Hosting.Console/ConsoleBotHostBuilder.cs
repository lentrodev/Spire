#region

using CommandLine;

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

        public static ConsoleBotHost CreateDefault(string[] args)
        {
            return ConsoleBotHost.CreateDefault(Parser.Default
                .ParseArguments<ConsoleBotConfigurationOptions>(args).Value);
        }
    }
}