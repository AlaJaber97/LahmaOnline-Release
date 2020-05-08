using System;
using System.Collections.Generic;
using System.Text;

namespace LahmaOnline.ViewModel
{
    public class ProductCart :BLL.M.Mobile.Product 
    {
        public string Note { get; set; }
        public double Quentity { get; set; } = 1;
        public double TotalAmount => Quentity > 0 ? double.Parse(string.IsNullOrWhiteSpace(PriceSale)? "0": PriceSale) * Quentity : 0;
    }
}
