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
    public class TermsAndConditionsProperty : BaseProperty
    {
        private ObservableCollection<BLL.M.Mobile.InfoTermsAndConditions> _InfoTermsAndConditions;

        public ObservableCollection<BLL.M.Mobile.InfoTermsAndConditions> InfoTermsAndConditions
        {
            get { return _InfoTermsAndConditions ?? (_InfoTermsAndConditions = new ObservableCollection<BLL.M.Mobile.InfoTermsAndConditions>()); }
            set { _InfoTermsAndConditions = value; OnPropertyChanged(); }
        }
    }
}