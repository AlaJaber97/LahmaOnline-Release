using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Property
{
    public class ProfileProperty : BaseProperty
    {
        private BLL.M.Identity.UserProfile _UserProfile;

        public BLL.M.Identity.UserProfile UserProfile
        {
            get { return _UserProfile; }
            set { _UserProfile = value; OnPropertyChanged(); }
        }

        private int _LanguageIndex = AppStatics.Language -1;

        public int LanguageIndex
        {
            get { return _LanguageIndex; }
            set { _LanguageIndex = value; OnPropertyChanged(); }
        }
        DateTime _Birthday;
        public DateTime Birthday
        {
            get { return _Birthday; }
            set { _Birthday= value; OnPropertyChanged(); }
        }

        DateTime _MaxDate = DateTime.Now;
        public DateTime MaxDate
        {
            get { return _MaxDate; }
            set { _MaxDate= value; OnPropertyChanged(); }
        }
        public List<string> GenderType { get; } = new List<string>
        {
            MultiLanguage.MLResource.Male,
            MultiLanguage.MLResource.Female
        };

        private string _GenderSelected = MultiLanguage.MLResource.Male;
        public string GenderSelected
        {
            get { return _GenderSelected; }
            set
            {
                if (!string.IsNullOrEmpty(value) && _GenderSelected != value)
                {
                    _GenderSelected = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _AddressSelected;

        public string AddressSelected
        {
            get { return _AddressSelected; }
            set { if (_AddressSelected != value) { _AddressSelected = value; OnPropertyChanged(); } }
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
        public ProfileProperty()
        {
            GetUserProfile();
            LanguageIndex = Convert.ToInt32(AppStatics.Language - 1);
            if(UserProfile.Gender >=1 & UserProfile.Gender <= 2)
                GenderSelected = GenderType[UserProfile.Gender - 1]; // last
            AddressSelected = UserProfile.Address;
            Birthday = UserProfile.Bod ?? DateTime.Now;
        }

        private void GetUserProfile()
        {
            if (AppStatics.UserID > 0)
                UserProfile = AppStatics.UserProfile;
            else
                UserProfile = new BLL.M.Identity.UserProfile();
        }
    }
}