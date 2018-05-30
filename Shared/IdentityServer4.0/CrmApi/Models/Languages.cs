using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmApi.Models
{
    public class Languages
    {
        
        public byte id { get; set; }
        
        public string name { get; set; }
        public string code { get; set; }
        public bool status { get; set; }
    }
}