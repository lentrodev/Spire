#region

using Serilog;
using Spire.Examples.Shared;
using Spire.Hosting.Console;

#endregion

namespace Spire.Examples.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureLogging();

            ConsoleBotHost consoleBotHost = ConsoleBotHost
                .CreateDefault(args)
                .WithBot(configuration => Defaults.BuildDefaultBot(configuration.ApiToken));

            consoleBotHost.OnBotStarted += (o, e) => Log.Information("Bot has been started.");
            consoleBotHost.OnBotStopped += (o, e) => Log.Information("Bot has been stopped.");

            consoleBotHost.OnUpdateProcessed += (o, e) =>
                Log.Information(
                    $"Update with id {e.ProcessingResult.Update.Id} and {e.ProcessingResult.Update.Type} type has been processed in {e.ProcessingResult.Time}.");

            consoleBotHost.OnErrorOccured += (o, e) =>
                Log.Error(e.Exception, "An error was occured when receiving/processing an update.");

            consoleBotHost.Run();

            while (true)
            {
                System.Console.ReadKey();
                consoleBotHost.Stop();
            }
        }

        static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console().CreateLogger();
        }
    }
}