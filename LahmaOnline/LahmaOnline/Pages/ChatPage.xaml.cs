using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        private ViewState _state = ViewState.Disconnected;
        string connectionId;
        public ObservableCollection<BLL.M.Mobile.MessageModel> Messages { get; set; }
        public string UserName { get; set; } = AppStatics.UserProfile?.UserName;
        private bool _IsLoading = false;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                if (_IsLoading != value)
                {
                    _IsLoading = value;
                }
                OnPropertyChanged();
            }
        }
        public ChatPage()
        {
            InitializeComponent();
            Messages = new ObservableCollection<BLL.M.Mobile.MessageModel>();

            if (Device.RuntimePlatform == Device.Android)
                txtNameReceive.IsVisible = true;

            if (string.IsNullOrEmpty(AppStatics.UserProfile?.UserName))
                StackInsertEmail.IsVisible = true;
            else
                ConnectDisconnectEvent(this,null);

            this.BindingContext = this;
        }


        private async void ConnectDisconnectEvent(object sender, EventArgs e)
        {
            StackInsertEmail.IsVisible = false;
            UpdateState(ViewState.Connecting);

            try
            {
                var UserName = !string.IsNullOrEmpty(AppStatics.UserProfile?.UserName) ? AppStatics.UserProfile?.UserName : txtUserName.Text;

                Helper.HubCon.Connection.On<string>("ReceiveMessage", (ObjectMessageJson) =>
                {
                    //var Message = JsonConvert.DeserializeObject<BLL.M.Chat.DetailsMessage>(ObjectMessageJson);
                    //AppendMessage(new BLL.M.Mobile.MessageModel
                    //{
                    //    Message = Message.Text,
                    //    IsOwnMessage = false,
                    //    IsSystemMessage = true,
                    //});
                    //Helper.HubCon.Connection.InvokeAsync("EditInfoMessageIsReading", msg.Id);                
                    _ = new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("DetailsChat/IsReadMessage", new BLL.M.Chat.UpdateDetailsChatIsReadingDto
                    {
                        id= 0,
                        isReading = true
                    });

            });
                Helper.HubCon.Connection.On<string>("LoadOldMessage", (Object) =>
                {
                    var msg = JsonConvert.DeserializeObject<BLL.M.Mobile.MessageModel>(Object);
                    InsertMessage(new BLL.M.Mobile.MessageModel
                    {
                        User = msg.User,
                        Message = msg.Message,
                        IsOwnMessage = msg.From == UserName,
                        IsSystemMessage = msg.From != UserName,
                    });
                });
                //Other Method to check if there no connect before
                //if not there, then create one

                if (string.IsNullOrEmpty(AppStatics.UserProfile?.Email))
                    await Helper.HubCon.Connection.InvokeAsync("Connect");
                connectionId = Helper.HubCon.Connection.ConnectionId;

                _ = new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("HeaderChat/StartChat", new BLL.M.Chat.SaveHeaderChatDto
                {
                    eMail = UserName,
                    mobile = "0785461900",
                    sessionId = connectionId,
                });
            }
            catch (Exception ex)
            {
                _ = DisplayAlert("Error", $"An error occurred while connecting: {ex}", "Ok");
                //AppendMessage(new BLL.M.Mobile.MessageModel { Message =  $"An error occurred while connecting: {ex}" ,IsOwnMessage=true});
                UpdateState(ViewState.Disconnected);
                return;
            }

            UpdateState(ViewState.Connected);
        }

        private async void SendButton_Clicked(object sender, EventArgs e)
        {
            if (_state != ViewState.Connected)
            {
                await DisplayAlert("Error", "Must be Connected to Send!", "OK");
                return;
            }

            try
            {
                var UserName = !string.IsNullOrEmpty(AppStatics.UserProfile?.UserName) ? AppStatics.UserProfile?.UserName : txtUserName.Text;
                var receiveName = "test0@gmail.com";
                if (Device.RuntimePlatform == Device.Android)
                       receiveName = txtNameReceive.Text;
                await Helper.HubCon.Connection.SendAsync("SendMessage", receiveName, MessageEntry.Text);
                //call api storge message in database
                _ = new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("DetailsChat/SendMessage", new BLL.M.Chat.SaveDetailsChatDto
                {                    
                    sessionID = connectionId,
                    message = MessageEntry.Text,
                });
                AppendMessage(new BLL.M.Mobile.MessageModel
                {
                    User = UserName,
                    Message = MessageEntry.Text,
                    IsOwnMessage = true,
                    IsSystemMessage = false,
                });
                MessageEntry.Text = "";
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"An error occurred while connecting: {ex}", "Ok");
            }
        }
        /// <summary>
        /// Used for Add Message in Last List in Chat Message
        /// </summary>
        /// <param name="Message"></param>
        private void AppendMessage(BLL.M.Mobile.MessageModel Message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Messages.Add(Message);
                MessagesListView.ScrollTo(Messages.Last(), ScrollToPosition.End, animated: true);
            });
        }
        /// <summary>
        /// Used for Load More Message in Above List in Chat Message
        /// </summary>
        /// <param name="Message"></param>
        private void InsertMessage(BLL.M.Mobile.MessageModel Message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var crruntCount = Messages.Count();
                Messages.Insert(Messages.Count() - crruntCount, Message);
            });
        }

        private void UpdateState(ViewState state)
        {
            if (_state == state)
            {
                return;
            }

            switch (state)
            {
                case ViewState.Disconnected:
                    NoteConnected.BackgroundColor = Color.FromHex("#E8290E");
                    (NoteConnected.Content as Label).Text = "Your are Disconnected";
                    MessageEntry.IsEnabled = false;
                    break;
                case ViewState.Connecting:
                    NoteConnected.BackgroundColor = Color.FromHex("#E8730E");
                    (NoteConnected.Content as Label).Text = "Connecting...";
                    MessageEntry.IsEnabled = false;
                    break;
                case ViewState.Disconnecting:
                    NoteConnected.BackgroundColor = Color.FromHex("#E8730E");
                    (NoteConnected.Content as Label).Text = "Disconnecting...";
                    MessageEntry.IsEnabled = false;
                    break;
                case ViewState.Connected:
                    NoteConnected.BackgroundColor = Color.FromHex("#0BBC4B");
                    (NoteConnected.Content as Label).Text = "Your are Connected Now";
                    MessageEntry.IsEnabled = true;
                    break;
            }
            _state = state;
        }

        private enum ViewState
        {
            Disconnected,
            Connecting,
            Connected,
            Disconnecting
        }
        public ICommand RefreshCommend => new Command(async () => 
        {
            Device.BeginInvokeOnMainThread(() => IsLoading = true);
            await Helper.HubCon.Connection.InvokeAsync("LoadMore", UserName, Messages.Count);
            Device.BeginInvokeOnMainThread(() => IsLoading = false);
        });
        private void DisplayContentView(object sender, EventArgs e)
        {
            StackInsertEmail.IsVisible = false;
        }
    }
}