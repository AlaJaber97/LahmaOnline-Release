using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Identity
{
    public class ResponseGetUserProfile
    {
        [JsonProperty("user")]
        public UserProfile User { get; set; }

    }
}
