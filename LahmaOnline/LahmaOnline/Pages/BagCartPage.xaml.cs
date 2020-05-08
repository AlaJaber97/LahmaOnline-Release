using LahmaOnline.Property;
using Plugin.InputKit.Shared.Controls;
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
    public partial class BagCartPage : ContentPage
    {
        public BagCartProperty BagCartProperty { get; set; } = new BagCartProperty();


        public BagCartPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListCart.BeginRefresh();
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<Interface.IStatusBarStyleManager>().SetDarkTheme();
            LoadData(null,null);
        }
        private async void LoadData(object sender, EventArgs e)
        {
            try
            {
                BagCartProperty.IsLoading = true;
                var modelUser = new BLL.M.Identity.GetCartByUserGustID
                {
                    UserId = AppStatics.UserID != -1 ? AppStatics.UserID : 0,
                    GuestId = AppStatics.UserID != -1 ? "0" : AppStatics.GestID.ToString(),
                };
                var responseContentCart =
                    await new Services.HttpExtension<List<BLL.M.Mobile.CartBag>>().Post("AddToCart/GetCartByCustomerGuestID", modelUser);
                #region Get Product In Cart
                    BagCartProperty.InfoBagCart.ProductInCart = responseContentCart == null ? null : new ObservableCollection<BLL.M.Mobile.CartBag>(responseContentCart);
                MainPage.CountBagCart = responseContentCart == null ? 0 : responseContentCart.Count();
                if (BagCartProperty.SettingOrganization == null)
                {
                    var SettingOrganization = await new Services.HttpExtension<BLL.M.Mobile.GetSettingOrganizationDto>().Get($"SettingOrganization/1");
                    BagCartProperty.SettingOrganization = SettingOrganization;
                }
                UpdateInoive();
                #endregion
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
            BagCartProperty.IsLoading = false;
        }

        private async void DeleteItemFromCart(object sender, EventArgs e)
        {
            BagCartProperty.IsLoading = true;
            var Context = ((FFImageLoading.Svg.Forms.SvgCachedImage)sender).BindingContext;
            var Product = Context as BLL.M.Mobile.CartBag;
            try
            {
                var modelRemoveItem = new List<BLL.M.Mobile.DeleteFromCart>
                {
                    new BLL.M.Mobile.DeleteFromCart
                    {
                        RecordId = Product.Id
                    }
                };
                var responseContentCart =
                    await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("AddToCart/DeleteAddToCart", modelRemoveItem);
                if (responseContentCart?.StatusCode == 200)
                {
                    if(MainPage.CountBagCart > 0) MainPage.CountBagCart--;
                    BagCartProperty.InfoBagCart.ProductInCart.Remove(Product); UpdateInoive();
                }
                else
                    await DisplayAlert(MultiLanguage.MLResource.Error, responseContentCart?.Message, MultiLanguage.MLResource.Ok);
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
            BagCartProperty.IsLoading = false;
        }
        private async void AcceptEdit(object sender, EventArgs e)
        {
            try
            {
                if (BagCartProperty.ProductSelect.Qty < 1)
                    BagCartProperty.ProductSelect.Qty = 1;
                var model = new BLL.M.Mobile.AddToCart
                {
                    Id = BagCartProperty.ProductSelect.Id,
                    UserId = AppStatics.UserID != -1 ? AppStatics.UserID : 0,
                    GuestId = AppStatics.UserID != -1 ? "0" : AppStatics.GestID.ToString(),
                    ItemId = BagCartProperty.ProductSelect.ItemId,
                    Fat = FatOptionSelect(),
                    Size = SizeOptionSelect(),
                    Nature = NatureOptionSelect(),
                    Qty = Convert.ToDecimal(BagCartProperty.ProductSelect.Qty),
                    Note = BagCartProperty.ProductSelect.Note
                };
                var responseAddToCart =
                                await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("AddToCart", model);
                if (responseAddToCart?.StatusCode == 200)
                {
                    var item = BagCartProperty.InfoBagCart.ProductInCart.FirstOrDefault(xitem => xitem.ItemId == BagCartProperty.ProductSelect.ItemId);
                    if (item != null)
                    {
                        var oldIndex = BagCartProperty.InfoBagCart.ProductInCart.IndexOf(item);
                        BagCartProperty.InfoBagCart.ProductInCart[oldIndex] = BagCartProperty.ProductSelect;
                    }
                    PopupOrderProduct(sender, e); UpdateInoive();
                }
                else
                    await DisplayAlert(MultiLanguage.MLResource.Error, responseAddToCart?.Message, MultiLanguage.MLResource.Ok);
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

        }
        private void UpdateInoive()
        {
            if (BagCartProperty.InfoBagCart.ProductInCart != null)
            {
                var count = BagCartProperty.InfoBagCart.ProductInCart.Count;
                BagCartProperty.InfoBagCart.ItemsCount = count;
                BagCartProperty.InfoBagCart.SubTotal = BagCartProperty.InfoBagCart.ProductInCart.Sum(item => item.TotalAmount);
                BagCartProperty.InfoBagCart.AddtionTax = BagCartProperty.InfoBagCart.SubTotal < (double)BagCartProperty.SettingOrganization.MinTotalOrder ? (double)BagCartProperty.SettingOrganization.ChargeDelivery : 0;
                BagCartProperty.InfoBagCart.TotalBeforeTax = BagCartProperty.InfoBagCart.SubTotal + BagCartProperty.InfoBagCart.AddtionTax;
                BagCartProperty.InfoBagCart.TotalTax = BagCartProperty.InfoBagCart.SubTotal * (double)BagCartProperty.SettingOrganization.Tax/100.00;
                BagCartProperty.InfoBagCart.TotalAmount = BagCartProperty.InfoBagCart.TotalBeforeTax + BagCartProperty.InfoBagCart.TotalTax;
            }
        }
        private int FatOptionSelect()
        {
            if (BagCartProperty.ProductSelect.ItemInfo.IsFat)
                foreach (var radio in FatOption.Children)
                {
                    if (radio is Plugin.InputKit.Shared.Controls.RadioButton)
                        if (((Plugin.InputKit.Shared.Controls.RadioButton)radio).IsChecked)
                            return int.Parse(((Plugin.InputKit.Shared.Controls.RadioButton)radio).Value.ToString());
                }
            return 0;
        }
        private int SizeOptionSelect()
        {
            if (BagCartProperty.ProductSelect.ItemInfo.IsSizing)
                foreach (var radio in SizeOption.Children)
                {
                    if (radio is Plugin.InputKit.Shared.Controls.RadioButton)
                        if (((Plugin.InputKit.Shared.Controls.RadioButton)radio).IsChecked)
                            return int.Parse(((Plugin.InputKit.Shared.Controls.RadioButton)radio).Value.ToString());
                }
            return 0;
        }
        private int NatureOptionSelect()
        {
            if (BagCartProperty.ProductSelect.ItemInfo.IsNature)
                foreach (var radio in SoftOption.Children)
                {
                    if (radio is Plugin.InputKit.Shared.Controls.RadioButton)
                        if (((Plugin.InputKit.Shared.Controls.RadioButton)radio).IsChecked)
                            return int.Parse(((Plugin.InputKit.Shared.Controls.RadioButton)radio).Value.ToString());
                }
            return 0;
        }
        private async void PopupOrderProduct(object sender, EventArgs e)
        {
            HolderPage.IsVisible = !HolderPage.IsVisible;
            if (!HolderPage.IsVisible)
            {
                BagCartProperty.ProductSelect = null;
                await FrameProduct.FadeTo(0, 250, Easing.CubicIn);
                FrameProduct.IsVisible = false;
            }
            else
            {
                if (sender is StackLayout)
                {
                    BagCartProperty.ProductSelect = (BLL.M.Mobile.CartBag)((StackLayout)sender).BindingContext;
                    FrameProduct.IsVisible = true;
                }
                await FrameProduct.FadeTo(1, 250, Easing.CubicIn);
            }
        }
        private void PlusWeight(object sender, EventArgs e)
        {
            if (double.TryParse(Weight.Text, out double WeightValue))
            {
                Weight.Text = (WeightValue + 1).ToString();
            }
            else
            {
                Weight.Text = "1";
            }
        }
        private void MinusWeight(object sender, EventArgs e)
        {
            if (double.TryParse(Weight.Text, out double WeightValue))
            {
                Weight.Text = WeightValue > 0 ? (WeightValue - 1).ToString() : "1";
            }
            else
            {
                Weight.Text = "1";
            }
        }
        private async void OpenChectOutPage(object sender, EventArgs e)
        {
            try
            {
                if (BagCartProperty.InfoBagCart.ItemsCount > 0)
                {
                    var AddInvoiceWithDetails = new ViewModel.AddInvoiceWithDetails
                    {
                        ItemsCount = BagCartProperty.InfoBagCart.ItemsCount,
                        SubTotal = BagCartProperty.InfoBagCart.SubTotal,
                        AddtionTax = BagCartProperty.InfoBagCart.AddtionTax,
                        TotalTax = BagCartProperty.InfoBagCart.TotalTax,
                        TotalBeforeTax = BagCartProperty.InfoBagCart.TotalBeforeTax,
                        TotalAmount = BagCartProperty.InfoBagCart.TotalAmount,

                    };
                    foreach (var item in BagCartProperty.InfoBagCart.ProductInCart)
                        AddInvoiceWithDetails.ModelCart.ListItemInvoice.Add(new BLL.M.Mobile.ListItemInvoice
                        {
                            ItemId = item.ItemId,
                            ItemCartId = item.Id,
                            Qty = item.Qty,
                            Fat = item.Fat,
                            Nature = item.Nature,
                            Size = item.Size,
                            ItemNote = item.Note ?? string.Empty
                        });
                    await App._nav.PushAsync(new CheckoutPage(AddInvoiceWithDetails));
                }
                else
                {
                    await DisplayAlert("", MultiLanguage.MLResource.NoItemInCart, MultiLanguage.MLResource.Ok);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert(MultiLanguage.MLResource.Error, ex.ToString(), MultiLanguage.MLResource.Ok);
            }
        }
        private void BackPage(object sender, EventArgs e)
        {
            App._nav.PopAsync(true);
        }
        private void OpenProductDetailsPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new ProductDetails(BagCartProperty.ProductSelect.ItemInfo.Id));
        }
        private void ShowDetailsInvoice(object sender, EventArgs e)
        {
            StackDetailsInvoice.IsVisible = !StackDetailsInvoice.IsVisible;
        }
    }
}

