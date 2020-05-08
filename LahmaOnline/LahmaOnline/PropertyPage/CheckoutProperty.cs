using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Property
{
    public class CheckoutProperty : BaseProperty
    {
        private BLL.M.Mobile.AddInvoice _ModelCart;
        public BLL.M.Mobile.AddInvoice ModelCart
        {
            get { return _ModelCart; }
            set { _ModelCart = value; OnPropertyChanged(); }
        }
        private bool _IsLoading = false;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                if (_IsLoading != value)
                {
                    _IsLoading = value;
                }
                OnPropertyChanged();
            }
        }
        private ReciveLocation _InfoLocation = null;
        public ReciveLocation InfoLocation
        {
            get
            {
                if (_InfoLocation == null)
                {
                    _InfoLocation = new ReciveLocation
                    {
                        LocationName = Xamarin.Essentials.Preferences.Get("NameReciveLocation", string.Empty),
                        Location = new Xamarin.Essentials.Location
                        {
                            Latitude = Xamarin.Essentials.Preferences.Get("Latitude", 24.466406),
                            Longitude = Xamarin.Essentials.Preferences.Get("Longitude", 54.3655755)
                        }
                    };
                }    

              return _InfoLocation;
            }
            set
            {
                _InfoLocation = value;
                OnPropertyChanged();
            }
        }
        #region Details Bill
            private int _ItemsCount = 0;
            public int ItemsCount
            {
                get { return _ItemsCount; }
                set { _ItemsCount = value; OnPropertyChanged(); }
            }
            private double _SubTotal = 0;
            public double SubTotal
            {
                get { return _SubTotal; }
                set { _SubTotal = value; OnPropertyChanged(); }
            }
            private double _AddtionTax = 0;
            public double AddtionTax
            {
                get { return _AddtionTax; }
                set { _AddtionTax = value; OnPropertyChanged(); }
            }
            private double _TotalBeforeTax = 0;
            public double TotalBeforeTax
            {
                get { return _TotalBeforeTax; }
                set { _TotalBeforeTax = value; OnPropertyChanged(); }
            }
            private double _TotalTax = 0;
            public double TotalTax
            {
                get { return _TotalTax; }
                set { _TotalTax = value; OnPropertyChanged(); }
            }
            private double _TotalAmount = 0;
            public double TotalAmount
            {
                get { return _TotalAmount; }
                set { _TotalAmount = value; OnPropertyChanged(); }
            }
            private double _DeliveryCharges = 0;
            public double DeliveryCharges
            {
                get { return _DeliveryCharges; }
                set { _DeliveryCharges = value; OnPropertyChanged(); }
            }
        #endregion
        #region Details Info ReceiveLocation
            private List<BLL.M.Mobile.NationalityOrigin> _DeliveryArea;
            public List<BLL.M.Mobile.NationalityOrigin> DeliveryArea
            {
                get { return _DeliveryArea ?? (_DeliveryArea = new List<BLL.M.Mobile.NationalityOrigin>()); }
                set { _DeliveryArea = value; DeliverySelect = value.FirstOrDefault(); OnPropertyChanged(); }
            }
            private BLL.M.Mobile.NationalityOrigin _DeliverySelect;
            public BLL.M.Mobile.NationalityOrigin DeliverySelect
            {
                get { return _DeliverySelect; }
                set
                {
                    if (value != null && _DeliverySelect != value)
                    {
                        _DeliverySelect = value;
                    }
                    OnPropertyChanged();
                }
            }
        #endregion
        #region info Map
        private Services.GooglePlacesApiSearchPlaces _GooglePlacesApiAutoComplet;
        public Services.GooglePlacesApiSearchPlaces GooglePlacesApiAutoComplet
        {
            get { return _GooglePlacesApiAutoComplet?? (_GooglePlacesApiAutoComplet = new Services.GooglePlacesApiSearchPlaces()); }
            set { _GooglePlacesApiAutoComplet = value; OnPropertyChanged(); }
        }

        #endregion
        public string MessageError { get; set; } = MultiLanguage.MLResource.TitleMessage;
    }
    public class ReciveLocation
    {
        public string LocationName { get; set; }
        public Xamarin.Essentials.Location Location { get; set; }
    }
}