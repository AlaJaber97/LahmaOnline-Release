using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class SaveEvalutionItemDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("itemId")]
        public int ItemId { get; set; }

        [JsonProperty("orderID")]
        public Int64 OrderId { get; set; }
    }
}
