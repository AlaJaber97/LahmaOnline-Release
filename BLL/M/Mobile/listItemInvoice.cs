using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class ListItemInvoice
    {
        [JsonProperty("itemID")]
        public int ItemId { get; set; }

        [JsonProperty("qty")]
        public double Qty { get; set; }

        [JsonProperty("itemNote")]
        public string ItemNote { get; set; } = string.Empty;

        [JsonProperty("fat")]
        public int Fat { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("nature")]
        public int Nature { get; set; }

        [JsonProperty("itemCartID")]
        public int ItemCartId { get; set; }
    }
}
