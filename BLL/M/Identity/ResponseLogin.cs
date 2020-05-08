using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Identity
{
    public class ResponseLogin
    {
        [JsonProperty("user")]
        public UserProfile User { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("fullSystemHirarchy")]
        public FullSystemHirarchy FullSystemHirarchy { get; set; }

        [JsonProperty("layoutUserDto")]
        public LayoutUserDto LayoutUserDto { get; set; }
    }
}
