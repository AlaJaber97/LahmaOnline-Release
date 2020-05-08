using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{

    public class PlacesMatchedSubstring
    {

        [Newtonsoft.Json.JsonProperty("length")]
        public int Length { get; set; }

        [Newtonsoft.Json.JsonProperty("offset")]
        public int Offset { get; set; }
    }

    public class PlacesTerm
    {

        [Newtonsoft.Json.JsonProperty("offset")]
        public int Offset { get; set; }

        [Newtonsoft.Json.JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Prediction
    {

        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("description")]
        public string Description { get; set; }

        [Newtonsoft.Json.JsonProperty("matched_substrings")]
        public List<PlacesMatchedSubstring> MatchedSubstrings { get; set; }

        [Newtonsoft.Json.JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [Newtonsoft.Json.JsonProperty("reference")]
        public string Reference { get; set; }

        [Newtonsoft.Json.JsonProperty("terms")]
        public List<PlacesTerm> Terms { get; set; }

        [Newtonsoft.Json.JsonProperty("types")]
        public List<string> Types { get; set; }
    }

    public class PlacesLocationPredictions
    {

        [Newtonsoft.Json.JsonProperty("predictions")]
        public List<Prediction> Predictions { get; set; }

        [Newtonsoft.Json.JsonProperty("status")]
        public string Status { get; set; }
    }

    public class SearchPlacesResponses
    {
        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }

    public  class Result
    {
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("plus_code")]
        public PlusCode PlusCode { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }

        [JsonProperty("user_ratings_total")]
        public int UserRatingsTotal { get; set; }
    }

    public   class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("viewport")]
        public Viewport Viewport { get; set; }
    }

    public   class Location
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public   class Viewport
    {
        [JsonProperty("northeast")]
        public Location Northeast { get; set; }

        [JsonProperty("southwest")]
        public Location Southwest { get; set; }
    }

    public   class Photo
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("html_attributions")]
        public List<string> HtmlAttributions { get; set; }

        [JsonProperty("photo_reference")]
        public string PhotoReference { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }

    public   class PlusCode
    {
        [JsonProperty("compound_code")]
        public string CompoundCode { get; set; }

        [JsonProperty("global_code")]
        public string GlobalCode { get; set; }
    }
}
