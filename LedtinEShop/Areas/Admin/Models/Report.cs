using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LedtinEShop.Areas.Admin.Models
{
    public class Report
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double AVG { get; set; }
    }
}