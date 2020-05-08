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
    public partial class MyOrdersPage : ContentPage
    {
        public Property.MyOrdersProperty MyOrdersProperty { get; set; } = new Property.MyOrdersProperty();
        private readonly BLL.M.Mobile.GetSalesInvoiceByEmail modelGetSalesInvoice;
        public MyOrdersPage()
        {
            InitializeComponent();
            modelGetSalesInvoice = new BLL.M.Mobile.GetSalesInvoiceByEmail
            {
                Email = AppStatics.UserProfile.Email,
                PageSize = 5,
                CurrentPage = 0
            };
            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<Interface.IStatusBarStyleManager>().SetDarkTheme();
            MyOrdersProperty.Orders.Clear();
            LoadInvoiceUser();
        }
        private async void LoadInvoiceUser()
        {
            try
            {
                MyOrdersProperty.IsLoading = true;
                var responseProducts = (await new Services.HttpExtension<List<BLL.M.Mobile.Invoice>>().Post("SalesInvoice/GetInvoicesByEMail", modelGetSalesInvoice));
                if (responseProducts != null)
                {
                    if (responseProducts.Count == 0)
                        modelGetSalesInvoice.CurrentPage--;
                    ButNext.IsVisible = responseProducts.Count == modelGetSalesInvoice.PageSize;
                    ButPrevious.IsVisible = modelGetSalesInvoice.CurrentPage > 0;

                    if (responseProducts?.Count > 0)
                        MyOrdersProperty.Orders = new ObservableCollection<BLL.M.Mobile.Invoice>(responseProducts);
                    else if (MyOrdersProperty.Orders.Count > 0)
                        await DisplayAlert("", MultiLanguage.MLResource.NoMoreInvoice, MultiLanguage.MLResource.Ok);

                }
                else
                    modelGetSalesInvoice.CurrentPage--;
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
            MyOrdersProperty.IsLoading = false;

        }

        private void PreviousListInvoice(object sender, EventArgs e)
        {
            ListOrders.ScrollTo(MyOrdersProperty.Orders.FirstOrDefault(), ScrollToPosition.Start, false);
            modelGetSalesInvoice.CurrentPage--;
            LoadInvoiceUser();
        }

        private void NextListInvoice(object sender, EventArgs e)
        {
            ListOrders.ScrollTo(MyOrdersProperty.Orders.FirstOrDefault(), ScrollToPosition.Start, false);
            modelGetSalesInvoice.CurrentPage++;
            LoadInvoiceUser();
        }

        private async void OpenInvoiceDetails(object sender, EventArgs e)
        {
            var frameElement = (Frame)sender;
            var Invoice = (BLL.M.Mobile.Invoice)frameElement.BindingContext;
            await App._nav.PushAsync(new Pages.InvoiceDetailsPage(Invoice));
        }

        private void BackPage(object sender, EventArgs e)
        {
            App._nav.PopAsync();
        }
    }
}