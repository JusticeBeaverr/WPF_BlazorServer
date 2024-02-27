using App1.Command;
using App1.Models;
using App1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IChatService _chatService;
        public ObservableCollection<MessageModel> Messages { get; set; }
        public DelegateCommand ConnectCommand { get; }
        public DelegateCommand SendCommand { get; }
        public string messageToSend { get; set; }
        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    RaisePropertyChanged();

                }
            }
        }



        public MainViewModel(IChatService chatService)
        {
            _chatService = chatService;
            ConnectCommand = new DelegateCommand(Connect);
            SendCommand = new DelegateCommand(Send);
            _chatService.MessageReceived += OnMessageReceived;
            Messages = new ObservableCollection<MessageModel>();

        }

        private async void Connect(object? parameter)
        {
            try
            {
                await _chatService.Connect();
                IsConnected = true;

            }
            catch (Exception ex)
            {
                IsConnected = false;
            }
        }
        private void Send(object? parameter)
        {
            var message = new MessageModel { Text = messageToSend };
            _chatService.SendMessage(message);
            messageToSend = "";
        }

        private void OnMessageReceived(MessageModel message)
        {
            Messages.Add(message);

        }


    }
}
