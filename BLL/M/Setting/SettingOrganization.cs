using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Setting
{
    public class SettingOrganization
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("groupId")]
        public int GroupId { get; set; }

        [JsonProperty("companyId")]
        public int CompanyId { get; set; }

        [JsonProperty("branchID")]
        public int BranchId { get; set; }

        [JsonProperty("gallaryID")]
        public int GallaryId { get; set; }

        [JsonProperty("isAllPage")]
        public bool IsAllPage { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("tempPath")]
        public string TempPath { get; set; }

        [JsonProperty("pageURL")]
        public Uri PageUrl { get; set; }

        [JsonProperty("postStatus")]
        public bool PostStatus { get; set; }

        [JsonProperty("isUserCust")]
        public bool IsUserCust { get; set; }

        [JsonProperty("payOnCash")]
        public int PayOnCash { get; set; }

        [JsonProperty("minTotalOrder")]
        public int MinTotalOrder { get; set; }

        [JsonProperty("chargeDelivery")]
        public int ChargeDelivery { get; set; }

        [JsonProperty("signCurrencyEN")]
        public string SignCurrencyEn { get; set; }

        [JsonProperty("signCurrencyAR")]
        public string SignCurrencyAr { get; set; }

        [JsonProperty("arGroupName")]
        public object ArGroupName { get; set; }

        [JsonProperty("enGroupName")]
        public object EnGroupName { get; set; }

        [JsonProperty("arCompoanyName")]
        public object ArCompoanyName { get; set; }

        [JsonProperty("enCompoanyName")]
        public object EnCompoanyName { get; set; }

        [JsonProperty("smsUrlGateway")]
        public object SmsUrlGateway { get; set; }

        [JsonProperty("smsUserName")]
        public object SmsUserName { get; set; }

        [JsonProperty("smsPassword")]
        public object SmsPassword { get; set; }

        [JsonProperty("smtp")]
        public object Smtp { get; set; }

        [JsonProperty("fromMail")]
        public object FromMail { get; set; }

        [JsonProperty("smtpPort")]
        public int SmtpPort { get; set; }

        [JsonProperty("smtpUserName")]
        public object SmtpUserName { get; set; }

        [JsonProperty("smtpPassword")]
        public object SmtpPassword { get; set; }

        [JsonProperty("smtpEnableSSL")]
        public bool SmtpEnableSsl { get; set; }

        [JsonProperty("smtpEnableTLS")]
        public bool SmtpEnableTls { get; set; }

        [JsonProperty("adminMail")]
        public object AdminMail { get; set; }

        [JsonProperty("applicationDateFormat")]
        public object ApplicationDateFormat { get; set; }

        [JsonProperty("dataBaseDateFormat")]
        public object DataBaseDateFormat { get; set; }

        [JsonProperty("useUnifiedLogin")]
        public bool UseUnifiedLogin { get; set; }

        [JsonProperty("connectionString")]
        public object ConnectionString { get; set; }

        [JsonProperty("connectionLDAB")]
        public object ConnectionLdab { get; set; }

        [JsonProperty("mailNickName")]
        public object MailNickName { get; set; }

        [JsonProperty("smsClientName")]
        public object SmsClientName { get; set; }

        [JsonProperty("optPickup")]
        public bool OptPickup { get; set; }

        [JsonProperty("smsType")]
        public int SmsType { get; set; }

        [JsonProperty("isMailCreateUSer")]
        public bool IsMailCreateUSer { get; set; }

        [JsonProperty("isSMSCreateUSer")]
        public bool IsSmsCreateUSer { get; set; }

        [JsonProperty("workerRole")]
        public int WorkerRole { get; set; }

        [JsonProperty("deliveryRole")]
        public int DeliveryRole { get; set; }

        [JsonProperty("accountantRole")]
        public int AccountantRole { get; set; }

        [JsonProperty("assignOrderRole")]
        public int AssignOrderRole { get; set; }

        [JsonProperty("tax")]
        public int Tax { get; set; }

        [JsonProperty("resetPasswordURL")]
        public object ResetPasswordUrl { get; set; }

        [JsonProperty("confirmationEmailCreateUser")]
        public bool ConfirmationEmailCreateUser { get; set; }

        [JsonProperty("confirmationSMSCreateUser")]
        public bool ConfirmationSmsCreateUser { get; set; }

        [JsonProperty("confirmationCreateUserURL")]
        public object ConfirmationCreateUserUrl { get; set; }

        [JsonProperty("offSetTime")]
        public int OffSetTime { get; set; }

        [JsonProperty("startWorkHour")]
        public int StartWorkHour { get; set; }

        [JsonProperty("endWorkHour")]
        public int EndWorkHour { get; set; }

        [JsonProperty("signCurrency")]
        public string SignCurrency { get; set; }
    }
}
