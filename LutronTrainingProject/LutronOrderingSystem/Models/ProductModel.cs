using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LutronOrderingSystem.Models.Enums;

namespace LutronOrderingSystem.Models
{
    public class ProductModel
    {
        public int ModelId { get; set; }
        public string ModelDisplayString { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public ProductCategory Category { get; set; } 
        public int? NumberOfButtons { get; set; } //for ControlStation
        public MountType? MountType { get; set; } //for Enclosure

    }
}
