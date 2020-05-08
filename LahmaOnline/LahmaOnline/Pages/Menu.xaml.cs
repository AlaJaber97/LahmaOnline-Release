using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {

        public Menu()
        {
            InitializeComponent();
            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetStyle();
        }
        private void GetStyle()
        {
            MyOrderButton.IsVisible = AppStatics.UserID > 0;
            MyWishlistButton.IsVisible = AppStatics.UserID > 0;
            LogoEle.IsVisible = AppStatics.UserID > 0;
            ButTermsAndConditions.SetValue(Grid.ColumnProperty, AppStatics.UserID > 0 ? 2 : 1);
            ButTermsAndConditions.SetValue(Grid.RowProperty, AppStatics.UserID > 0 ? 0 : 1);
            imgButtonSigInOut.Source = AppStatics.UserID > 0 ? "logoutGray.svg" : "login.svg";
            txtButtonSigInOut.Text = AppStatics.UserID > 0 ? MultiLanguage.MLResource.Signout : MultiLanguage.MLResource.Signin;
        }

        private void MyOrdersPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.MyOrdersPage());
        }
        private void OpenMyFavoritesPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.MyWishListPage());
        }

        private void OpenLicensePage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.License());
        }

        private void OpenTermsAndConditionsPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.TermsAndConditionsPage());
        }

        private void OpenDeliveryAreasPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.DeliveryAreas());
        }

        private void OpenAboutUsPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.AboutUsPage());
        }

        private void OpenContactUsPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.ContentUsPage());
        }
        private async void SingOutUser(object sender, EventArgs e)
        {
            if (AppStatics.UserID != -1)
            {
                //Sing Out
                new Helper.REtoken().DeleteUserID();
                new Helper.UserInfo().DeleteUserProfile();
                GetStyle();
            }
            else
            {
                //Sing In
                await App._nav.PushAsync(new Pages.LoginPage());
            }
        }

        private void HideMorePage(object sender, EventArgs e)
        {
            this.IsVisible = false;
        }
    }
}