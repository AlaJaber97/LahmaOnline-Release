using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterUser : ContentPage
    {
        public Property.RegisterUserProperty RegisterUserProperty { get; set; } = new Property.RegisterUserProperty();
        public RegisterUser()
        {
            InitializeComponent();
            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<Interface.IStatusBarStyleManager>().SetDarkTheme();
            _ = LoadData();
        }
        private async Task LoadData()
        {
            try
            {
                var responseNationality =
                    await new Services.HttpExtension<List<BLL.M.Mobile.NationalityOrigin>>().Get("Lookup/1");
                RegisterUserProperty.Nationality = new ObservableCollection<BLL.M.Mobile.NationalityOrigin>(responseNationality);
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
        }
        private async Task PostData(BLL.M.Identity.UserProfile model)
        {
            try
            {
                RegisterUserProperty.IsLoading = true;
                var (EncryptPass, KeyUsed) = Helper.Rfc2898.Encrypt(model.Password);
                model.EncryptionKey = KeyUsed;
                model.Password = EncryptPass;
                var ResponesRegisterUser = await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("User", model);
                if (ResponesRegisterUser?.StatusCode == (int)System.Net.HttpStatusCode.OK)
                {
                    var Respones = await new Services.HttpExtension<BLL.M.Identity.ResponseLogin>().Post("User/CheckUserByEmail", new BLL.M.Identity.Login
                    {
                        Mail = model.Email,
                        EncryptionKey = model.EncryptionKey,
                        Password = model.Password
                    });
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
                        await App._nav.DisplayAlert("", Respones?.Message, MultiLanguage.MLResource.Ok);
                }
                else
                    await App._nav.DisplayAlert("", ResponesRegisterUser?.Message, MultiLanguage.MLResource.Ok);
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
            RegisterUserProperty.IsLoading = false;
        }
        private void PreviousFrameRegister(object sender, EventArgs e)
        {
            if (!InfoUserFrame.IsVisible)
            {
                InfoUserFrame2.IsVisible = false;
                ButSubmit.IsVisible = false;
                ButPrevious.IsVisible = false;

                InfoUserFrame.IsVisible = true;
                ButNext.IsVisible = true;
            }
        }
        private void NextFrameRegister(object sender, EventArgs e)
        {
            var CanGoAway = StoreEnFullName() & StoreArFullName() & StoreEmail() & StoreMobile() & StorePassword();
            if (!InfoUserFrame2.IsVisible)
            {
                if (CanGoAway)
                {
                    InfoUserFrame2.IsVisible = true;
                    ButSubmit.IsVisible = true;
                    ButPrevious.IsVisible = true;

                    InfoUserFrame.IsVisible = false;
                    ButNext.IsVisible = false;
                }
                else
                {
                    DisplayAlert("", RegisterUserProperty.MessageError, MultiLanguage.MLResource.Ok);
                }
            }
        }
        private async void SubmitInfoUser(object sender, EventArgs e)
        {
            try
            {
                if (FillModel())
                    _ = PostData(RegisterUserProperty.InfoUserRegister);
                else
                {
                    await DisplayAlert("", RegisterUserProperty.MessageError, MultiLanguage.MLResource.Ok);
                    if (!string.IsNullOrWhiteSpace(RegisterUserProperty.InfoUserRegister.EnFullName) ||
                        !string.IsNullOrWhiteSpace(RegisterUserProperty.InfoUserRegister.ArFullName) ||
                        !string.IsNullOrWhiteSpace(RegisterUserProperty.InfoUserRegister.Email) ||
                        !string.IsNullOrWhiteSpace(RegisterUserProperty.InfoUserRegister.UserName) ||
                        !string.IsNullOrWhiteSpace(RegisterUserProperty.InfoUserRegister.Password))
                        PreviousFrameRegister(null, null);
                }
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
        }
        private bool FillModel()
        {
            return
                StoreEnFullName() &
                StoreArFullName() &
                StoreEmail() &
                StoreMobile() &
                StorePassword() &
                StoreAddress();
        }
        private bool StoreEnFullName()
        {
            var Value = txtEnFullName.Text;
            var MessageError = MultiLanguage.MLResource.EnglishNameEmptyMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                var ValidName = true;
                if (ValidName)
                {
                    RegisterUserProperty.InfoUserRegister.EnFullName = Value;
                    fraEnFullName.BorderColor = Color.FromHex("#3A1D0F");
                    WRenName.IsVisible = false;
                    var boolean = RegisterUserProperty.MessageError.Contains($"{MessageError}");
                    if (boolean)
                    {
                        var IndexString = RegisterUserProperty.MessageError.IndexOf($"\n{MessageError}");
                        RegisterUserProperty.MessageError = RegisterUserProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                    }
                    return true;
                }
            }
            var IsContainsInMessageError = RegisterUserProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                RegisterUserProperty.MessageError += $"\n{MessageError}";
            fraEnFullName.BorderColor = Color.FromHex("#EF5450");
            WRenName.IsVisible = true;
            return false;
        }
        private bool StoreArFullName()
        {
            var Value = txtArFullName.Text;
            var MessageError = MultiLanguage.MLResource.ArabicNameEmptyMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                var ValidName = true;
                if (ValidName)
                {
                    RegisterUserProperty.InfoUserRegister.ArFullName = Value;
                    fraArFullName.BorderColor = Color.FromHex("#3A1D0F");
                    WRarName.IsVisible = false;
                    var boolean = RegisterUserProperty.MessageError.Contains($"{MessageError}");
                    if (boolean)
                    {
                        var IndexString = RegisterUserProperty.MessageError.IndexOf($"\n{MessageError}");
                        RegisterUserProperty.MessageError = RegisterUserProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                    }
                    return true;
                }
            }
            var IsContainsInMessageError = RegisterUserProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                RegisterUserProperty.MessageError += $"\n{MessageError}";
            fraArFullName.BorderColor = Color.FromHex("#EF5450");
            WRarName.IsVisible = true;
            return false;
        }
        private bool StoreEmail()
        {
            var Value = txtEmail.Text;
            var MessageError = MultiLanguage.MLResource.EmailFormatMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                var ValidEmail = (new System.Net.Mail.MailAddress(Value)).Address == Value;
                if (ValidEmail)
                {
                    RegisterUserProperty.InfoUserRegister.Email = RegisterUserProperty.InfoUserRegister.UserName = Value;
                    fraEmail.BorderColor = Color.FromHex("#3A1D0F");
                    WRemail.IsVisible = false;
                    var boolean = RegisterUserProperty.MessageError.Contains(MessageError);
                    if (boolean)
                    {
                        var IndexString = RegisterUserProperty.MessageError.IndexOf($"\n{MessageError}");
                        RegisterUserProperty.MessageError = RegisterUserProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                    }
                    return true;
                }
            }
            var IsContainsInMessageError = RegisterUserProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                RegisterUserProperty.MessageError += $"\n{MessageError}";
            fraEmail.BorderColor = Color.FromHex("#EF5450");
            WRemail.IsVisible = true;
            return false;
        }
        private bool StoreMobile()
        {
            var Value = txtMobile.Text;
            var MessageError = MultiLanguage.MLResource.NumberPhoneMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                bool ValidPhone = Value.All(char.IsDigit) && Value.Length >= 10;

                if (ValidPhone)
                {
                    RegisterUserProperty.InfoUserRegister.Mobile = Value;
                    fraMobile.BorderColor = Color.FromHex("#3A1D0F");
                    WRmobile.IsVisible = false;
                    var boolean = RegisterUserProperty.MessageError.Contains(MessageError);
                    if (boolean)
                    {
                        var IndexString = RegisterUserProperty.MessageError.IndexOf($"\n{MessageError}");
                        RegisterUserProperty.MessageError = RegisterUserProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                    }
                    return true;
                }
            }
            var IsContainsInMessageError = RegisterUserProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                RegisterUserProperty.MessageError += $"\n{MessageError}";
            fraMobile.BorderColor = Color.FromHex("#EF5450");
            WRmobile.IsVisible = true;
            return false;
        }
        private bool StorePassword()
        {
            var Value = txtPassword.Text;
            var MessageError = MultiLanguage.MLResource.PasswordRuleMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                bool ValidPassword = true /*Value.Any(char.IsLower) && Value.Any(char.IsUpper) && Value.Any(char.IsSymbol) &&  Value.Any(char.IsDigit) && (Value.Length >= 6)*/;
                if (ValidPassword)
                {
                    RegisterUserProperty.InfoUserRegister.Password = Value;
                    fraPassword.BorderColor = Color.FromHex("#3A1D0F");
                    WRpassword.IsVisible = false;
                    var boolean = RegisterUserProperty.MessageError.Contains($"{MessageError}");
                    if (boolean)
                    {
                        var IndexString = RegisterUserProperty.MessageError.IndexOf($"\n{MessageError}");
                        RegisterUserProperty.MessageError = RegisterUserProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                    }
                    return true;
                }
            }
            var IsContainsInMessageError = RegisterUserProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                RegisterUserProperty.MessageError += $"\n{MessageError}";
            fraPassword.BorderColor = Color.FromHex("#EF5450");
            WRpassword.IsVisible = true;
            return false;
        }
        private bool StoreAddress()
        {
            var Value = txtAddress.Text;
            var MessageError = MultiLanguage.MLResource.AddressEmptyMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                RegisterUserProperty.InfoUserRegister.Address = Value;
                fraAddress.BorderColor = Color.FromHex("#3A1D0F");
                WRaddress.IsVisible = false;
                var boolean = RegisterUserProperty.MessageError.Contains($"{MessageError}");
                if (boolean)
                {
                    var IndexString = RegisterUserProperty.MessageError.IndexOf($"\n{MessageError}");
                    RegisterUserProperty.MessageError = RegisterUserProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                }
                return true;
            }
            var IsContainsInMessageError = RegisterUserProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                RegisterUserProperty.MessageError += $"\n{MessageError}";
            fraAddress.BorderColor = Color.FromHex("#EF5450");
            WRaddress.IsVisible = true;
            return false;
        }
        private void StoreNationality(object sender, EventArgs e)
        {
            var MessageError = MultiLanguage.MLResource.MustSelectNationalityMessage;
            var Value = (((Picker)sender).SelectedItem as BLL.M.Mobile.NationalityOrigin);
            if (Value.Name != "--Select--")
            {
                RegisterUserProperty.InfoUserRegister.NationalityId = Value.Id;
                fraNationality.BorderColor = Color.FromHex("#3A1D0F");
                WRnationality.IsVisible = false;
                var boolean = RegisterUserProperty.MessageError.Contains($"{MessageError}");
                if (boolean)
                {
                    var IndexString = RegisterUserProperty.MessageError.IndexOf($"\n{MessageError}");
                    RegisterUserProperty.MessageError = RegisterUserProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                }
            }
            else
            {
                var IsContainsInMessageError = RegisterUserProperty.MessageError.Contains(MessageError);
                if (!IsContainsInMessageError)
                    RegisterUserProperty.MessageError += $"\n{MessageError}";
                fraNationality.BorderColor = Color.FromHex("#EF5450");
                WRnationality.IsVisible = true;
            }
        }
        private void StoreGender(object sender, EventArgs e)
        {
            var MessageError = MultiLanguage.MLResource.MustSelectGenderMessage;
            if (RegisterUserProperty.GenderSelected != null)
            {
                RegisterUserProperty.InfoUserRegister.Gender = (RegisterUserProperty.GenderSelected == MultiLanguage.MLResource.Male ? 1 : 2);
                fraGender.BorderColor = Color.FromHex("#3A1D0F");
                WRgender.IsVisible = false;
                var boolean = RegisterUserProperty.MessageError.Contains($"{MessageError}");
                if (boolean)
                {
                    var IndexString = RegisterUserProperty.MessageError.IndexOf($"\n{MessageError}");
                    RegisterUserProperty.MessageError = RegisterUserProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                }
                return;
            }
            var IsContainsInMessageError = RegisterUserProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                RegisterUserProperty.MessageError += $"\n{MessageError}";
            fraGender.BorderColor = Color.FromHex("#EF5450");
            WRgender.IsVisible = true;
        }
        private void StoreBirthday(object sender, DateChangedEventArgs e)
        {
            var MessageError = MultiLanguage.MLResource.BirthdayRuleMessage;
            var Value = ((DatePicker)sender).Date;
            var ValidDate = Value.Year < DateTime.Now.Year - 4;
            if (ValidDate)
            {
                RegisterUserProperty.InfoUserRegister.Bod = Value;
                fraBirthday.BorderColor = Color.FromHex("#3A1D0F");
                WRbirthday.IsVisible = false;
                var boolean = RegisterUserProperty.MessageError.Contains($"{MessageError}");
                if (boolean)
                {
                    var IndexString = RegisterUserProperty.MessageError.IndexOf($"\n{MessageError}");
                    RegisterUserProperty.MessageError = RegisterUserProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                }
                return;
            }
            var IsContainsInMessageError = RegisterUserProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                RegisterUserProperty.MessageError += $"\n{MessageError}";
            fraBirthday.BorderColor = Color.FromHex("#EF5450");
            WRbirthday.IsVisible = true;
        }
        private void OpenSingInPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.LoginPage());
        }

    }
}