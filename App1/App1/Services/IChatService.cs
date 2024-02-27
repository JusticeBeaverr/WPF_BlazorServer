using App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services
{
    public interface IChatService
    {
        Task SendMessage(MessageModel message);
        Task Connect();
        event Action<MessageModel> MessageReceived;
    }
}
