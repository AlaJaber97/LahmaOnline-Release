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
    public class ProductListProperty : BaseProperty
    {
        private int _CountResult = 0;
        public int CountResult
        {
            get { return _CountResult; }
            set { _CountResult = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BLL.M.Mobile.Product> _Products;

        public ObservableCollection<BLL.M.Mobile.Product> Products
        {
            get { return _Products ?? (_Products = new ObservableCollection<BLL.M.Mobile.Product>()); }
            set { _Products = value;if(value!=null) CountResult = value.Count(); OnPropertyChanged(); }
        }

        private BLL.M.Mobile.Product _ProductSelect;
        public BLL.M.Mobile.Product ProductSelect
        {
            get { return _ProductSelect ?? (_ProductSelect = new BLL.M.Mobile.Product()); }
            set { _ProductSelect = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ViewModel.Category> _AllCategory;
        public ObservableCollection<ViewModel.Category> AllCategory
        {
            get { return _AllCategory ?? (_AllCategory = new ObservableCollection<ViewModel.Category>()); }
            set { _AllCategory = value; OnPropertyChanged(); }
        }
        private ObservableCollection<BLL.M.Mobile.NationalityOrigin> _Origins;
        public ObservableCollection<BLL.M.Mobile.NationalityOrigin> Origins
        {
            get { return _Origins ?? (_Origins = new ObservableCollection<BLL.M.Mobile.NationalityOrigin>()); }
            set { _Origins = value; OnPropertyChanged(); }
        }

        private BLL.M.Mobile.Filter _PropertiesFilter;
        public BLL.M.Mobile.Filter PropertiesFilter
        {
            get { return _PropertiesFilter ?? (_PropertiesFilter = new BLL.M.Mobile.Filter()); }
            set { _PropertiesFilter = value; OnPropertyChanged(); }
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
                        if (Products.Count == 0)
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
            set { if (_IsNoItems != value) { SetProperty(ref _IsNoItems, value); } OnPropertyChanged(); }
        }

    }
}