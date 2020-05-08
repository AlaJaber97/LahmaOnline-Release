using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Property
{
    public class MyOrdersProperty : BaseProperty
    {
        private ObservableCollection<BLL.M.Mobile.Invoice> _Orders;

        public ObservableCollection<BLL.M.Mobile.Invoice> Orders
        {
            get { return _Orders?? (_Orders = new ObservableCollection<BLL.M.Mobile.Invoice>()); }
            set { _Orders = value; OnPropertyChanged(); }
        }
        private bool _IsLoading = true;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                if (_IsLoading != value)
                {
                    _IsLoading = value;
                    if (!value)
                        if (Orders.Count == 0)
                            IsNoItems = true;
                        else
                            IsNoItems = false;
                }
                OnPropertyChanged();
            }
        }
        private bool _IsNoItems = true;

        public bool IsNoItems
        {
            get { return _IsNoItems; }
            set { if (_IsNoItems != value) { SetProperty(ref _IsNoItems, value); } OnPropertyChanged(); }
        }
        public MyOrdersProperty()
        {

        }
    }
}