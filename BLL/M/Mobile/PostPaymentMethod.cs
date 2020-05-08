using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class PostPaymentMethod
    {
        public string orderID { get; set; }
        public string systemLang { get; set; }

    }
}
