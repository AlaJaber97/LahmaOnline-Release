using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class Category
    {

        [JsonProperty("lstItem")]
        public List<LstItem> LstItem { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("arName")]
        public string ArName { get; set; }

        [JsonProperty("enName")]
        public string EnName { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("imageURL")]
        public string ImageUrl { get; set; }

        [JsonProperty("altImageURL")]
        public string AltImageUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public partial class LstItem
    {
        [JsonProperty("lstRelatedItem")]
        public List<object> LstRelatedItem { get; set; }

        [JsonProperty("lstItemComment")]
        public List<LstItemComment> LstItemComment { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("arName")]
        public string ArName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("enName")]
        public string EnName { get; set; }

        [JsonProperty("arShortDescription")]
        public string ArShortDescription { get; set; }

        [JsonProperty("enShortDescription")]
        public string EnShortDescription { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("imageURL")]
        public string ImageUrl { get; set; }

        [JsonProperty("barCode")]
        public string BarCode { get; set; }

        [JsonProperty("priceSale")]
        public string PriceSale { get; set; }

        [JsonProperty("weightAR")]
        public string WeightAr { get; set; }

        [JsonProperty("weightEN")]
        public string WeightEn { get; set; }

        [JsonProperty("arShortCut")]
        public string ArShortCut { get; set; }

        [JsonProperty("enShortCut")]
        public string EnShortCut { get; set; }

        [JsonProperty("measurUnit")]
        public int MeasurUnit { get; set; }

        [JsonProperty("countryOriginAR")]
        public string CountryOriginAr { get; set; }

        [JsonProperty("countryOriginEN")]
        public string CountryOriginEn { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

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

        [JsonProperty("catarName")]
        public string CatarName { get; set; }

        [JsonProperty("catenName")]
        public string CatenName { get; set; }

        [JsonProperty("catName")]
        public string CatName { get; set; }

        [JsonProperty("arFullShortCut")]
        public string ArFullShortCut { get; set; }

        [JsonProperty("enFullShortCut")]
        public string EnFullShortCut { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("rate")]
        public int Rate { get; set; }

        [JsonProperty("fullShortCut")]
        public string FullShortCut { get; set; }

        [JsonProperty("shortCut")]
        public string ShortCut { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("wishlistItem")]
        public bool WishlistItem { get; set; }

        [JsonProperty("evaluationComment")]
        public string EvaluationComment { get; set; }
    }
}