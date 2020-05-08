using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Identity
{

    public class UserProfile
    {
        [JsonProperty("id")]
        public int Id { get; set; }


        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("encryptionKey")]
        public string EncryptionKey { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("fstName")]
        public string FstName { get; set; }

        [JsonProperty("secName")]
        public string SecName { get; set; }

        [JsonProperty("thirdName")]
        public string ThirdName { get; set; }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("bod")]
        public DateTime? Bod { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("arFullName")]
        public string ArFullName { get; set; }

        [JsonProperty("enFullName")]
        public string EnFullName { get; set; }

        [JsonProperty("nationalityID")]
        public int NationalityId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("source")]
        public int Source { get; set; } = 2;

        [JsonProperty("isFstLog")]
        public bool IsFstLog { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }
    }
}
