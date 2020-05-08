using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class GetOrderByEmail
    {

        [JsonProperty("listItem")]
        public List<ListItem> ListItem { get; set; }

        [JsonProperty("id")]
        public decimal Id { get; set; }

        [JsonProperty("customerID")]
        public decimal CustomerId { get; set; }

        [JsonProperty("invoiceDate")]
        public DateTime InvoiceDate { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("paymentMethod")]
        public int PaymentMethod { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("reciveLocation")]
        public string ReciveLocation { get; set; }

        [JsonProperty("getMethod")]
        public int GetMethod { get; set; }

        [JsonProperty("userInfo")]
        public UserInfo UserInfo { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("totalPrice")]
        public int TotalPrice { get; set; }

        [JsonProperty("orderStatus")]
        public int OrderStatus { get; set; }

        [JsonProperty("deliverymanNote")]
        public string DeliverymanNote { get; set; }

        [JsonProperty("accountantNote")]
        public string AccountantNote { get; set; }

        [JsonProperty("arCustomreName")]
        public string ArCustomreName { get; set; }

        [JsonProperty("enCustomreName")]
        public string EnCustomreName { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("isNotificationSent")]
        public bool IsNotificationSent { get; set; }

        [JsonProperty("totalRecordCount")]
        public int TotalRecordCount { get; set; }

        [JsonProperty("arCityName")]
        public string ArCityName { get; set; }

        [JsonProperty("enCityName")]
        public string EnCityName { get; set; }

        [JsonProperty("customreName")]
        public string CustomreName { get; set; }
    }

    public class ListItem
    {
        [JsonProperty("itemID")]
        public int ItemId { get; set; }

        [JsonProperty("qty")]
        public decimal Qty { get; set; }

        [JsonProperty("arItemName")]
        public string ArItemName { get; set; }

        [JsonProperty("enItemName")]
        public string EnItemName { get; set; }

        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        [JsonProperty("arCustomerName")]
        public string ArCustomerName { get; set; }

        [JsonProperty("enCustomerName")]
        public string EnCustomerName { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("itemNote")]
        public string ItemNote { get; set; }

        [JsonProperty("reciveLocation")]
        public string ReciveLocation { get; set; }

        [JsonProperty("orderID")]
        public Int64 OrderId { get; set; }

        [JsonProperty("arOrigionName")]
        public string ArOrigionName { get; set; }

        [JsonProperty("enOrigionName")]
        public string EnOrigionName { get; set; }

        [JsonProperty("origionName")]
        public string OrigionName { get; set; }

        [JsonProperty("isFat")]
        public bool IsFat { get; set; }

        [JsonProperty("isSizing")]
        public bool IsSizing { get; set; }

        [JsonProperty("isNature")]
        public bool IsNature { get; set; }

        [JsonProperty("fat")]
        public int Fat { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("nature")]
        public int Nature { get; set; }

        [JsonProperty("imageURL")]
        public string ImageUrl { get; set; }

        [JsonProperty("rate")]
        public int Rate { get; set; }
    }

    public class UserInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

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
        public DateTime Bod { get; set; }

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
        public int Source { get; set; }

        [JsonProperty("isFstLog")]
        public bool IsFstLog { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }
    }
}
