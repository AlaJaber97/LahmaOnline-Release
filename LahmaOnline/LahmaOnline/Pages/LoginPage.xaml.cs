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
    public partial class LoginPage : ContentPage
    {
        public Property.LoginProperty LoginProperty { get; set; } = new Property.LoginProperty();
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<Interface.IStatusBarStyleManager>().SetDarkTheme();
        }
        private async void LoginAction(object sender, EventArgs e)
        {
            try
            {
                if (IsFieldEmailEmpty() | IsFieldPasswordEmpty())
                {
                    DisplayAlert("", LoginProperty.MessageError, MultiLanguage.MLResource.Ok);
                    return;
                }
                LoginProperty.IsLoading = true;

                var (EncryptPass, KeyUsed) = Helper.Rfc2898.Encrypt(txtPassword.Text);
                var model = new BLL.M.Identity.Login
                {
                    Mail = txtEmail.Text,
                    EncryptionKey = KeyUsed,
                    Password = EncryptPass
                };
                var Respones = await new Services.HttpExtension<BLL.M.Identity.ResponseLogin>().Post("User/CheckUserByEmail", model);
                if (!string.IsNullOrWhiteSpace(Respones?.Token))
                {
                    await new Helper.REtoken().SetUserID(default, Respones.User.Id);
                    await new Helper.REtoken().SetUserToken(Respones.Token);
                    (new Helper.UserInfo()).SetUserProfile(Respones.User);
                    var modelCartMove = new BLL.M.Identity.GetCartByUserGustID
                    {
                        GuestId = AppStatics.GestID.ToString(),
                        UserId = Respones.User.Id
                    };
                    await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("AddToCart/MoveCartToCustomer", modelCartMove);
                    Application.Current.MainPage = App._nav = new NavigationPage(new MainPage());
                }
                else
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, Respones?.Message, MultiLanguage.MLResource.Ok);
            }
            catch (Exception ex)
            {
                if (Xamarin.Essentials.Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.InternetConnectionTitle, MultiLanguage.MLResource.InternetConnectionMessage, MultiLanguage.MLResource.Ok);
                else
#if DEBUG
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, ex.ToString(), MultiLanguage.MLResource.Ok);
#elif RELEASE
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, MultiLanguage.MLResource.FailedMessage , MultiLanguage.MLResource.Ok);
#endif
            }
            LoginProperty.IsLoading = false;
        }

        private void OpenRegisterPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.RegisterUser());
        }

        private void OpenHomePage(object sender, EventArgs e)
        {
            Application.Current.MainPage = App._nav = new NavigationPage(new MainPage());
        }

        private void ForgetPassword(object sender, EventArgs e)
        {
            ForgetPopup.IsVisible = !ForgetPopup.IsVisible;
        }

        private async void SendLoginLink(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmailforgetuser.Text))
                {
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.MissingValue, MultiLanguage.MLResource.ErrorMissingValue, MultiLanguage.MLResource.Ok);
                    return;
                }
                LoginProperty.IsLoading = true;
                var model = new BLL.M.Identity.ResetPasswordUserDto
                {
                    UserName = txtEmailforgetuser.Text,
                    Password = string.Empty
                };
                var Respones = await new Services.HttpExtension<BLL.M.Identity.ResetPasswordUserDto>().PostReturnStatusCode("User/SendNotificationForgetPassword", model);
                if (Respones == System.Net.HttpStatusCode.OK)
                {
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.DONE, $"{MultiLanguage.MLResource.Thanks_PleaseCheck} {txtEmailforgetuser.Text} {MultiLanguage.MLResource.ForLinkToResetYourPassword}", MultiLanguage.MLResource.Ok);
                    ForgetPassword(null, null);
                }
                else
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, MultiLanguage.MLResource.FailedMessage, MultiLanguage.MLResource.Ok);
            }
            catch (Exception ex)
            {
                if (Xamarin.Essentials.Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.InternetConnectionTitle, MultiLanguage.MLResource.InternetConnectionMessage, MultiLanguage.MLResource.Ok);
                else
#if DEBUG
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, ex.ToString(), MultiLanguage.MLResource.Ok);
#elif RELEASE
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, MultiLanguage.MLResource.FailedMessage , MultiLanguage.MLResource.Ok);
#endif
            }
            LoginProperty.IsLoading = false;
        }
        private bool IsFieldEmailEmpty()
        {
            var Value = txtEmail.Text;
            var MessageError = MultiLanguage.MLResource.EmailFormatMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                var ValidEmail = (new System.Net.Mail.MailAddress(Value)).Address == Value;
                if (ValidEmail)
                {
                    fraEmail.BorderColor = Color.FromHex("#3A1D0F");
                    var boolean = LoginProperty.MessageError.Contains(MessageError);
                    if (boolean)
                    {
                        var IndexString = LoginProperty.MessageError.IndexOf($"\n{MessageError}");
                        LoginProperty.MessageError = LoginProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                    }
                    return false;
                }
            }
            var IsContainsInMessageError = LoginProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                LoginProperty.MessageError += $"\n{MessageError}";
            fraEmail.BorderColor = Color.FromHex("#EF5450");
            return true;
        }
        
        private bool IsFieldPasswordEmpty()
        {
            var Value = txtPassword.Text;
            var MessageError = MultiLanguage.MLResource.PasswordRuleMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                fraPass.BorderColor = Color.FromHex("#3A1D0F");
                var boolean = LoginProperty.MessageError.Contains($"{MessageError}");
                if (boolean)
                {
                    var IndexString = LoginProperty.MessageError.IndexOf($"\n{MessageError}");
                    LoginProperty.MessageError = LoginProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                }
                return false;
            }
            var IsContainsInMessageError = LoginProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                LoginProperty.MessageError += $"\n{MessageError}";
            fraPass.BorderColor = Color.FromHex("#EF5450");
            return true;
        }
    }
}