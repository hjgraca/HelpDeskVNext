namespace HelpDeskVNext.Services.SmsProvider
{
    public interface ISmsService
    {
        string ProcessMessage(string from, string body);
        void SendMessage();
    }
}