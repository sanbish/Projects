//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrmApi.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class BrandInfo
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
    
        public virtual Branch Branch { get; set; }
        public virtual Department Department { get; set; }
        public virtual User User { get; set; }
    }
}
