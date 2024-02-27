using App2.Models;
using Microsoft.AspNetCore.SignalR;

namespace App2.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(MessageModel message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

    }
}
