﻿using System.Security.Cryptography.Xml;

namespace ObjectInfo
{
    public class UserInfo : MasterDataBase
    {
        public decimal STT { get; set; }
        public string? User_Name { get; set; }
        public string Full_Name { get; set; }

        public string? Password { get; set; }
        public string? Status { get; set; }
        public string? StatusText { get; set; }
        public decimal User_Type { get; set; }
        public string? User_Type_Text { get; set; }


        public decimal UserId { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Token { get; set; }
    }

    public class SAUser
    {
        public decimal UserId { get; set; }
        public string Full_Name { get; set; }
        public decimal User_Type { get; set; }
        public string User_Name { get; set; }
        public decimal Brid { get; set; }
        public decimal DPId { get; set; }
        public DateTime timeoutToken { get; set; }
    }

}
