using Twilio;

namespace HelpDeskVNext.ViewModels.Tickets
{
    public class SmsClient : TwilioRestClient
    {
        public string Phone { get; set; }

        public SmsClient(string accountSid, string authToken) : 
            base(accountSid, authToken)
        {
        }

        public SmsClient(string accountSid, string authToken, string accountResourceSid, string apiVersion, string baseUrl) : 
            base(accountSid, authToken, accountResourceSid, apiVersion, baseUrl)
        {
        }

        public SmsClient(string accountSid, string authToken, string phone) :
            base(accountSid, authToken)
        {
            Phone = phone;
        }
    }
}