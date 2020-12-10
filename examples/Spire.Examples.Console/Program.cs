#region

using System.Threading.Tasks;
using Serilog;
using Spire.Examples.Shared;
using Spire.Hosting.Console;
#endregion

namespace Spire.Examples.Console
{
    class Program
    {
        static Task Main(string[] args)
        {
            ConfigureLogging();

            ConsoleBotHostBuilder consoleBotHostBuilder = ConsoleBotHostBuilder
                .CreateDefault(args)
                .WithBot(configuration => Defaults.BuildDefaultBot(configuration.ApiToken));

            consoleBotHostBuilder.OnBotStarted += (o, e) => Log.Information("Bot has been started.");
            consoleBotHostBuilder.OnBotStopped += (o, e) => Log.Information("Bot has been stopped.");

            consoleBotHostBuilder.OnUpdateProcessed += (o, e) =>
                Log.Information(
                    $"Update with id {e.ProcessingResult.Update.Id} and {e.ProcessingResult.Update.Type} type has been processed in {e.ProcessingResult.Time}.");

            consoleBotHostBuilder.OnErrorOccured += (o, e) =>
                Log.Error(e.Exception, "An error was occured when receiving/processing an update.");

            consoleBotHostBuilder.Run();

            while (true)
            {
                System.Console.ReadKey();
                consoleBotHostBuilder.Stop();
            }
        }

        static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console().CreateLogger();
        }
    }
}