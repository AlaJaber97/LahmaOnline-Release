using Newtonsoft.Json;

namespace BLL.M.Identity
{
    public class Login
    {
        [JsonProperty("email")]
        public string Mail { get; set; }

        [JsonProperty("encryptionKey")]
        public string EncryptionKey { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}