using System.Security.Cryptography.Xml;

namespace ObjectInfo
{
    public class UserInfo : MasterDataBase
    {
        public decimal STT { get; set; }
        public decimal UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string StatusText { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string UserTypeText { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }

    public class SAUser
    {
        public decimal UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string User_Type { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        //public decimal Brid { get; set; }
        //public decimal DPId { get; set; }
        //public DateTime timeoutToken { get; set; }
    }

}
