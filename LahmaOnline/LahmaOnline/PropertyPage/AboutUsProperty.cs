using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Property
{
    public class AboutUsProperty : BaseProperty
    {
        private string _TitlePage;

        public string TitlePage
        {
            get { return _TitlePage; }
            set { _TitlePage = value; OnPropertyChanged(); }
        }
        private string _TextPage;

        public string TextPage
        {
            get { return _TextPage; }
            set { _TextPage = value; OnPropertyChanged(); }
        }
    }
}