namespace HelpDeskVNext.Services
{
    public interface ISmsService
    {
        string ProcessMessage(string from, string body);
        void SendMessage();
    }
}