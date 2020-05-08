using Newtonsoft.Json;
using System.Collections.Generic;

namespace BLL.M.Identity
{
    public class FullSystemHirarchy
    {
        [JsonProperty("lstSystems")]
        public List<string> LstSystems { get; set; }

        [JsonProperty("lstModules")]
        public List<string> LstModules { get; set; }

        [JsonProperty("lstPags")]
        public List<string> LstPags { get; set; }

        [JsonProperty("lstRoles")]
        public List<string> LstRoles { get; set; }
    }
}