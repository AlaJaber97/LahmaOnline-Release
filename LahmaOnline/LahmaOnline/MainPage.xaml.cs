using LahmaOnline.Helper;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Plugin.Badge.Abstractions;
//using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        static Pages.BagCartPage pageBagCartPage = new Pages.BagCartPage()
        {
            IconImageSource = "BagButUnActive.png",
            Title = MultiLanguage.MLResource.MyCart
        };
        static Pages.HomePage pageHomePage = new Pages.HomePage
        {
            IconImageSource = "StoreButActive.png",
            Title = MultiLanguage.MLResource.Store
        };
        static Pages.ProductList pageProductList = new Pages.ProductList
        {
            IconImageSource = "Component.png",
            Title = MultiLanguage.MLResource.Search
        };
        static Pages.ProfilePage pageProfilePage = new Pages.ProfilePage
        {
            IconImageSource = "ProfileButUnActive.png",
            Title = MultiLanguage.MLResource.Profile
        };
        static Pages.Menu pageMenu = new Pages.Menu
        {
            IconImageSource = "MoreButUnAcive.png",
            Title = MultiLanguage.MLResource.More
        };

        private static int _CountBagCart;
        public static int CountBagCart
        {
            get { return _CountBagCart; }
            set
            {
                _CountBagCart = value;
                TabBadge.SetBadgeText(pageBagCartPage, value.ToString());
                Xamarin.Essentials.Preferences.Set("#ItemInCart", _CountBagCart);
            }
        }
        enum TabbedPages
        {
            HomePage,
            BagCartPage,
            ProductList,
            ProfilePage,
            Menu
        }
        
        public MainPage()
        {
            InitializeComponent();

            if (!AppStatics.IsRTL)
            {
                this.Children.Add(pageHomePage);
                this.Children.Add(pageBagCartPage);
                this.Children.Add(pageProductList);
                this.Children.Add(pageProfilePage);
                this.Children.Add(pageMenu);
            }
            else
            {
                this.Children.Add(pageMenu);
                this.Children.Add(pageProfilePage);
                this.Children.Add(pageProductList);
                this.Children.Add(pageBagCartPage);
                this.Children.Add(pageHomePage);
            }

            int ItemNumber = Xamarin.Essentials.Preferences.Get("#ItemInCart", 0);
            TabBadge.SetBadgeText(pageBagCartPage, ItemNumber.ToString());
            TabBadge.SetBadgeTextColor(pageBagCartPage, Color.White);
            TabBadge.SetBadgeColor(pageBagCartPage, Color.FromHex("#4D3A2D"));

            this.CurrentPage = pageHomePage;

            this.CurrentPageChanged += (s, e) =>
            {
                pageHomePage.IconImageSource = "StoreButUnActive.png";
                pageBagCartPage.IconImageSource = "BagButUnActive.png";
                pageProductList.IconImageSource = "Component.png";
                pageProfilePage.IconImageSource = "ProfileButUnActive.png";
                pageMenu.IconImageSource = "MoreButUnAcive.png";
                switch ((s as TabbedPage).CurrentPage.ToString())
                {
                    case "LahmaOnline.Pages.HomePage":
                        pageHomePage.IconImageSource = "StoreButActive.png";
                        break;
                    case "LahmaOnline.Pages.BagCartPage":
                        pageBagCartPage.IconImageSource = "BagButActive.png";
                        break;
                    case "LahmaOnline.Pages.ProductList":
                        pageProductList.IconImageSource = "ComponentActiveSearch.png";
                        break;
                    case "LahmaOnline.Pages.ProfilePage":
                        pageProfilePage.IconImageSource = "ProfileButActive.png";
                        break;
                    case "LahmaOnline.Pages.Menu":
                        pageMenu.IconImageSource = "MoreButActive.png";
                        break;
                };
            };

            if (!string.IsNullOrEmpty(AppStatics.UserProfile?.Email))
            {
                var userName = AppStatics.UserProfile.Email;
                HubCon.Connection.InvokeAsync("Connect", userName);
                NotificationIsPlaying();
            }

           // if (Device.RuntimePlatform != Device.UWP)
           //     NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;
        }

        public static void NotificationIsPlaying()
        {
            try
            {
                HubCon.Connection.On<string>("ReceiveMessage", (message) =>
                {
                    var msg = JsonConvert.DeserializeObject<BLL.M.Mobile.MessageModel>(message);
                    if (Device.RuntimePlatform != Device.UWP)
                    {
                        if (App._nav.CurrentPage is MainPage)
                        {
                            ShowNotification();
                        }
                    }
                });
            }
            catch
            {

            }
        }

        private static void ShowNotification()
        {
            //var notification = new NotificationRequest
            //{
            //    Title = "LahmaOnline",
            //    Description = $"There new message for you. Please Check your inbox",
            //    NotifyTime = DateTime.Now.AddSeconds(30)
            //};
            //NotificationCenter.Current.Show(notification);
        }

        //private void OnLocalNotificationTapped(NotificationTappedEventArgs e)
        //{
        //    App._nav.PushAsync(new Pages.ChatPage());
        //}
    }
}