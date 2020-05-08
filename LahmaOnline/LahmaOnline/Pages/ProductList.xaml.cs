using LahmaOnline.StyleViews.Styles;
using Plugin.InputKit.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductList : ContentPage
    {
        public Property.ProductListProperty ProductListProperty { get; set; } = new Property.ProductListProperty();
        public int IdCategory { get; set; } = 0;

        public ProductList()
        {
            InitializeComponent();
            BindingContext = this;
        }
        public ProductList(int id = 0)
        {
            InitializeComponent();
            IdCategory = id;

            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ProductListProperty.PropertiesFilter.PageSize = 5;
            ProductListProperty.PropertiesFilter.CategoryId = IdCategory;
            _ = LoadAllProduct();
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<Interface.IStatusBarStyleManager>().SetDarkTheme();

            if (HolderFilter.IsVisible)
                ShowFilter(null, null);
        }
        private async Task LoadAllProduct()
        {
            try
            {
                ApplyFilter(null, null);

                var responseCategories = await new Services.HttpExtension<List<ViewModel.Category>>().Get("Category");
                ProductListProperty.AllCategory = responseCategories == null ? null : new ObservableCollection<ViewModel.Category>(responseCategories.OrderBy(item=>item.Name.Length));
                if (ProductListProperty.PropertiesFilter.CategoryId != 0)
                    ProductListProperty.AllCategory.FirstOrDefault(item => item.Id == ProductListProperty.PropertiesFilter.CategoryId).IsSelected = true;

                var responseOrigins = await new Services.HttpExtension<List<BLL.M.Mobile.NationalityOrigin>>().Get($"Lookup/2");
                ProductListProperty.Origins = responseOrigins == null ? null : new ObservableCollection<BLL.M.Mobile.NationalityOrigin>(responseOrigins);
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
        private async Task LoadOriginFromeCategory(int iD)
        {
            try
            {
                var responseOrigins = await new Services.HttpExtension<List<BLL.M.Mobile.NationalityOrigin>>().Get($"Category/GetOrigionByCategoryId{iD.ToString()}");
                ProductListProperty.Origins = responseOrigins == null ? null : new ObservableCollection<BLL.M.Mobile.NationalityOrigin>(responseOrigins);
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
        private async void PopupOrderProduct(object sender, EventArgs e)
        {
            HolderPage.IsVisible = !HolderPage.IsVisible;
            if (!HolderPage.IsVisible)
            {
                ProductListProperty.ProductSelect = null;
                await FrameProduct.FadeTo(0, 250, Easing.CubicIn);
                FrameProduct.IsVisible = false;
            }
            else
            {
                if (sender is StackLayout)
                {
                    ProductListProperty.ProductSelect = (BLL.M.Mobile.Product)((StackLayout)sender).BindingContext;
                    FrameProduct.IsVisible = true;
                }
                await FrameProduct.FadeTo(1, 250, Easing.CubicIn);
            }
        }
        private void PlusWeight(object sender, EventArgs e)
        {
            if (double.TryParse(Quentity.Text, out double WeightValue))
            {
                Quentity.Text = (WeightValue + 1).ToString();
            }
            else
            {
                Quentity.Text = "1";
            }
        }
        private void MinusWeight(object sender, EventArgs e)
        {
            if (double.TryParse(Quentity.Text, out double WeightValue))
            {
                Quentity.Text = WeightValue > 0 ? (WeightValue - 1).ToString() : "1";
            }
            else
            {
                Quentity.Text = "1";
            }
        }
        private async void ShowFilter(object sender, EventArgs e)
        {
            HolderFilter.IsVisible = !HolderFilter.IsVisible;
            if (!HolderFilter.IsVisible)
            {
                var DefualtHeight = App.ScreenHeight;
                await FilterView.TranslateTo(0, (FilterView.Height == -1 ? DefualtHeight : FilterView.Height) + SetterStyle.Maintain_HeightAspectRatio(15), 250);
            }
            else
            {
                await FilterView.TranslateTo(0, 0, 250);
            }
        }
        private void BackPage(object sender, EventArgs e)
        {
            App._nav.PopAsync(true);
        }
        private void ResetFilter(object sender, EventArgs e)
        {
            //Reset  UI Filter
            txtSearch.Text = string.Empty;
            var ObjectOldSelected = ProductListProperty.AllCategory.Where(x => x.IsSelected == true);
            foreach (var item in ObjectOldSelected)
            {
                item.IsSelected = false;
                item.BackgroundColor = Color.Transparent;
            }
            PickerOrigin.SelectedItem = 0;
            (RadioButtonGroupView.Children[0] as Plugin.InputKit.Shared.Controls.RadioButton).IsChecked = true;
            (RadioButtonGroupView.Children[1] as Plugin.InputKit.Shared.Controls.RadioButton).IsChecked = false;
            (RadioButtonGroupView.Children[2] as Plugin.InputKit.Shared.Controls.RadioButton).IsChecked = false;
            (RadioButtonGroupView.Children[3] as Plugin.InputKit.Shared.Controls.RadioButton).IsChecked = false;
            (RadioButtonGroupView.Children[4] as Plugin.InputKit.Shared.Controls.RadioButton).IsChecked = false;
            //Reset Properties Filter
            ProductListProperty.PropertiesFilter.CurrentPage = 0;
            ProductListProperty.PropertiesFilter.CategoryId = 0;
            ProductListProperty.PropertiesFilter.OrigionId = 0;
            ProductListProperty.PropertiesFilter.Keyword = string.Empty;
            ProductListProperty.PropertiesFilter.FromPrice = ProductListProperty.PropertiesFilter.ToPrice = 0;
        }
        private async void ApplyFilter(object sender, EventArgs e)
        {
            ProductListProperty.IsLoading = true;
            try
            {
                if (HolderFilter.IsVisible)
                    ShowFilter(null, null);
                ProductListProperty.PropertiesFilter.UserId = AppStatics.UserID != -1 ? AppStatics.UserID : 0;
                var responseProductsFiltered =
                            await new Services.HttpExtension<List<BLL.M.Mobile.Product>>().Post("Category/GetFullSearchCategories", ProductListProperty.PropertiesFilter);
                if (responseProductsFiltered != null)
                {
                    if (responseProductsFiltered.Count == 0)
                        ProductListProperty.PropertiesFilter.CurrentPage--;
                    Device.BeginInvokeOnMainThread(() => {
                        ButNext.IsVisible = responseProductsFiltered?.Count == ProductListProperty.PropertiesFilter.PageSize;
                        ButPrevious.IsVisible = ProductListProperty.PropertiesFilter.CurrentPage > 0;
                    });
                    if (responseProductsFiltered.Count > 0)
                    {
                        ProductListProperty.Products = responseProductsFiltered == null ? null : new ObservableCollection<BLL.M.Mobile.Product>(responseProductsFiltered);
                        Device.BeginInvokeOnMainThread(() =>
                        ListViewProduct.ScrollTo(ProductListProperty.Products.FirstOrDefault(), ScrollToPosition.Start, false));
                    }
                    else
                        await DisplayAlert("", MultiLanguage.MLResource.NoMoreItem, MultiLanguage.MLResource.Ok);
                }
                else
                    ProductListProperty.PropertiesFilter.CurrentPage--;
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
            ProductListProperty.IsLoading = false;
        }
        private void SearchKeyword(object sender, EventArgs e)
        {
            var Keyword = ((CustomRenderer.CustomEntry)sender).Text;
            ProductListProperty.PropertiesFilter.Keyword = Keyword;
        }
        private void SelectCategory(object sender, EventArgs e)
        {
            if (sender is Frame)
            {
                var ObjectSize = (((Frame)sender).BindingContext as ViewModel.Category);
                if (ObjectSize.IsSelected == false)
                {
                    //DisActive Old 
                    var ObjectOldSelected = ProductListProperty.AllCategory.Where(x => x.IsSelected == true).SingleOrDefault();
                    if (ObjectOldSelected != null)
                        ObjectOldSelected.IsSelected = false;

                    ProductListProperty.PropertiesFilter.CategoryId = ObjectSize.Id;
                    _ = LoadOriginFromeCategory(ObjectSize.Id);
                    //Active New
                    ObjectSize.IsSelected = true;
                }
                else
                {
                    ProductListProperty.PropertiesFilter.CategoryId = 0;
                    _ = LoadOriginFromeCategory(0);
                    //DisActive for the cureent selected 
                    ObjectSize.IsSelected = false;
                }
            }
        }
        private void SelectOrigin(object sender, EventArgs e)
        {
            if (sender is UserControls.GradientPicker)
            {
                if (!(((UserControls.GradientPicker)sender).SelectedItem is BLL.M.Mobile.NationalityOrigin ItemSelect)) return;
                if (ItemSelect.Name != "---Select---")
                    ProductListProperty.PropertiesFilter.OrigionId = int.Parse((((UserControls.GradientPicker)sender).SelectedItem as BLL.M.Mobile.NationalityOrigin).Code);
                else
                    ProductListProperty.PropertiesFilter.OrigionId = 0;
            }
        }
        private void SelectPrice(object sender, EventArgs e)
        {
            foreach(var item in (sender as RadioButtonGroupView).Children)
                if((item as Plugin.InputKit.Shared.Controls.RadioButton).IsChecked)
                {
                    var ValuePrice = (item as Plugin.InputKit.Shared.Controls.RadioButton).Value.ToString();
                    var Price = ValuePrice.Split(',');
                    ProductListProperty.PropertiesFilter.FromPrice = int.Parse(Price[0]);
                    ProductListProperty.PropertiesFilter.ToPrice = int.Parse(Price[1]);
                }
        }
        private async void AddToCart(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(Quentity.Text) < 1 )
                        Quentity.Text = "1";
                var model = new BLL.M.Mobile.AddToCart
                {
                    UserId = AppStatics.UserID != -1 ? AppStatics.UserID : 0,
                    GuestId = AppStatics.UserID != -1 ? "0" : AppStatics.GestID.ToString(),
                    ItemId = ProductListProperty.ProductSelect.Id,
                    Fat = FatOptionSelect(),
                    Size = SizeOptionSelect(),
                    Nature = NatureOptionSelect(),
                    Qty = Convert.ToDecimal(Quentity.Text),
                    Note = Note.Text
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
            if (ProductListProperty.ProductSelect.IsFat)
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
            if (ProductListProperty.ProductSelect.IsSizing)
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
            if (ProductListProperty.ProductSelect.IsNature)
                foreach (var radio in SoftOption.Children)
                {
                    if (radio is Plugin.InputKit.Shared.Controls.RadioButton)
                        if (((Plugin.InputKit.Shared.Controls.RadioButton)radio).IsChecked)
                            return int.Parse(((Plugin.InputKit.Shared.Controls.RadioButton)radio).Value.ToString());
                }
            return 0;
        }
        private async void AddToFavourites(object sender, EventArgs e)
        {
            try
            {
                if (AppStatics.UserID != -1)
                {
                    var Object = (FFImageLoading.Svg.Forms.SvgCachedImage)sender;
                    var Product = (BLL.M.Mobile.Product)Object.BindingContext;
                    if (!Product.IsWishlist)
                    {
                        BLL.M.Mobile.Favourites favourites = new BLL.M.Mobile.Favourites
                        {
                            CustomerId = AppStatics.UserID,
                            ItemId = Product.Id,
                        };
                        var responseAddToFavourites =
                            await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("WishList", favourites);
                        if (responseAddToFavourites.StatusCode == (int)System.Net.HttpStatusCode.OK)
                        {
                            ((FFImageLoading.Svg.Forms.SvgCachedImage)sender).Source = "hearton.svg";
                            Product.IsWishlist = true;
                        }
                        else
                        {
                            await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, responseAddToFavourites.Message, MultiLanguage.MLResource.Ok);
                        }
                    }
                    else
                    {
                        List<BLL.M.Mobile.Favourites> favourites = new List<BLL.M.Mobile.Favourites>
                        {
                            new BLL.M.Mobile.Favourites
                            {
                                CustomerId = AppStatics.UserID,
                                ItemId = Product.Id,
                            }
                        };
                        var responseAddToFavourites =
                            await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("WishList/DeleteWishList", favourites);

                        if (responseAddToFavourites.StatusCode == (int)System.Net.HttpStatusCode.OK)
                        {
                            ((FFImageLoading.Svg.Forms.SvgCachedImage)sender).Source = "heartoff.svg";
                            Product.IsWishlist = false;
                        }
                        else
                        {
                            await App._nav.DisplayAlert(MultiLanguage.MLResource.Error, responseAddToFavourites.Message, MultiLanguage.MLResource.Ok);
                        }

                    }
                }
                else
                {
                    HideMessage(null, null);
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
        private async void HideMessage(object sender, EventArgs e)
        {
            if (HolderMessage.IsVisible)
            {
                HolderMessage.IsVisible = false;
                await PopupMessage.ScaleTo(0, 250);
                PopupMessage.IsVisible = false;
            }
            else
            {
                HolderMessage.IsVisible = true;
                PopupMessage.IsVisible = true;
                await PopupMessage.ScaleTo(1, 250);
            }
        }
        private void OpenLoginPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.LoginPage());
        }
        private void OpenProductDetailsPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.ProductDetails(ProductListProperty.ProductSelect.Id));
        }

        private void NextListProduct(object sender, EventArgs e)
        {
            ListViewProduct.ScrollTo(ListViewProduct.Header, ScrollToPosition.Start, false);
            ProductListProperty.PropertiesFilter.CurrentPage++;
            ApplyFilter(null, null);
        }

        private void PreviousListProduct(object sender, EventArgs e)
        {
            ListViewProduct.ScrollTo(ListViewProduct.Header, ScrollToPosition.Start, false);
            ProductListProperty.PropertiesFilter.CurrentPage--;
            ApplyFilter(null, null);
        }
    }
}