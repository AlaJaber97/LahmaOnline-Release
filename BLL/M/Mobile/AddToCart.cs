using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class AddToCart
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("itemId")]
        public int ItemId { get; set; }

        [JsonProperty("qty")]
        public decimal Qty { get; set; }

        [JsonProperty("customerId")]
        public int UserId { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; } = string.Empty;

        [JsonProperty("guestID")]
        public string GuestId { get; set; } = string.Empty;

        [JsonProperty("fat")]
        public int Fat { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("nature")]
        public int Nature { get; set; }

        [JsonProperty("source")]
        public int Source { get; set; } = 2;
    }
}
