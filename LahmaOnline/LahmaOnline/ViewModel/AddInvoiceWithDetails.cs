using LahmaOnline.Property;
using System;
using System.Collections.Generic;
using System.Text;

namespace LahmaOnline.ViewModel
{
    public class AddInvoiceWithDetails : BaseProperty
    {
        private BLL.M.Mobile.AddInvoice _ModelCart;
        public BLL.M.Mobile.AddInvoice ModelCart 
        {
            get
            {
                return _ModelCart?? (_ModelCart = new BLL.M.Mobile.AddInvoice
                {
                    Source = 2,
                    InvoiceDate = DateTime.Now,
                    CustomerId = AppStatics.UserID,
                    CustomerInfo = new BLL.M.Mobile.CustomerInfo
                    {
                        ArFullName = AppStatics.UserProfile?.ArFullName ?? string.Empty,
                        EnFullName = AppStatics.UserProfile?.EnFullName ?? string.Empty,
                        Email = AppStatics.UserProfile?.Email ?? string.Empty,
                        Mobile = AppStatics.UserProfile?.Mobile ?? string.Empty,
                        Password = string.Empty
                    },
                    ReciveLocation = AppStatics.UserProfile?.Address ?? string.Empty,
                    ListItemInvoice = new List<BLL.M.Mobile.ListItemInvoice>(),
                });
            }
            set { _ModelCart = value; }
        }

        private int _ItemsCount;
        public int ItemsCount
        {
            get { return _ItemsCount; }
            set { _ItemsCount = value; OnPropertyChanged(); }
        }
        private double _SubTotal;
        public double SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; OnPropertyChanged(); }
        }

        private double _AddtionTax;
        public double AddtionTax
        {
            get { return _AddtionTax; }
            set { _AddtionTax = value; OnPropertyChanged(); }
        }
        private double _TotalBeforeTax;
        public double TotalBeforeTax
        {
            get { return _TotalBeforeTax; }
            set { _TotalBeforeTax = value; OnPropertyChanged(); }
        }
        private double _TotalTax;
        public double TotalTax
        {
            get { return _TotalTax; }
            set { _TotalTax = value; OnPropertyChanged(); }
        }
        private double _TotalAmount;
        public double TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; OnPropertyChanged(); }
        }
    }
}
