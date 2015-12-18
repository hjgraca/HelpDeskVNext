using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using Twilio;

namespace HelpDeskVNext.ViewModels.Tickets
{
    public class SmsService : ISmsService
    {
        private readonly SmsClient _twilioRestClient;
        private readonly TelemetryClient _telemetry;
        private readonly ILogger _logger;

        public SmsService(SmsClient twilioRestClient, ILoggerFactory loggerFactory, TelemetryClient telemetry)
        {
            _twilioRestClient = twilioRestClient;
            _telemetry = telemetry;
            _logger = loggerFactory.CreateLogger<SmsService>();
        }

        public void SendMessage(string to, string body)
        {
            string phone = _twilioRestClient.Phone;
            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(body)) return;

            _twilioRestClient.SendMessage(phone, to, body);
            string message = $"Sms enviada para {to} com o texto: {body}";
            _logger.LogInformation(message);

            var properties = new Dictionary<string, string> { { "Sms", message }};
            _telemetry.TrackEvent("smsenviada", properties);
        }
    }
}