#region

using CommandLine;

#endregion

namespace Spire.Hosting.Console
{
    public class ConsoleBotHostBuilder : BotHostBuilderBase<ConsoleBotHostBuilder>
    {
        ConsoleBotHostBuilder(BotConfigurationOptions botConfigurationOptions) : base(botConfigurationOptions)
        {
        }

        public static ConsoleBotHostBuilder CreateDefault(BotConfigurationOptions botConfigurationOptions)
        {
            return new ConsoleBotHostBuilder(botConfigurationOptions);
        }

        public static ConsoleBotHostBuilder CreateDefault(string[] args)
        {
            return ConsoleBotHostBuilder.CreateDefault(Parser.Default
                .ParseArguments<ConsoleBotConfigurationOptions>(args).Value);
        }
    }
}