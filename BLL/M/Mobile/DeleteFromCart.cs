using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class DeleteFromCart
    {
        [JsonProperty("id")]
        public int RecordId { get; set; }
    }
}
