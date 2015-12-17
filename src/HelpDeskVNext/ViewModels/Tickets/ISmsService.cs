using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDeskVNext.ViewModels.Tickets
{
    public interface ISmsService
    {
        void SendMessage(string to, string body);
    }
}
