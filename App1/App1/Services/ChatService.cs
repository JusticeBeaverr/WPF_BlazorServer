using App1.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services
{
    public class ChatService : IChatService
    {
        private readonly HubConnection _hubConnection;

        public event Action<MessageModel> MessageReceived;

        public ChatService()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7113/webapp")
                .Build();

            _hubConnection.On<MessageModel>("ReceiveMessage", message =>
            {
                MessageReceived?.Invoke(message);
            });
        }

        public async Task Connect()
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
                await _hubConnection.StartAsync();
        }

        public async Task SendMessage(MessageModel message)
        {
            if (_hubConnection.State == HubConnectionState.Connected)
                await _hubConnection.SendAsync("SendMessage", message);
            else
                throw new InvalidOperationException("Connection is not established.");
        }
    }
}
