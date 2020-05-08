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
    public partial class HomePage : ContentPage
    {
        public Property.HomeProperty HomeProperty { get; set; } = new Property.HomeProperty();

        public HomePage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(Device.RuntimePlatform == Device.Android)
                DependencyService.Get<Interface.IStatusBarStyleManager>().SetDarkTheme();
            if (HomeProperty.Categories.Count == 0)
                _ = LoadData();
            ObservProductBuy(null, null);
        }
        private async Task LoadData()
        {
            try
            {
                var responseCategories =
                    await new Services.HttpExtension<List<BLL.M.Mobile.Category>>().Get("Category");
                var responseNewArrival = (
                    await new Services.HttpExtension<List<ViewModel.ProductCart>>().Get("Item", "GetItemByNewArrival")).OrderBy(x => (new Random(2019)).Next()).Take(5);
                var responseTopSelection = (
                    await new Services.HttpExtension<List<ViewModel.ProductCart>>().Get("Item", "GetItemByTopSelection")).OrderBy(x => (new Random(2015)).Next()).Take(5);

                #region Get Categories
                HomeProperty.Categories = responseCategories == null ? null : new ObservableCollection<BLL.M.Mobile.Category>(responseCategories);
                #endregion
                #region Get New Arrival
                HomeProperty.NewArrival = responseNewArrival == null ? null : new ObservableCollection<ViewModel.ProductCart>(responseNewArrival);
                #endregion
                #region Get Top Selection
                HomeProperty.TopSelection = responseTopSelection == null ? null : new ObservableCollection<ViewModel.ProductCart>(responseTopSelection);
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
        }
        private async void AddToCartAction(object sender, EventArgs e)
        {
            try
            {
                if (HomeProperty.ProductSelected.Quentity < 1)
                    HomeProperty.ProductSelected.Quentity = 1;
                var model = new BLL.M.Mobile.AddToCart
                {
                    UserId = AppStatics.UserID != -1 ? AppStatics.UserID : 0,
                    GuestId = AppStatics.UserID != -1 ? "0" : AppStatics.GestID.ToString(),
                    ItemId = HomeProperty.ProductSelected.Id,
                    Fat = FatOptionSelect(),
                    Size = SizeOptionSelect(),
                    Nature = NatureOptionSelect(),
                    Qty = Convert.ToDecimal(HomeProperty.ProductSelected.Quentity),
                    Note = HomeProperty.ProductSelected.Note ?? string.Empty,
                    Source = 2

                };
                var responseAddToCart =
                                await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("AddToCart", model);
                if (responseAddToCart?.StatusCode == 200)
                {
                    MainPage.CountBagCart++;
                    ObservProductBuy(sender, e);
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
        private async void ObservProductBuy(object sender, EventArgs e)
        {
            if (sender == null && !HolderPage.IsVisible) return;
            HolderPage.IsVisible = !HolderPage.IsVisible;
            if (!HolderPage.IsVisible)
            {
                var DefualtHeight = LahmaOnline.StyleViews.Styles.SetterStyle.Maintain_HeightAspectRatio(600);
                HomeProperty.ProductSelected = null;
                await FrameProduct.TranslateTo(0, FrameProduct.Height == -1 ? DefualtHeight : FrameProduct.Height, 250);
            }
            else
            {
                if (sender is StackLayout)
                {
                    HomeProperty.ProductSelected = (ViewModel.ProductCart)((StackLayout)sender).BindingContext;
                }
                await FrameProduct.TranslateTo(0, 0, 250);
            }
        }
        private int FatOptionSelect()
        {
            if (HomeProperty.ProductSelected.IsFat)
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
            if (HomeProperty.ProductSelected.IsSizing)
                foreach (var radio in SizeOption.Children)
                {
                    if(radio is Plugin.InputKit.Shared.Controls.RadioButton)
                    if (((Plugin.InputKit.Shared.Controls.RadioButton)radio).IsChecked)
                            return int.Parse(((Plugin.InputKit.Shared.Controls.RadioButton)radio).Value.ToString());
                }
            return 0;
        }
        private int NatureOptionSelect()
        {
            if (HomeProperty.ProductSelected.IsNature)
                foreach (var radio in SoftOption.Children)
                {
                    if (radio is Plugin.InputKit.Shared.Controls.RadioButton)
                        if (((Plugin.InputKit.Shared.Controls.RadioButton)radio).IsChecked)
                            return int.Parse(((Plugin.InputKit.Shared.Controls.RadioButton)radio).Value.ToString());
                }
            return 0;
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
        private void OpenCategoryByID(object sender, EventArgs e)
        {
            var Category = (BLL.M.Mobile.Category)((StackLayout)sender).BindingContext;
            var page = ((Pages.ProductList)((TabbedPage)this.Parent).Children[2]);
            page.IdCategory = Category.Id;
            page.ProductListProperty.PropertiesFilter = null;
            ((TabbedPage)this.Parent).CurrentPage = page;
        }
        private void OpenProductDetailsPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.ProductDetails(HomeProperty.ProductSelected.Id));
        }
        
        private void OpenChatPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.ChatPage());
        }
    }
}