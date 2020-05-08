using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class GetSalesInvoiceByEmail
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 5;

        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; } = 0;
    }
}
