using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using Twilio;

namespace HelpDeskVNext.ViewModels.Tickets
{
    public class SmsService : ISmsService
    {
        private readonly TwilioRestClient _twilioRestClient;
        private readonly TelemetryClient _telemetry;
        private readonly ILogger _logger;

        private static readonly string Telefone =
            System.Configuration.ConfigurationManager.AppSettings.Get("Twilio:TwilioPhoneNumber");

        public SmsService(TwilioRestClient twilioRestClient, ILoggerFactory loggerFactory, TelemetryClient telemetry)
        {
            _twilioRestClient = twilioRestClient;
            _telemetry = telemetry;
            _logger = loggerFactory.CreateLogger<SmsService>();
        }

        public void SendMessage(string to, string body)
        {
            if (string.IsNullOrWhiteSpace(Telefone) || string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(body)) return;

            _twilioRestClient.SendMessage(Telefone, to, body);
            string message = $"Sms enviada para {to} com o texto: {body}";
            _logger.LogInformation(message);

            var properties = new Dictionary<string, string> { { "Sms", message }};
            _telemetry.TrackEvent("smsenviada", properties);
        }
    }
}