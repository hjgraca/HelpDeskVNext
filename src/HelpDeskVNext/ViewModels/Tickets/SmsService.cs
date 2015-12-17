using Microsoft.Extensions.Logging;
using Twilio;

namespace HelpDeskVNext.ViewModels.Tickets
{
    public class SmsService : ISmsService
    {
        private readonly TwilioRestClient _twilioRestClient;
        private readonly ILogger _logger;

        private static readonly string Telefone =
            System.Configuration.ConfigurationManager.AppSettings.Get("Twilio:TwilioPhoneNumber");

        public SmsService(TwilioRestClient twilioRestClient, ILoggerFactory loggerFactory)
        {
            _twilioRestClient = twilioRestClient;
            _logger = loggerFactory.CreateLogger<SmsService>();
        }

        public void SendMessage(string to, string body)
        {
            _twilioRestClient.SendMessage(Telefone, to, body);
            _logger.LogInformation($"Sms enviada para {to} com o texto: {body}");
        }
    }
}