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
    public class BagCartProperty : BaseProperty
    {
        public InfoBagCart InfoBagCart { get; set; } = new InfoBagCart();


        private BLL.M.Mobile.CartBag _ProductSelect;
        public BLL.M.Mobile.CartBag ProductSelect
        {
            get { return _ProductSelect ?? (_ProductSelect = new BLL.M.Mobile.CartBag()); }
            set { _ProductSelect = value; OnPropertyChanged(); }
        }

        private BLL.M.Mobile.GetSettingOrganizationDto _SettingOrganization;

        public BLL.M.Mobile.GetSettingOrganizationDto SettingOrganization
        {
            get { return _SettingOrganization; }
            set { _SettingOrganization = value; OnPropertyChanged(); }
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
                        if (InfoBagCart.ProductInCart.Count == 0)
                            IsNoItems = true;
                        else
                            IsNoItems = false;
                }
                OnPropertyChanged();
            }
        }
        private bool _IsNoItems = false;

        public bool IsNoItems
        {
            get { return _IsNoItems; }
            set
            {
                if (_IsNoItems != value)
                    _IsNoItems= value; 
                OnPropertyChanged();
            }
        }

    }

    public class InfoBagCart : BaseProperty
    { 
        private ObservableCollection<BLL.M.Mobile.CartBag> _ProductInCart;
        public ObservableCollection<BLL.M.Mobile.CartBag> ProductInCart
        {
            get { return _ProductInCart ?? (_ProductInCart = new ObservableCollection<BLL.M.Mobile.CartBag>()); }
            set{ _ProductInCart = value; OnPropertyChanged(); }
        }
        private int _ItemsCount;
        public int ItemsCount
        {
            get { return _ItemsCount; }
            set { _ItemsCount = value; OnPropertyChanged(); }
        }
        private double _SubTotal;
        public double SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; OnPropertyChanged(); }
        }

        private double _AddtionTax;
        public double AddtionTax
        {
            get { return _AddtionTax; }
            set { _AddtionTax = value; OnPropertyChanged(); }
        }
        private double _TotalBeforeTax;
        public double TotalBeforeTax
        {
            get { return _TotalBeforeTax; }
            set { _TotalBeforeTax = value; OnPropertyChanged(); }
        }
        private double _TotalTax;
        public double TotalTax
        {
            get { return _TotalTax; }
            set { _TotalTax = value; OnPropertyChanged(); }
        }
        private double _TotalAmount;
        public double TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; OnPropertyChanged(); }
        }
    }
}