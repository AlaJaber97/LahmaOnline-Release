using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LahmaOnline.Property
{
    public class BaseProperty : ObservableObject
    {
        public Page CurrentPage;

        protected async void OpenPage(Page page)
        {
            CurrentPage = page;
            CurrentPage.BindingContext = this;
            await App._nav.PushAsync(CurrentPage);
        }

    }
}
