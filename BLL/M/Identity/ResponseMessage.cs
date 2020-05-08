using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Identity
{
    public class ResponseMessage
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("imageURl")]
        public string ImageURl { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
    }
}
