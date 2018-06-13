using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmApi.Models
{
    public class BrandInfo
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
        public int branchId { get; set; }
        public string branchName { get; set; }
        public Nullable<int> departmentId { get; set; }
        public string departmentName { get; set; }
        public Nullable<bool> departmentManager { get; set; }
        public Nullable<int> managerId { get; set; }
        public Nullable<int> workingHoursStart { get; set; }
        public Nullable<int> workingHoursEnd { get; set; }

        public ICollection<IdText> branches { get; set; }
        public ICollection<IdText> departments { get; set; }
    }



    public class IdText
    {
        public int id { get; set; }
        public string text { get; set; }
    }
}