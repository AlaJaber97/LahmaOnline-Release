using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    public class MessageModel
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public string From { get; set; }
        [JsonProperty("To")]
        public string User { get; set; }
        [JsonProperty("Text")]
        public string Message { get; set; }
        public bool IsReading { get; set; }
        [JsonIgnore]
        public bool IsOwnMessage { get; set; }
        [JsonIgnore]
        public bool IsSystemMessage { get; set; }
    }
}
