using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class GetByIdItemDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
