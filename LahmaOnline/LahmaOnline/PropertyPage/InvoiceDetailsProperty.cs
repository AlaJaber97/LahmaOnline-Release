using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.Property
{
    public class InvoiceDetailsProperty : BaseProperty
    {
        private BLL.M.Mobile.Invoice _Invoice;

        public BLL.M.Mobile.Invoice Invoice
        {
            get { return _Invoice ?? (_Invoice = new BLL.M.Mobile.Invoice()); }
            set { _Invoice = value; OnPropertyChanged(); }
        }
        private ReviewEvaluationUser _reviewComment;
        public ReviewEvaluationUser ReviewComment
        {
            get { return _reviewComment ?? (_reviewComment = new ReviewEvaluationUser()); }
            set { _reviewComment = value; OnPropertyChanged(); }
        }
        private BLL.M.Mobile.ListItem _ItemSelectd;

        public BLL.M.Mobile.ListItem ItemSelectd
        {
            get { return _ItemSelectd ?? (_ItemSelectd = new BLL.M.Mobile.ListItem()); }
            set { _ItemSelectd = value; OnPropertyChanged(); }
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
        public InvoiceDetailsProperty()
        {

        }
    }
}