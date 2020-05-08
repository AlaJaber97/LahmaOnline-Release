using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Property
{
    public class HomeProperty : BaseProperty
    {
        private ObservableCollection<BLL.M.Mobile.Category> _Categories;

        public ObservableCollection<BLL.M.Mobile.Category> Categories
        {
            get { return _Categories?? (_Categories= new ObservableCollection<BLL.M.Mobile.Category>()); }
            set { _Categories = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ViewModel.ProductCart> _NewArrival;

        public ObservableCollection<ViewModel.ProductCart> NewArrival
        {
            get { return _NewArrival??(_NewArrival = new ObservableCollection<ViewModel.ProductCart>()); }
            set { _NewArrival = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ViewModel.ProductCart> _TopSelection;

        public ObservableCollection<ViewModel.ProductCart> TopSelection
        {
            get { return _TopSelection ?? (_TopSelection = new ObservableCollection<ViewModel.ProductCart>()); }
            set { _TopSelection = value; OnPropertyChanged(); }
        }

        private ViewModel.ProductCart _ProductSelected;
        public ViewModel.ProductCart ProductSelected
        {
            get { return _ProductSelected ?? (_ProductSelected = new ViewModel.ProductCart()); }
            set { _ProductSelected = value; OnPropertyChanged(); }
        }
    }
}