using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Identity
{
    public class GetCartByUserGustID
    {
        [JsonProperty("customerId")]
        public int UserId { get; set; }

        [JsonProperty("guestID")]
        public string GuestId { get; set; }
    }
}
