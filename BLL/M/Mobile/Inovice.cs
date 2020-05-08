using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class Invoice
    {
        [JsonProperty("listItem")]
        public List<ListItem> ListItem { get; set; }

        [JsonIgnore]
        public List<ListItem> ListItemMax3Only => ListItem.Take(3).ToList();

        [JsonIgnore]
        public bool IsThereMore3items => ListItem.Count > 3;

        [JsonProperty("id")]
        public decimal Id { get; set; }

        [JsonProperty("customerID")]
        public decimal CustomerId { get; set; }

        [JsonProperty("invoiceDate")]
        public string InvoiceDate { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; } = string.Empty;

        [JsonProperty("paymentMethod")]
        public int PaymentMethod { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; } = string.Empty;

        [JsonProperty("latitude")]
        public string Latitude { get; set; } = string.Empty;

        [JsonProperty("reciveLocation")]
        public string ReciveLocation { get; set; } = string.Empty;

        [JsonProperty("getMethod")]
        public int GetMethod { get; set; } = 1;

        [JsonProperty("userInfo")]
        public BLL.M.Identity.UserProfile UserInfo { get; set; }
        
        [JsonProperty("mobile")]
        public string Mobile { get; set; } = string.Empty;

        [JsonProperty("totalPrice")]
        public decimal TotalPrice { get; set; }

        [JsonProperty("orderStatus")]
        public int OrderStatus { get; set; }

        [JsonProperty("deliverymanNote")]
        public string DeliverymanNote { get; set; } = string.Empty;

        [JsonProperty("accountantNote")]
        public string AccountantNote { get; set; } = string.Empty;

        [JsonProperty("arCustomreName")]
        public string ArCustomreName { get; set; } = string.Empty;

        [JsonProperty("enCustomreName")]
        public string EnCustomreName { get; set; } = string.Empty;

        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("isNotificationSent")]
        public bool IsNotificationSent { get; set; }

        [JsonProperty("totalRecordCount")]
        public int TotalRecordCount { get; set; }

        [JsonProperty("arCityName")]
        public string ArCityName { get; set; } = string.Empty;

        [JsonProperty("enCityName")]
        public string EnCityName { get; set; } = string.Empty;

        [JsonProperty("customreName")]
        public string CustomreName { get; set; } = string.Empty;
    }
}
