using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
using System.ComponentModel.DataAnnotations;

namespace ABCSupermarket_19001700_Final.Models
{
    public class Products : TableEntity 
    {
        public Products() { }

       
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public string FilePath { get; set; }
    }
}