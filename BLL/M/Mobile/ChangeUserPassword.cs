using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class ChangeUserPassword
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("encryptionKey")]
        public string EncryptionKey { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("oldPassword")]
        public string OldPassword { get; set; }
    }
}
