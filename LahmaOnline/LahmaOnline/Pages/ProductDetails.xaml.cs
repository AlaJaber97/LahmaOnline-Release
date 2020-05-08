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
    public partial class ProductDetails : ContentPage
    {
        public Property.ProductDetailsProperty DetailsProperty { get; set; } = new Property.ProductDetailsProperty();
        private int ProductID;
        public ProductDetails(int productID)
        {
            InitializeComponent();
            this.ProductID = productID;
            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = LoadInfoItem();
        }
        private async Task LoadInfoItem()
        {
            var itemModel = new List<BLL.M.Mobile.GetByIdItemDto>
            {
                new BLL.M.Mobile.GetByIdItemDto
                {
                    Id = this.ProductID
                }
            };
            var responseIinfoProduct = await new Services.HttpExtension<List<BLL.M.Mobile.Product>>().Post("Item/GetFullItemInfoById", itemModel);
            DetailsProperty.Product = responseIinfoProduct.FirstOrDefault();
        }
        private async void AddToWishList(object sender, EventArgs e)
        {
            try
            {
                if (AppStatics.UserID != -1)
                {
                    var Product = DetailsProperty.Product;
                    if (!Product.IsWishlist)
                    {
                        BLL.M.Mobile.Favourites favourites = new BLL.M.Mobile.Favourites
                        {
                            CustomerId = AppStatics.UserID,
                            ItemId = Product.Id,
                        };
                        var responseAddToFavourites =
                            await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("WishList", favourites);
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

                        if (!(responseAddToFavourites.StatusCode == (int)System.Net.HttpStatusCode.OK))
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
        private async void AddToCart(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(Quentity.Text) < 1)
                    Quentity.Text = "1";
                var model = new BLL.M.Mobile.AddToCart
                {
                    UserId = AppStatics.UserID != -1 ? AppStatics.UserID : 0,
                    GuestId = AppStatics.UserID != -1 ? "0" : AppStatics.GestID.ToString(),
                    ItemId = DetailsProperty.Product.Id,
                    Fat = FatOptionSelect(),
                    Size = SizeOptionSelect(),
                    Nature = NatureOptionSelect(),
                    Qty = Convert.ToDecimal(Quentity.Text) == 0 ? 1 : Convert.ToDecimal(Quentity.Text),
                    Note = Note.Text
                };
                var responseAddToCart = await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("AddToCart", model);
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
            if (DetailsProperty.Product.IsFat)
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
            if (DetailsProperty.Product.IsSizing)
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
            if (DetailsProperty.Product.IsNature)
                foreach (var radio in SoftOption.Children)
                {
                    if (radio is Plugin.InputKit.Shared.Controls.RadioButton)
                        if (((Plugin.InputKit.Shared.Controls.RadioButton)radio).IsChecked)
                            return int.Parse(((Plugin.InputKit.Shared.Controls.RadioButton)radio).Value.ToString());
                }
            return 0;
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
        private async void PopupOrderProduct(object sender, EventArgs e)
        {
            HolderPage.IsVisible = !HolderPage.IsVisible;
            if (!HolderPage.IsVisible)
            {
                await FrameProduct.FadeTo(0, 250, Easing.CubicIn);
            }
            else
            {
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
        private void BackPage(object sender, EventArgs e)
        {
            App._nav.PopAsync();
        }
        private void OpenLoginPage(object sender, EventArgs e)
        {
            App._nav.PushAsync(new Pages.LoginPage());
        }
        private void SwitchTabBar(object sender, EventArgs e)
        {
            if (sender is Label l)
            {
                box.SetValue(Grid.ColumnProperty, int.Parse(l.ClassId));
                if (l.ClassId == "0")
                {
                    ReviewView.IsVisible = false;
                    lsComments.IsVisible = true;

                    ReviewBtn.TextColor = Color.FromHex("#818BA3");
                    CommentBtn.TextColor = Color.FromHex("#8B4624");
                }
                else if (l.ClassId == "1")
                {
                    ReviewView.IsVisible = true;
                    lsComments.IsVisible = false;

                    ReviewBtn.TextColor = Color.FromHex("#8B4624");
                    CommentBtn.TextColor = Color.FromHex("#818BA3");
                }
            }
        }
        private async void SubmitReview(object sender, EventArgs e)
        {
            try
            {
                var itemReview = new BLL.M.Mobile.SaveItemCommentDto
                {
                    Comment = DetailsProperty.ReviewComment.Comment,
                    Email = DetailsProperty.ReviewComment.Email,
                    CustomerId = AppStatics.UserID != -1 ? AppStatics.UserID : 0,
                    ItemId = this.ProductID,
                };

                var responseIinfoProduct = await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("ItemComment", itemReview);
                await DisplayAlert("", responseIinfoProduct?.Message, MultiLanguage.MLResource.Ok);
                DetailsProperty.ReviewComment = new Property.ReviewCommentUser();
                await ScrollPage.ScrollToAsync(0, 0, true);
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
    }
}