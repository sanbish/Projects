using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmApi.Models
{
    public class Branches
    {
       public int branchId { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
        public bool rootEntity { get; set; }
        public int? parentId { get; set; }
        public string parentBranchName { get; set; }
        public string branchName { get; set; }
        public string code { get; set; }
        public string affiliateTag { get; set; }
        public string location { get; set; }
        public Nullable<byte> language { get; set; }
        public Nullable<byte> timeZone { get; set; }
        public  Nullable<bool> enabled { get; set; }
       
    }
}