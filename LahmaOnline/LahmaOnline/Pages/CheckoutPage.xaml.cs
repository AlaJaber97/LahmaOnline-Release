using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using BLL.M.Mobile;
using LahmaOnline.StyleViews.Styles;
using Plugin.InputKit.Shared.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutPage : ContentPage
    {
        private Property.CheckoutProperty _CheckoutProperty;
        public Property.CheckoutProperty CheckoutProperty
        {
            get { return _CheckoutProperty; }
            set { _CheckoutProperty = value; OnPropertyChanged(); }
        }
        public Xamarin.Essentials.Placemark PlaceMark { get; set; }
        public CheckoutPage(ViewModel.AddInvoiceWithDetails AddInvoiceWithDetails)
        {
            this.InitializeComponent();
            BindingContext = this;
            CheckoutProperty = new Property.CheckoutProperty
            {
                ModelCart = AddInvoiceWithDetails.ModelCart
            };
            if (AppStatics.UserID != -1)
                fraPass.IsVisible = false;
            UpdateInoive(AddInvoiceWithDetails);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtAddress.Text = CheckoutProperty.InfoLocation.LocationName;
            LoadListCity();
        }
        private async void LoadListCity()
        {
            var responseCreateInvoice = await new Services.HttpExtension<List<BLL.M.Mobile.NationalityOrigin>>().Get("Lookup/3");
            if (responseCreateInvoice != null)
            {
                CheckoutProperty.DeliveryArea = responseCreateInvoice;
            }
        }
        private void UpdateInoive(ViewModel.AddInvoiceWithDetails addInvoiceWithDetails)
        {
            if (addInvoiceWithDetails != null)
            {
                CheckoutProperty.ItemsCount = addInvoiceWithDetails.ItemsCount;
                CheckoutProperty.SubTotal = addInvoiceWithDetails.SubTotal;
                CheckoutProperty.AddtionTax = addInvoiceWithDetails.AddtionTax;
                CheckoutProperty.TotalBeforeTax = addInvoiceWithDetails.TotalBeforeTax;
                CheckoutProperty.TotalTax = addInvoiceWithDetails.TotalTax;
                CheckoutProperty.TotalAmount = addInvoiceWithDetails.TotalAmount;
            }
        }
        private int SelectPaymentMethod()
        {
            foreach (var radio in MethodPayment.Children)
            {
                if (radio is Plugin.InputKit.Shared.Controls.RadioButton)
                    if (((Plugin.InputKit.Shared.Controls.RadioButton)radio).IsChecked)
                        return int.Parse(((Plugin.InputKit.Shared.Controls.RadioButton)radio).Value.ToString());
            }
            return 1;
        }
        private void BackPage(object sender, EventArgs e)
        {
            App._nav.PopAsync(true);
        }
        private async void Submit(object sender, EventArgs e)
        {
            try
            {
                if (!(StoreEmail() & StorePassword() & StoreArFullName() & StoreEnFullName() & StoreMobile() & StoreAddress() & CheckIsAgreeOnTermsAndConditions()))
                {
                    await DisplayAlert("", CheckoutProperty.MessageError, MultiLanguage.MLResource.Ok);
                    return;
                }
                CheckoutProperty.IsLoading = true;
                var DataPasswordEncrpty = Helper.Rfc2898.Encrypt(txtPass.Text);
                CheckoutProperty.ModelCart.CustomerInfo = new BLL.M.Mobile.CustomerInfo
                {
                    Email = txtEmail.Text,
                    EncryptionKey = DataPasswordEncrpty.KeyUsed,
                    Password = DataPasswordEncrpty.Encrypt,
                    ArFullName = txtNameAr.Text,
                    EnFullName = txtName.Text,
                    Mobile = txtMobile.Text,
                };
                CheckoutProperty.ModelCart.CustomerId = AppStatics.UserID > 0 ? AppStatics.UserID : 0;
                CheckoutProperty.ModelCart.Mobile = txtMobile.Text;
                CheckoutProperty.ModelCart.City = int.Parse(CheckoutProperty.DeliverySelect.Code);
                CheckoutProperty.ModelCart.GetMethod = 1;
                CheckoutProperty.ModelCart.PaymentMethod = SelectPaymentMethod();
                CheckoutProperty.ModelCart.Note = string.Empty;
                CheckoutProperty.ModelCart.CustomerClassId = 0;
                CheckoutProperty.ModelCart.DiscountCode = string.Empty;
                CheckoutProperty.ModelCart.ReciveLocation = txtAddress.Text;
                var responseCreateInvoice =
                    await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("SalesInvoice", CheckoutProperty.ModelCart);

                if (responseCreateInvoice == null)
                {
                    await DisplayAlert("", MultiLanguage.MLResource.FailedOpration, MultiLanguage.MLResource.Ok);
                    CheckoutProperty.IsLoading = false;
                    return;
                }

                if (responseCreateInvoice.StatusCode == 200)
                {
                    MainPage.CountBagCart = 0;
                    if (CheckoutProperty.ModelCart.PaymentMethod == 2)
                    {
                        var responsePayment = await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Send($"SalesInvoice/LahmaOnlinePayment?orderID={responseCreateInvoice.Id}&systemLang={AppStatics.Language.ToString()}");
                        if (IsValidURI(responsePayment.Message))
                            await Xamarin.Essentials.Browser.OpenAsync(responsePayment.Message);
                        else
                            await DisplayAlert("", responsePayment?.Message, MultiLanguage.MLResource.Ok);
                    }
                    var Parent = ((NavigationPage)this.Parent).RootPage;
                    var page = ((TabbedPage)Parent).Children[!AppStatics.IsRTL ? 0 : 4];
                    ((TabbedPage)Parent).CurrentPage = page;
                    await DisplayAlert("", responseCreateInvoice.Message, MultiLanguage.MLResource.Ok);
                    CheckoutProperty.IsLoading = false;
                    await App._nav.PopAsync();
                }
                else
                    await DisplayAlert("", responseCreateInvoice.Message, MultiLanguage.MLResource.Ok);
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
            CheckoutProperty.IsLoading = false;
        }
        public static bool IsValidURI(string uri)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                return false;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri tmp))
                return false;
            return tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps;
        }
        private bool CheckIsAgreeOnTermsAndConditions()
        {
            var MessageError = MultiLanguage.MLResource.PleaseAgreeTermsAndConditions;
            var IsContainsInMessageError = CheckoutProperty.MessageError.Contains(MessageError);
            if (IsAgreeOnTermsAndConditions.IsChecked)
            {
                //Remove Message if exisite
                if (IsContainsInMessageError)
                {
                    var IndexString = CheckoutProperty.MessageError.IndexOf($"\n{MessageError}");
                    CheckoutProperty.MessageError = CheckoutProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                }
                return true;
            }
            else
            {
                //Add Message
                if (!IsContainsInMessageError)
                    CheckoutProperty.MessageError += $"\n{MessageError}";
                return false;
            }
        }
        private bool StoreEnFullName()
        {
            var Value = txtName.Text;
            var MessageError = MultiLanguage.MLResource.EnglishNameEmptyMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                var ValidName = true;
                if (ValidName)
                {
                    fraName.BorderColor = Color.FromHex("#3A1D0F");
                    var boolean = CheckoutProperty.MessageError.Contains($"{MessageError}");
                    if (boolean)
                    {
                        var IndexString = CheckoutProperty.MessageError.IndexOf($"\n{MessageError}");
                        CheckoutProperty.MessageError = CheckoutProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                    }
                    return true;
                }
            }
            var IsContainsInMessageError = CheckoutProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                CheckoutProperty.MessageError += $"\n{MessageError}";
            fraName.BorderColor = Color.FromHex("#EF5450");
            return false;
        }
        private bool StoreArFullName()
        {
            var Value = txtNameAr.Text;
            var MessageError = MultiLanguage.MLResource.ArabicNameEmptyMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                var ValidName = true;
                if (ValidName)
                {
                    fraNameAr.BorderColor = Color.FromHex("#3A1D0F");
                    var boolean = CheckoutProperty.MessageError.Contains($"{MessageError}");
                    if (boolean)
                    {
                        var IndexString = CheckoutProperty.MessageError.IndexOf($"\n{MessageError}");
                        CheckoutProperty.MessageError = CheckoutProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                    }
                    return true;
                }
            }
            var IsContainsInMessageError = CheckoutProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                CheckoutProperty.MessageError += $"\n{MessageError}";
            fraNameAr.BorderColor = Color.FromHex("#EF5450");
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
                    fraEmail.BorderColor = Color.FromHex("#3A1D0F");
                    var boolean = CheckoutProperty.MessageError.Contains(MessageError);
                    if (boolean)
                    {
                        var IndexString = CheckoutProperty.MessageError.IndexOf($"\n{MessageError}");
                        CheckoutProperty.MessageError = CheckoutProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                    }
                    return true;
                }
            }
            var IsContainsInMessageError = CheckoutProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                CheckoutProperty.MessageError += $"\n{MessageError}";
            fraEmail.BorderColor = Color.FromHex("#EF5450");
            return false;
        }
        private bool StoreMobile()
        {
            var Value = txtMobile.Text;
            var MessageError = MultiLanguage.MLResource.NumberPhoneMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                bool ValidPhone = Value.All(char.IsDigit) && Value.Length >= 15;

                if (ValidPhone)
                {
                    fraMobile.BorderColor = Color.FromHex("#3A1D0F");
                    var boolean = CheckoutProperty.MessageError.Contains(MessageError);
                    if (boolean)
                    {
                        var IndexString = CheckoutProperty.MessageError.IndexOf($"\n{MessageError}");
                        CheckoutProperty.MessageError = CheckoutProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                    }
                    return true;
                }
            }
            var IsContainsInMessageError = CheckoutProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                CheckoutProperty.MessageError += $"\n{MessageError}";
            fraMobile.BorderColor = Color.FromHex("#EF5450");
            return false;
        }
        private bool StorePassword()
        {
            if (AppStatics.UserID > 0)
                return true;
            var Value = txtPass.Text;
            var MessageError = MultiLanguage.MLResource.PasswordRuleMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                bool ValidPassword = true /*Value.Any(char.IsLower) && Value.Any(char.IsUpper) /*&& Value.Any(char.IsSymbol) && Value.Any(char.IsDigit) && (Value.Length >= 6)*/;
                if (ValidPassword)
                {
                    fraPass.BorderColor = Color.FromHex("#3A1D0F");
                    var boolean = CheckoutProperty.MessageError.Contains($"{MessageError}");
                    if (boolean)
                    {
                        var IndexString = CheckoutProperty.MessageError.IndexOf($"\n{MessageError}");
                        CheckoutProperty.MessageError = CheckoutProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                    }
                    return true;
                }
            }
            var IsContainsInMessageError = CheckoutProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                CheckoutProperty.MessageError += $"\n{MessageError}";
            fraPass.BorderColor = Color.FromHex("#EF5450");
            return false;
        }
        private bool StoreAddress()
        {
            var Value = txtAddress.Text;
            var MessageError = MultiLanguage.MLResource.AddressEmptyMessage;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                fraAddress.BorderColor = Color.FromHex("#3A1D0F");
                var boolean = CheckoutProperty.MessageError.Contains($"{MessageError}");
                if (boolean)
                {
                    var IndexString = CheckoutProperty.MessageError.IndexOf($"\n{MessageError}");
                    CheckoutProperty.MessageError = CheckoutProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                }
                return true;
            }
            var IsContainsInMessageError = CheckoutProperty.MessageError.Contains(MessageError);
            if (!IsContainsInMessageError)
                CheckoutProperty.MessageError += $"\n{MessageError}";
            fraAddress.BorderColor = Color.FromHex("#EF5450");
            return false;
        }
        private void StoreNationality(object sender, EventArgs e)
        {
            var MessageError = MultiLanguage.MLResource.MustSelectEmaritiesMessage;
            var Value = (((Picker)sender).SelectedItem as BLL.M.Mobile.NationalityOrigin);
            if (Value != null && Value.Name != "--Select--")
            {
                fraEmarities.BorderColor = Color.FromHex("#3A1D0F");
                var boolean = CheckoutProperty.MessageError.Contains($"{MessageError}");
                if (boolean)
                {
                    var IndexString = CheckoutProperty.MessageError.IndexOf($"\n{MessageError}");
                    CheckoutProperty.MessageError = CheckoutProperty.MessageError.Remove(IndexString, MessageError.Length + 1);
                }
            }
            else
            {
                var IsContainsInMessageError = CheckoutProperty.MessageError.Contains(MessageError);
                if (!IsContainsInMessageError)
                    CheckoutProperty.MessageError += $"\n{MessageError}";
                fraEmarities.BorderColor = Color.FromHex("#EF5450");
            }
        }
        /// <summary>
        /// Darw Map after get permission
        /// </summary>
        Map MapView;
        private async void OpenMap(object sender, EventArgs e)
        {
            try
            {
                var CurrentUserLocation = await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync();
                var Latitude = CheckoutProperty.InfoLocation.Location.Latitude;
                var Longitude = CheckoutProperty.InfoLocation.Location.Longitude;
                Position position = new Position(Latitude, Longitude);

                if (CurrentUserLocation != null)
                    position = new Position(CurrentUserLocation.Latitude, CurrentUserLocation.Longitude);
                //Create New Map and Add it to page after Get Permission
                MapView = new Map(
                MapSpan.FromCenterAndRadius(
                position, Distance.FromMiles(0.3)))
                {
                    IsShowingUser = true,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,                    
                };
                //MapView.MoveToRegion(new MapSpan(position, position.Latitude, position.Longitude));
                
                var PinMapImage = new FFImageLoading.Svg.Forms.SvgCachedImage
                {
                    Source = "PinMap.svg",
                    Margin = (Thickness)SetterStyle.GetMargin(bottom:3).Value,
                    Style = StyleViews.Styles.GeneralStyles.FFImage32X32Style,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                ContentMap.Children.Add(MapView);
                ContentMap.Children.Add(PinMapImage);


                //Countinue Down 5 => 0 and then select location and Get it's name
                DateTime originalTime = DateTime.Now.AddSeconds(2);
                TimeSpan Time = new TimeSpan();
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    Time = originalTime.Subtract(DateTime.Now);
                    var Countdown = Time.ToString(@"hh\:mm\:ss");
                    if (Countdown == Convert.ToString(TimeSpan.Zero))
                    {
                        GetLocation();
                        if (PlaceMark != null)
                            CheckoutProperty.InfoLocation = new Property.ReciveLocation
                            {
                                LocationName = $"Near {PlaceMark.FeatureName}, {PlaceMark.Locality}, {PlaceMark.CountryCode}",
                                Location = new Xamarin.Essentials.Location
                                {
                                    Latitude = PlaceMark.Location.Latitude,
                                    Longitude = PlaceMark.Location.Longitude
                                }
                            };
                        HintText.Text = CheckoutProperty.InfoLocation.LocationName;
                        originalTime = DateTime.Now.AddSeconds(2);
                    }
                    return MapContentView.IsVisible;
                });
                MapView.PropertyChanged += (s, eM) =>
                {
                    try
                    {
                        if (eM.PropertyName == nameof(MapView.VisibleRegion))
                        {
                            //Reset Timer timer = 5;
                            HintText.Text = string.Empty;
                            HintRefresh.IsRunning = true;
                            originalTime = DateTime.Now.AddSeconds(2);
                        }
                    }
                    catch
                    {

                    }
                };
                MapContentView.IsVisible = true;
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                var boolean = await App._nav.DisplayAlert(MultiLanguage.MLResource.Permission, MultiLanguage.MLResource.PermissionGetLocation, MultiLanguage.MLResource.Allow, MultiLanguage.MLResource.Denied);
                if (boolean)
                {
                    DependencyService.Get<Interface.IAuthorizeCamera>().Authorized();
                }
                else
                {
                    MapContentView.IsVisible = false;
                    _ = App._nav.DisplayAlert(MultiLanguage.MLResource.Error, MultiLanguage.MLResource.FailedMessage, MultiLanguage.MLResource.Ok);
                }
            }
            catch
            {
                MapContentView.IsVisible = false;
                _ = App._nav.DisplayAlert(MultiLanguage.MLResource.Error, MultiLanguage.MLResource.FailedMessage, MultiLanguage.MLResource.Ok);
            }
        }
        private async void GetLocation()
        {
            try
            {
                if (MapView.VisibleRegion != null)
                    PlaceMark = (await Xamarin.Essentials.Geocoding.GetPlacemarksAsync(MapView.VisibleRegion.Center.Latitude, MapView.VisibleRegion.Center.Longitude)).FirstOrDefault();

                HintRefresh.IsRunning = false;
            }
            catch
            {
                // if it can not get name for select location
            }
        }
        private void HideMap(object sender, EventArgs e)
        {
            MapContentView.IsVisible = false;
        }
        private void ConfiremLocation(object sender, EventArgs e)
        {
            CheckoutProperty.ModelCart.Latitude = CheckoutProperty.InfoLocation.Location.Latitude.ToString();
            CheckoutProperty.ModelCart.Longitude = CheckoutProperty.InfoLocation.Location.Longitude.ToString();
            txtAddress.Text = CheckoutProperty.InfoLocation.LocationName;

            (new Helper.UserInfo()).SaveLastReciveLocation(CheckoutProperty.InfoLocation.LocationName, CheckoutProperty.InfoLocation.Location.Latitude, CheckoutProperty.InfoLocation.Location.Longitude);
            MapContentView.IsVisible = false;
        }
        private async void SeacrhAddress_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(CheckoutProperty.GooglePlacesApiAutoComplet.AddressText))
                {
                    await CheckoutProperty.GooglePlacesApiAutoComplet.FindPlaceAsync();
                }
                ListSuggestPlaces.IsVisible = CheckoutProperty.GooglePlacesApiAutoComplet.Addresses.Count > 0;
            }
            catch(Exception ex)
            {
                await DisplayAlert("Not Work", ex.Message, "Ok");
            }
        }
        private void PlacesLocations_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var DataSelect = ((ListView)sender).SelectedItem as AddressInfo;
            MapView.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(DataSelect.Latitude, DataSelect.Longitude), Distance.FromMiles(0.3)));
            ListSuggestPlaces.IsVisible = false;
        }
    }
}