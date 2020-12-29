#region

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Telegram.Bot.Types;

#endregion

namespace Spire.Hosting.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSpire(this IServiceCollection serviceCollection,
            Action<WebBotConfigurator> configureWebBotHost)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (configureWebBotHost == null)
            {
                throw new ArgumentNullException(nameof(configureWebBotHost));
            }

            serviceCollection.AddSingleton(provider =>
            {
                WebBotConfigurator configurator = new WebBotConfigurator(provider);

                configureWebBotHost(configurator);

                WebBotHost webBotHost = configurator.BuildWebBotHost();

                return webBotHost;
            });
            serviceCollection.AddSingleton<BotHostBase<WebBotHost, WebBotConfigurationOptions>>(
                provider => provider.GetService<WebBotHost>());

            return serviceCollection;
        }

        public static IApplicationBuilder UseSpire(this IApplicationBuilder app)
        {
            WebBotHost webBotHost = app.ApplicationServices.GetService<WebBotHost>() ??
                                    throw new ArgumentNullException(nameof(webBotHost));

            WebBotConfigurationOptions options = webBotHost.ConfigurationOptions;

            Uri requiredRequestUrl =
                new Uri(new Uri(options.WebhookUrl, UriKind.RelativeOrAbsolute), options.EndpointPath);

            if (requiredRequestUrl.Scheme != Uri.UriSchemeHttps)
            {
                throw new InvalidOperationException("Telegram Api Webhook supports only HTTPS.");
            }

            if (requiredRequestUrl.Port != 80 &&
                requiredRequestUrl.Port != 88 &&
                requiredRequestUrl.Port != 443 &&
                requiredRequestUrl.Port != 8443)
            {
                throw new InvalidOperationException(
                    "Telegram Bot Api webhook supports only ports 80, 88, 443, and 8443.");
            }

            app.Use(async (context, next) =>
            {
                if (HttpMethods.IsPost(context.Request.Method)
                    && context.Request.IsHttps
                    && string.Compare(context.Request.Path.Value, options.EndpointPath, StringComparison.Ordinal) >= 0)
                {
                    using StreamReader updateReader = new StreamReader(context.Request.Body);

                    string updateRawData = await updateReader.ReadToEndAsync();

                    Update update = JsonConvert.DeserializeObject<Update>(updateRawData);

                    await webBotHost.HandleUpdate(webBotHost.Bot.BotClient, update, CancellationToken.None);
                }
                else await next();
            });

            ConfigureWebhook(webBotHost, requiredRequestUrl, options).GetAwaiter().GetResult();

            webBotHost.Run();

            return app;
        }

        private static async Task ConfigureWebhook(WebBotHost webBotHost, Uri requiredRequestUrl,
            WebBotConfigurationOptions options)
        {
            WebhookInfo webhookInfo = await webBotHost.Bot.BotClient.GetWebhookInfoAsync();

            if (!string.IsNullOrEmpty(webhookInfo.Url))
            {
                await webBotHost.Bot.BotClient.DeleteWebhookAsync();
            }

            await webBotHost.Bot.BotClient.SetWebhookAsync(requiredRequestUrl.ToString(), options.Certificate,
                options.MaxConnections, webBotHost.AllowedUpdates);
        }
    }
}