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
    public partial class ProfilePage : ContentPage
    {
        private Property.ProfileProperty _ProfileProperty;
        public Property.ProfileProperty ProfileProperty
        {
            get { return _ProfileProperty; }
            set { _ProfileProperty = value; OnPropertyChanged(); }
        }
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ProfileProperty = new Property.ProfileProperty();
            if (AppStatics.UserID <= 0)
            {
                imgSetting.IsVisible = true;
                imgPincelEdit.IsVisible = false;
                StackUserInfo.IsVisible = false;
            }
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<Interface.IStatusBarStyleManager>().SetLightTheme();
        }
        private void ShowEditFrame(object sender, EventArgs e)
        {
            var Stack = (StackLayout)sender;
            var arrow = Stack.Children[2] as FFImageLoading.Svg.Forms.SvgCachedImage;
            var label = Stack.Children[1] as Label;
            var Parent = (Stack.Parent as StackLayout).Children;
            if (label.Text == MultiLanguage.MLResource.Language || AppStatics.UserID != -1)
            {
                var rotationOriginDegree = !AppStatics.IsRTL ? 0 : 180;
                if (!Parent[1].IsVisible)
                    arrow.RotateTo(90, 250);
                else
                    arrow.RotateTo(rotationOriginDegree, 250);

                Parent[1].IsVisible = !Parent[1].IsVisible;
            }
            else
                HideMessageLogin(null, null);
        }
        private async void HideMessageLogin(object sender, EventArgs e)
        {
            if (HolderMessageLogin.IsVisible)
            {
                HolderMessageLogin.IsVisible = false;
                await PopupMessageLogin.ScaleTo(0, 250);
                PopupMessageLogin.IsVisible = false;
            }
            else
            {
                HolderMessageLogin.IsVisible = true;
                PopupMessageLogin.IsVisible = true;
                await PopupMessageLogin.ScaleTo(1, 250);
            }
        }
        private void OpenLoginPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.LoginPage());
        }
        private async void HideMessageRestart(object sender, EventArgs e)
        {
            if (HolderMessageRestart.IsVisible)
            {
                HolderMessageRestart.IsVisible = false;
                await PopupMessageRestart.ScaleTo(0, 250);
                PopupMessageRestart.IsVisible = false;
            }
            else
            {
                HolderMessageRestart.IsVisible = true;
                PopupMessageRestart.IsVisible = true;
                await PopupMessageRestart.ScaleTo(1, 250);
            }
        }
        private void EditLanguage(object sender, EventArgs e)
        {
            var langID = ProfileProperty.LanguageIndex + 1;
            (new Helper.UserInfo()).SetLanguage(langID);
            var StackLayout = ((Button)sender).Parent;
            var Frame = (Frame)StackLayout.Parent;
            var StackLayoutInner = (StackLayout)Frame.Parent;
            var StackLayoutOuter = (StackLayout)StackLayoutInner.Parent;
            var StackLayoutFrame = (StackLayout)StackLayoutOuter.Children[0];
            ShowEditFrame(StackLayoutFrame, null);
            HideMessageRestart(null, null);
        }

        private async void EditAddress(object sender, EventArgs e)
        {
            ProfileProperty.UserProfile.Address = ProfileProperty.AddressSelected;
            var StackLayout = ((Button)sender).Parent;
            var Frame = (Frame)StackLayout.Parent;
            var StackLayoutInner = (StackLayout)Frame.Parent;
            var StackLayoutOuter = (StackLayout)StackLayoutInner.Parent;
            var StackLayoutFrame = (StackLayout)StackLayoutOuter.Children[0];
            if (await PostData())
                ShowEditFrame(StackLayoutFrame, null);

            ProfileProperty.IsLoading = false;
        }

        private async void EditBirthday(object sender, EventArgs e)
        {
            ProfileProperty.UserProfile.Bod = ProfileProperty.Birthday;
            var StackLayout = ((Button)sender).Parent;
            var Frame = (Frame)StackLayout.Parent;
            var StackLayoutInner = (StackLayout)Frame.Parent;
            var StackLayoutOuter = (StackLayout)StackLayoutInner.Parent;
            var StackLayoutFrame = (StackLayout)StackLayoutOuter.Children[0];
            if (await PostData())
                ShowEditFrame(StackLayoutFrame, null);

            ProfileProperty.IsLoading = false;
        }
        private async void EditGender(object sender, EventArgs e)
        {
            ProfileProperty.UserProfile.Gender = ProfileProperty.GenderSelected == MultiLanguage.MLResource.Male ? 1 : 2;
            var StackLayout = ((Button)sender).Parent;
            var Frame = (Frame)StackLayout.Parent;
            var StackLayoutInner = (StackLayout)Frame.Parent;
            var StackLayoutOuter = (StackLayout)StackLayoutInner.Parent;
            var StackLayoutFrame = (StackLayout)StackLayoutOuter.Children[0];

            if (await PostData())
                ShowEditFrame(StackLayoutFrame, null);

            ProfileProperty.IsLoading = false;
        }
        private async void EditBasicInformation(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEnFullName.Text))
            {
                var MessageError = MultiLanguage.MLResource.EnglishNameEmptyMessage;
                await DisplayAlert("", MessageError, MultiLanguage.MLResource.Ok);
                return;
            }
            ProfileProperty.UserProfile.EnFullName = txtEnFullName.Text;

            if (string.IsNullOrEmpty(txtArFullName.Text))
            {
                var MessageError = MultiLanguage.MLResource.ArabicNameEmptyMessage;
                await DisplayAlert("", MessageError, MultiLanguage.MLResource.Ok);
                return;
            }
            ProfileProperty.UserProfile.ArFullName = txtArFullName.Text;

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                var MessageError = MultiLanguage.MLResource.EmailFormatMessage;
                await DisplayAlert("", MessageError, MultiLanguage.MLResource.Ok);
                return;
            }
            ProfileProperty.UserProfile.Email = txtEmail.Text;

            if (string.IsNullOrEmpty(txtMobile.Text) || txtMobile.Text.Length < 10 || !txtMobile.Text.All(char.IsDigit))
            {;
                var MessageError = MultiLanguage.MLResource.NumberPhoneMessage;
                await DisplayAlert("", MessageError, MultiLanguage.MLResource.Ok);
                return;
            }
            ProfileProperty.UserProfile.Mobile = txtMobile.Text;
            if (await PostData())
                ShowPopupBasicInfo(null, null);

            ProfileProperty.IsLoading = false;
        }
        private async Task<bool> PostData()
        {
            ProfileProperty.IsLoading = true;
            try
            {
                ProfileProperty.UserProfile.Id = AppStatics.UserID;

                var ResponesEdit = await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("User", ProfileProperty.UserProfile);
                if (ResponesEdit.StatusCode == (int)System.Net.HttpStatusCode.OK)
                {
                    (new Helper.UserInfo()).SetUserProfile(ProfileProperty.UserProfile);
                    AppStatics.UserProfile = ProfileProperty.UserProfile;
                    await DisplayAlert("", ResponesEdit.Message, MultiLanguage.MLResource.Ok);
                    return true;
                }
                await DisplayAlert("", ResponesEdit.Message, MultiLanguage.MLResource.Ok);
            }
            catch (Exception ex)
            {
                if (Xamarin.Essentials.Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.InternetConnectionTitle, MultiLanguage.MLResource.InternetConnectionMessage, MultiLanguage.MLResource.Ok);
                else
#if DEBUG
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, ex.ToString(), MultiLanguage.MLResource.Ok);
#elif RELEASE
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, MultiLanguage.MLResource.FailedMessage, MultiLanguage.MLResource.Ok);
#endif
            };
            return false;
        }
        private async void EditPassword(object sender, EventArgs e)
        {
            try
            {
                var StackLayout = ((Button)sender).Parent;
                var Frame = (Frame)StackLayout.Parent;
                var StackLayoutInner = (StackLayout)Frame.Parent;
                var StackLayoutOuter = (StackLayout)StackLayoutInner.Parent;
                var StackLayoutFrame = (StackLayout)StackLayoutOuter.Children[0];

                var MessageError = MultiLanguage.MLResource.PasswordRuleMessage;
                var Value = txtNewPassword.Text;
                bool ValidPassword =true /*Value.Any(char.IsLower) && Value.Any(char.IsUpper) /*&& Value.Any(char.IsSymbol) && Value.Any(char.IsDigit) && (Value.Length >= 6)*/;
                if (!ValidPassword)
                {
                    await DisplayAlert("", MessageError, MultiLanguage.MLResource.Ok);
                    return;
                }
                var DataOldPasswordEncrypt = Helper.Rfc2898.Encrypt(txtCurrentPass.Text);
                var DataNewPasswordEncrypt = Helper.Rfc2898.Encrypt(txtNewPassword.Text, DataOldPasswordEncrypt.KeyUsed);

                var modelChangePass = new BLL.M.Mobile.ChangeUserPassword
                {
                    UserName = ProfileProperty.UserProfile.UserName,
                    EncryptionKey = DataOldPasswordEncrypt.KeyUsed,
                    OldPassword = DataOldPasswordEncrypt.Encrypt,
                    Password = DataNewPasswordEncrypt.Encrypt
                };

                ProfileProperty.IsLoading = true;
                var responseChangePass = (await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("User/ChangePassword", modelChangePass));
                ShowEditFrame(StackLayoutFrame, null);
                await DisplayAlert("", responseChangePass.Message, MultiLanguage.MLResource.Ok);
            }
            catch (Exception ex)
            {
                if (Xamarin.Essentials.Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.InternetConnectionTitle, MultiLanguage.MLResource.InternetConnectionMessage, MultiLanguage.MLResource.Ok);
                else
#if DEBUG
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, ex.ToString(), MultiLanguage.MLResource.Ok);
#elif RELEASE
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, MultiLanguage.MLResource.FailedMessage, MultiLanguage.MLResource.Ok);
#endif
            }
            ProfileProperty.IsLoading = false;
        }

        private void ShowPopupBasicInfo(object sender, EventArgs e)
        {
            var InverseBool = !HolderEditInfoUser.IsVisible;
            HolderEditInfoUser.IsVisible = PopupEditInfoUser.IsVisible = InverseBool;
        }
    }
}