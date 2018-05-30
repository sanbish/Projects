using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CrmApi.Models
{
    public class Brands
    {
        [Key]
        public int brandId { get; set; }
        public string brandName { get; set; }
        public bool rootEntity { get; set; }
        public int mainBranchId { get; set; }
        public int mainDepartmentId { get; set; }
        public string brandLocation { get; set; }
        public string website { get; set; }
        public int timeZone { get; set; }
        public bool enabled { get; set; }
        public TradingServer[] tradingServers { get; set; }
    }
}