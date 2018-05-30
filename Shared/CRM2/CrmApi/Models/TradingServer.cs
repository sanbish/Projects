using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CrmApi.Models
{
    public class TradingServer
    {
        [Key]
        public int ServerId { get; set; }
        public string ServerName { get; set; }
        public string Url { get; set; }
    }
}