using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public class AddInvoice
    {
        [JsonProperty("listItemInvoice")]
        public List<ListItemInvoice> ListItemInvoice { get; set; } = new List<ListItemInvoice>();

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("customerID")]
        public int CustomerId { get; set; }

        [JsonProperty("invoiceDate")]
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        [JsonProperty("note")]
        public string Note { get; set; } =  string.Empty;

        [JsonProperty("paymentMethod")]
        public int PaymentMethod { get; set; } = 1;

        [JsonProperty("longitude")]
        public string Longitude { get; set; } = string.Empty;

        [JsonProperty("latitude")]
        public string Latitude { get; set; } = string.Empty;

        [JsonProperty("reciveLocation")]
        public string ReciveLocation { get; set; } = string.Empty;

        [JsonProperty("getMethod")]
        public int GetMethod { get; set; } = 1;

        [JsonProperty("mobile")]
        public string Mobile { get; set; } = string.Empty;

        [JsonProperty("city")]
        public int City { get; set; }

        [JsonProperty("source")]
        public int Source { get; set; } = 2;

        [JsonProperty("customerInfo")]
        public CustomerInfo CustomerInfo { get; set; }
        [JsonProperty("customerClassID")]
        public int CustomerClassId { get; set; }

        [JsonProperty("discountCode")]
        public string DiscountCode { get; set; }
    }

    public class CustomerInfo
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("encryptionKey")]
        public string EncryptionKey { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("arFullName")]
        public string ArFullName { get; set; }

        [JsonProperty("enFullName")]
        public string EnFullName { get; set; }
    }
}
