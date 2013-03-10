using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sentinel.Models
{
    public class MenuViewModel
    {
        public string ID { get; set; }
        public string URL { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }
        public string Permission { get; set; }
    }
}