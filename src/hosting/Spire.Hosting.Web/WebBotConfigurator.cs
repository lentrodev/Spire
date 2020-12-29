#region

using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types.InputFiles;

#endregion

namespace Spire.Hosting.Web
{
    public class WebBotConfigurator
    {
        private WebBotConfigurationOptions _webBotConfigurationOptions;
        private Action<WebBotHost> _configureWebBotHost;

        public IServiceProvider Services { get; }

        public WebBotConfigurator(IServiceProvider services)
        {
            _webBotConfigurationOptions = new WebBotConfigurationOptions();
            Services = services;
        }

        public WebBotConfigurator ConfigureBotHost(Action<WebBotHost> configureWebBotHost)
        {
            _configureWebBotHost = configureWebBotHost ?? throw new ArgumentNullException(nameof(configureWebBotHost));

            return this;
        }

        public WebBotConfigurator WithConfigurationOptions(string path = "Spire")
        {
            IConfiguration configuration = Services.GetService<IConfiguration>();

            return WithConfigurationOptions(configuration, path);
        }

        public WebBotConfigurator WithConfigurationOptions(IConfiguration configuration, string path = "Spire")
        {
            return WithConfigurationOptions(configuration.GetSection(path));
        }

        public WebBotConfigurator WithConfigurationOptions(IConfigurationSection configurationSection)
        {
            WebBotConfigurationOptions options = configurationSection.Get<WebBotConfigurationOptions>();

            return WithConfigurationOptions(options);
        }

        public WebBotConfigurator WithConfigurationOptions(WebBotConfigurationOptions webBotConfigurationOptions)
        {
            if (webBotConfigurationOptions == null)
            {
                throw new ArgumentNullException(nameof(webBotConfigurationOptions));
            }

            if (string.IsNullOrEmpty(webBotConfigurationOptions.Name))
            {
                throw new ArgumentNullException(nameof(webBotConfigurationOptions.Name));
            }

            if (string.IsNullOrEmpty(webBotConfigurationOptions.ApiToken))
            {
                throw new ArgumentNullException(nameof(webBotConfigurationOptions.ApiToken));
            }

            _webBotConfigurationOptions = webBotConfigurationOptions;

            return this;
        }

        public WebBotConfigurator WithApiToken(string apiToken)
        {
            if (string.IsNullOrEmpty(apiToken))
            {
                throw new ArgumentNullException(nameof(apiToken));
            }

            _webBotConfigurationOptions.ApiToken = apiToken;

            return this;
        }


        public WebBotConfigurator WithBotName(string botName)
        {
            if (string.IsNullOrEmpty(botName))
            {
                throw new ArgumentNullException(nameof(botName));
            }

            _webBotConfigurationOptions.Name = botName;

            return this;
        }

        public WebBotConfigurator WithWebhookPath(string webhookPath)
        {
            if (string.IsNullOrEmpty(webhookPath))
            {
                throw new ArgumentNullException(nameof(webhookPath));
            }

            _webBotConfigurationOptions.EndpointPath = webhookPath;

            return this;
        }

        public WebBotConfigurator WithCertificate(InputFileStream certificateFileStream)
        {
            if (certificateFileStream == null)
            {
                throw new ArgumentNullException(nameof(certificateFileStream));
            }

            _webBotConfigurationOptions.Certificate = certificateFileStream;

            return this;
        }

        public WebBotConfigurator WithCertificate(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return WithCertificate(new InputOnlineFile(stream));
        }

        public WebBotConfigurator WithCertificate(Stream stream, string fileName)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return WithCertificate(new InputOnlineFile(stream, fileName));
        }

        public WebBotConfigurator WithMaxConnections(int maxConnections)
        {
            if (maxConnections < 1 || maxConnections > 100)
            {
                throw new ArgumentException("Max connections should be in range from 1 to 100");
            }

            _webBotConfigurationOptions.MaxConnections = maxConnections;

            return this;
        }

        internal WebBotHost BuildWebBotHost()
        {
            WebBotHost webBotHost = new WebBotHost(_webBotConfigurationOptions);

            _configureWebBotHost(webBotHost);

            return webBotHost;
        }
    }
}