using Plugin.InputKit.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyWishListPage : ContentPage
    {
        public Property.MyFavouritesProperty MyFavouritesProperty { get; set; } = new Property.MyFavouritesProperty();

        public MyWishListPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<Interface.IStatusBarStyleManager>().SetDarkTheme();
            LoadFavouriteProducts();
        }
        private async void LoadFavouriteProducts()
        {
            try
            {                
                if (AppStatics.UserID != -1)
                {
                    ListItemFav.BeginRefresh();
                       var userId = AppStatics.UserID;
                    var responseProducts = (await new Services.HttpExtension<List<ViewModel.ProductCart>>().Get($"WishList/GetWishListByCustomerID?customerID={userId}"));
                    MyFavouritesProperty.Products = responseProducts == null ? null : new ObservableCollection<ViewModel.ProductCart>(responseProducts);
                    
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
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, MultiLanguage.MLResource.FailedMessage , MultiLanguage.MLResource.Ok);
#endif

            }
            ListItemFav.EndRefresh();
        }
        private async void PopupOrderProduct(object sender, EventArgs e)
        {
            HolderPage.IsVisible = !HolderPage.IsVisible;
            if (!HolderPage.IsVisible)
            {
                var DefualtHeight = LahmaOnline.StyleViews.Styles.SetterStyle.Maintain_HeightAspectRatio(600);
                MyFavouritesProperty.ProductSelect = null;
                 await FrameProduct.FadeTo(0, 250, Easing.CubicIn);
                FrameProduct.IsVisible = false;
            }
            else
            {
                if (sender is StackLayout)
                {
                    MyFavouritesProperty.ProductSelect = (ViewModel.ProductCart)((StackLayout)sender).BindingContext;
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
        private void BackPage(object sender, EventArgs e)
        {
            App._nav.PopAsync(true);
        }
        private async void RemoveFromFavourites(object sender, EventArgs e)
        {
            try
            {
                if (AppStatics.UserID != -1)
                {
                    var Object = (FFImageLoading.Svg.Forms.SvgCachedImage)sender;
                    var Product = (ViewModel.ProductCart)Object.BindingContext;
                    List<BLL.M.Mobile.Favourites> favourites = new List<BLL.M.Mobile.Favourites>
                    {
                        new BLL.M.Mobile.Favourites
                        {
                            CustomerId = AppStatics.UserID,
                            ItemId = Product.Id,
                        },
                    };
                    var responseAddToFavourites =
                        await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("WishList/DeleteWishList", favourites);

                    if (responseAddToFavourites.StatusCode == (int)System.Net.HttpStatusCode.OK)
                    {
                        ((FFImageLoading.Svg.Forms.SvgCachedImage)sender).Source = "heartoff.svg";
                        await Task.Delay(500);
                        MyFavouritesProperty.Products.Remove(Product);
                    }
                    else
                    {
                        await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, responseAddToFavourites.Message, MultiLanguage.MLResource.Ok);
                    }
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
                    await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, MultiLanguage.MLResource.FailedMessage , MultiLanguage.MLResource.Ok);
#endif

            }
        }
        private async void AddToCartAction(object sender, EventArgs e)
        {
            try
            {
                if (MyFavouritesProperty.ProductSelect.Quentity < 1)
                    MyFavouritesProperty.ProductSelect.Quentity = 1;
                var model = new BLL.M.Mobile.AddToCart
                {
                    UserId = AppStatics.UserID != -1 ? AppStatics.UserID : 0,
                    GuestId = AppStatics.UserID != -1 ? "0" : AppStatics.GestID.ToString(),
                    ItemId = MyFavouritesProperty.ProductSelect.Id,
                    Fat = FatOptionSelect(),
                    Size = SizeOptionSelect(),
                    Nature = NatureOptionSelect(),
                    Qty = Convert.ToDecimal(MyFavouritesProperty.ProductSelect.Quentity),
                    Note = MyFavouritesProperty.ProductSelect.Note
                };
                var responseAddToCart =
                                await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("AddToCart", model);
                if (responseAddToCart?.StatusCode == 200)
                {
                    MainPage.CountBagCart++;
                    PopupOrderProduct(sender, e);
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
        private int FatOptionSelect()
        {
            if (MyFavouritesProperty.ProductSelect.IsFat)
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
            if (MyFavouritesProperty.ProductSelect.IsSizing)
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
            if (MyFavouritesProperty.ProductSelect.IsNature)
                foreach (var radio in SoftOption.Children)
                {
                    if (radio is Plugin.InputKit.Shared.Controls.RadioButton)
                        if (((Plugin.InputKit.Shared.Controls.RadioButton)radio).IsChecked)
                            return int.Parse(((Plugin.InputKit.Shared.Controls.RadioButton)radio).Value.ToString());
                }
            return 0;
        }

        private void OpenProductDetailsPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.ProductDetails(MyFavouritesProperty.ProductSelect.Id));
        }
    }
}