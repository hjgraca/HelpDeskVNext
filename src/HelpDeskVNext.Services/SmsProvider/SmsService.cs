using Twilio;

namespace HelpDeskVNext.Services.SmsProvider
{
    public class SmsService : ISmsService
    {
        private readonly TwilioRestClient _twilioRestClient;

        public SmsService(TwilioRestClient twilioRestClient)
        {
            _twilioRestClient = twilioRestClient;
        }

        public string ProcessMessage(string from, string body)
        {
            //string smsResponse;
            //return TwiML(Respond(smsResponse));

            //_twilioRestClient.SendMessage("441455561010", "4407808557240", "test");

            return string.Empty;
        }

        public void SendMessage()
        {
            
        }
    }
}
