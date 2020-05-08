using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Property
{
    public class LoginProperty : BaseProperty
    {
        private BLL.M.Identity.Login _LoginUser;
        public BLL.M.Identity.Login LoginUser
        {
            get { return _LoginUser?? (_LoginUser = new BLL.M.Identity.Login()); }
            set { _LoginUser = value; OnPropertyChanged(); }
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