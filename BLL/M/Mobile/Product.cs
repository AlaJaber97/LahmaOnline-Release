using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class Product
    {
        [JsonProperty("lstRelatedItem")]
        public List<object> LstRelatedItem { get; set; }

        [JsonProperty("lstItemComment")]
        public List<LstItemComment> LstItemComment { get; set; } = new List<LstItemComment>();

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("catName")]
        public string CategoryName { get; set; } = string.Empty;

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; } = string.Empty;

        [JsonProperty("imageURL")]
        public string ImageUrl { get; set; } = string.Empty;

        [JsonProperty("barCode")]
        public string BarCode { get; set; } = string.Empty;

        [JsonProperty("priceSale")]
        public string PriceSale { get; set; } = string.Empty;

        [JsonProperty("weight")]
        public string Weight { get; set; } = string.Empty;

        [JsonProperty("shortCut")]
        public string ShortCut { get; set; } = string.Empty;

        [JsonProperty("measurUnit")]
        public decimal MeasurUnit { get; set; } 

        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;

        [JsonProperty("nationalityID")]
        public int NationalityId { get; set; }

        [JsonProperty("isBone")]
        public bool IsBone { get; set; }

        [JsonProperty("isFat")]
        public bool IsFat { get; set; }

        [JsonProperty("isSizing")]
        public bool IsSizing { get; set; }

        [JsonProperty("isNature")]
        public bool IsNature { get; set; }

        [JsonProperty("totalRecordCount")]
        public int TotalRecordCount { get; set; }

        [JsonProperty("fullShortCut")]
        public string FullShortCut { get; set; } = string.Empty;

        [JsonProperty("tags")]
        public string Tags { get; set; } = string.Empty;

        [JsonProperty("rate")]
        public int Rate { get; set; }

        [JsonProperty("wishlistItem")]
        public bool IsWishlist { get; set; }
    }
    public class LstItemComment
    {
        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonIgnore]
        public string ImageDefualt => $"imageProfile{(new Random(Comment.Length)).Next(1,10)}.svg";
    }
}
