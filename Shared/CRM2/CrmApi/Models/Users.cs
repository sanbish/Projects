using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmApi.Models
{
    public class Users
    {
        public int userId { get; set; }
        public long memebershipUserId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string profilePicture { get; set; }
        public string signturePicture { get; set; }
        public string firstName { get; set; }
        public string fullName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }
        public string memberSince { get; set; }
        public string location { get; set; }
        public Nullable<int> languageId { get; set; }
        public string languageName { get; set; }
        public Nullable<int> position { get; set; }
        public string job { get; set; }
        public string voIpExtension { get; set; }
        public string voIpSip { get; set; }
        public string promoCode { get; set; }
        public Nullable<bool> depositSmsNotification { get; set; }
        public bool enabled { get; set; }
        public bool isOnline { get; set; }       
        public List<BrandInfo> BrandInfoes { get; set; }
    }
}