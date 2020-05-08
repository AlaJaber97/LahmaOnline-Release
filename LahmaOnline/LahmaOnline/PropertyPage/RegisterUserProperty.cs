using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LahmaOnline.Property
{
    public class RegisterUserProperty : BaseProperty
    {
        public BLL.M.Identity.UserProfile InfoUserRegister { get; set; } = new BLL.M.Identity.UserProfile();

        private ObservableCollection<BLL.M.Mobile.NationalityOrigin> _Nationality;
        public ObservableCollection<BLL.M.Mobile.NationalityOrigin> Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; OnPropertyChanged(); }
        }
        public List<string> GenderType { get; } = new List<string>
        {
            MultiLanguage.MLResource.Male,
            MultiLanguage.MLResource.Female
        };

        private string _GenderSelected = string.Empty;
        public string GenderSelected
        {
            get { return _GenderSelected; }
            set
            {
                if (value != null && _GenderSelected != value)
                {
                    _GenderSelected = value;
                }
                OnPropertyChanged();
            }
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
        public string MessageError { get; set; } = MultiLanguage.MLResource.TitleMessage;
    }
}
