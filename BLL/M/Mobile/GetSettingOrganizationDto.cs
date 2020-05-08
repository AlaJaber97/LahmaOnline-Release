using Newtonsoft.Json;

namespace BLL.M.Mobile
{
    [Preserve(AllMembers = true)]
    public  class GetSettingOrganizationDto
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
        public string PageUrl { get; set; }

        [JsonProperty("postStatus")]
        public bool PostStatus { get; set; }

        [JsonProperty("isUserCust")]
        public bool IsUserCust { get; set; }

        [JsonProperty("payOnCash")]
        public decimal PayOnCash { get; set; }

        [JsonProperty("minTotalOrder")]
        public decimal MinTotalOrder { get; set; }

        [JsonProperty("chargeDelivery")]
        public decimal ChargeDelivery { get; set; }

        [JsonProperty("signCurrencyEN")]
        public string SignCurrencyEn { get; set; }

        [JsonProperty("signCurrencyAR")]
        public string SignCurrencyAr { get; set; }

        [JsonProperty("arGroupName")]
        public string ArGroupName { get; set; }

        [JsonProperty("enGroupName")]
        public string EnGroupName { get; set; }

        [JsonProperty("arCompoanyName")]
        public string ArCompoanyName { get; set; }

        [JsonProperty("enCompoanyName")]
        public string EnCompoanyName { get; set; }

        [JsonProperty("smsUrlGateway")]
        public string SmsUrlGateway { get; set; }

        [JsonProperty("smsUserName")]
        public string SmsUserName { get; set; }

        [JsonProperty("smsPassword")]
        public string SmsPassword { get; set; }

        [JsonProperty("smtp")]
        public string Smtp { get; set; }

        [JsonProperty("fromMail")]
        public string FromMail { get; set; }

        [JsonProperty("smtpPort")]
        public int SmtpPort { get; set; }

        [JsonProperty("smtpUserName")]
        public string SmtpUserName { get; set; }

        [JsonProperty("smtpPassword")]
        public string SmtpPassword { get; set; }

        [JsonProperty("smtpEnableSSL")]
        public bool SmtpEnableSsl { get; set; }

        [JsonProperty("smtpEnableTLS")]
        public bool SmtpEnableTls { get; set; }

        [JsonProperty("adminMail")]
        public string AdminMail { get; set; }

        [JsonProperty("applicationDateFormat")]
        public string ApplicationDateFormat { get; set; }

        [JsonProperty("dataBaseDateFormat")]
        public string DataBaseDateFormat { get; set; }

        [JsonProperty("useUnifiedLogin")]
        public bool UseUnifiedLogin { get; set; }

        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }

        [JsonProperty("connectionLDAB")]
        public string ConnectionLdab { get; set; }

        [JsonProperty("mailNickName")]
        public string MailNickName { get; set; }

        [JsonProperty("smsClientName")]
        public string SmsClientName { get; set; }

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
        public decimal Tax { get; set; }

        [JsonProperty("resetPasswordURL")]
        public string ResetPasswordUrl { get; set; }

        [JsonProperty("confirmationEmailCreateUser")]
        public bool ConfirmationEmailCreateUser { get; set; }

        [JsonProperty("confirmationSMSCreateUser")]
        public bool ConfirmationSmsCreateUser { get; set; }

        [JsonProperty("confirmationCreateUserURL")]
        public string ConfirmationCreateUserUrl { get; set; }

        [JsonProperty("offSetTime")]
        public int OffSetTime { get; set; }

        [JsonProperty("startWorkHour")]
        public int StartWorkHour { get; set; }

        [JsonProperty("endWorkHour")]
        public int EndWorkHour { get; set; }

        [JsonProperty("signCurrency")]
        public string SignCurrency { get; set; }

        [JsonProperty("isSentInvioceEmail")]
        public bool IsSentInvioceEmail { get; set; }

        [JsonProperty("isSentInvioceSms")]
        public bool IsSentInvioceSms { get; set; }

        [JsonProperty("isSentCreateUserEmail")]
        public bool IsSentCreateUserEmail { get; set; }

        [JsonProperty("isSentCreateUserSms")]
        public bool IsSentCreateUserSms { get; set; }

        [JsonProperty("adminUserId")]
        public int AdminUserId { get; set; }
    }
}
