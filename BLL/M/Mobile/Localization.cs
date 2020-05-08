using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class Localization
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("languageId")]
        public int LanguageId { get; set; }

        [JsonProperty("resourceName")]
        public string ResourceName { get; set; } = string.Empty;

        [JsonProperty("resourceValue")]
        public string ResourceValue { get; set; } = string.Empty;
    }
}
