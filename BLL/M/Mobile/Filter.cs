using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class Filter
    {
        [JsonProperty("keyword")]
        public string Keyword { get; set; } = string.Empty;

        [JsonProperty("categoryID")]
        public int CategoryId { get; set; }

        [JsonProperty("origionID")]
        public int OrigionId { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty("description")]
        public int Description { get; set; }

        [JsonProperty("fromPrice")]
        public int FromPrice { get; set; }

        [JsonProperty("toPrice")]
        public int ToPrice { get; set; }

        [JsonProperty("customerId")]
        public int UserId { get; set; }
    }
}
