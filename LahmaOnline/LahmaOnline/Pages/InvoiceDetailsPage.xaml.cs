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
    public partial class InvoiceDetailsPage : ContentPage
    {
        public Property.InvoiceDetailsProperty InvoiceDetailsProperty { get; set; }
        public InvoiceDetailsPage()
        {
            InitializeComponent();
            InvoiceDetailsProperty = new Property.InvoiceDetailsProperty();
            GetLastInvoice();
             BindingContext = this;
        }
        public InvoiceDetailsPage(BLL.M.Mobile.Invoice invoice)
        {
            InitializeComponent();
            InvoiceDetailsProperty = new Property.InvoiceDetailsProperty
            {
                Invoice = invoice
            };
            BindingContext = this;
            
        }
        private async void GetLastInvoice()
        {
            try
            {
                InvoiceDetailsProperty.IsLoading = true;
                var modelGetSalesInvoice = new BLL.M.Mobile.GetSalesInvoiceByEmail
                {
                    Email = AppStatics.UserProfile.Email,
                    PageSize = 1,
                    CurrentPage = 0
                };
                var responseProducts = await new Services.HttpExtension<List<BLL.M.Mobile.Invoice>>().Post("SalesInvoice/GetInvoicesByEMail", modelGetSalesInvoice);
                if(responseProducts!=null)
                    InvoiceDetailsProperty.Invoice =  responseProducts.SingleOrDefault();

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
            InvoiceDetailsProperty.IsLoading = false;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<Interface.IStatusBarStyleManager>().SetDarkTheme();
        }

        private void BackPage(object sender, EventArgs e)
        {
            App._nav.PopAsync(true);
        }

        private void ShowEvalutionView(object sender, EventArgs e)
        {
            if (!HolderPage.IsVisible)
            {
                var StackSelected = (Button)sender;
                var ItemSelected = (BLL.M.Mobile.ListItem)StackSelected.BindingContext;
                InvoiceDetailsProperty.ItemSelectd = ItemSelected;
                HolderPage.IsVisible = true;
            }
            else
            {
                HolderPage.IsVisible = false;
                InvoiceDetailsProperty.ItemSelectd = null;
                InvoiceDetailsProperty.ReviewComment = null;
            }
        }

        private async void SubmitReview(object sender, EventArgs e)
        {
            var itemReview = new BLL.M.Mobile.SaveEvalutionItemDto
            {
                CustomerId = AppStatics.UserID,
                ItemId = InvoiceDetailsProperty.ItemSelectd.ItemId,
                OrderId = InvoiceDetailsProperty.ItemSelectd.OrderId,
                Rating = InvoiceDetailsProperty.ReviewComment.Rating,
                Comment = InvoiceDetailsProperty.ReviewComment.Comment,
            };

            var responseIinfoProduct = await new Services.HttpExtension<BLL.M.Identity.ResponseMessage>().Post("EvalutionItem", itemReview);
            await DisplayAlert("", responseIinfoProduct.Message, MultiLanguage.MLResource.Ok);
            ShowEvalutionView(null,null);
        }
        StackLayout OldStarsStack;

        private void SelectRating(object sender, EventArgs e)
        {
            var StackLayout = (StackLayout)sender;
            InvoiceDetailsProperty.ReviewComment.Rating = 0;
            if (OldStarsStack != StackLayout)
                foreach (FFImageLoading.Svg.Forms.SvgCachedImage Star in StackLayout.Children)
                {
                    Star.Source = ("star_filled.svg");
                    InvoiceDetailsProperty.ReviewComment.Rating++;
                }
            if (OldStarsStack != null)
                foreach (FFImageLoading.Svg.Forms.SvgCachedImage Star in OldStarsStack.Children)
                {
                    Star.Source = ("star_empty.svg");
                }
            OldStarsStack = StackLayout;
        }
    }
}