using Newtonsoft.Json;

namespace BLL.M.Identity
{
    public class LayoutUserDto
    {
        [JsonProperty("userID")]
        public int UserId { get; set; }

        [JsonProperty("fontsize")]
        public string Fontsize { get; set; }

        [JsonProperty("fonttype")]
        public string Fonttype { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }
}