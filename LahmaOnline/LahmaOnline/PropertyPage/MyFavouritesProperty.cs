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
    public class MyFavouritesProperty : BaseProperty
    {
        private ObservableCollection<ViewModel.ProductCart> _Products;

        public ObservableCollection<ViewModel.ProductCart> Products
        {
            get { return _Products ?? (_Products = new ObservableCollection<ViewModel.ProductCart>()); }
            set { _Products = value; OnPropertyChanged(); }
        }

        private ViewModel.ProductCart _ProductSelect;
        public ViewModel.ProductCart ProductSelect
        {
            get { return _ProductSelect ?? (_ProductSelect = new ViewModel.ProductCart()); }
            set { _ProductSelect = value; OnPropertyChanged(); }
        }
        public MyFavouritesProperty()
        {

        }
    }
}