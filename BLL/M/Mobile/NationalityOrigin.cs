using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class NationalityOrigin
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("arName")]
        public string ArName { get; set; } = string.Empty;

        [JsonProperty("enName")]
        public string EnName { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("majorId")]
        public int FormId { get; set; }

        [JsonProperty("imageURL")]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
