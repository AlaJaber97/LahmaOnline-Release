using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class Favourites
    {

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("itemId")]
        public int ItemId { get; set; }
    }
}
