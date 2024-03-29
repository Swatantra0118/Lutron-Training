﻿namespace OrderServiceAPI.Models
{
    public class ProductModel
    {
        public enum ProductCategory
        {
            ControlStation,
            Enclosure
        }
        public enum MountTypeEnum
        {
            WallMount,
            TableTop,
            PanelMount
        }
        public int ModelId { get; set; }
        public string ModelDisplayString { get; set; }
        public string Description { get; set; }
        public ProductCategory Category { get; set; }
        public int? NumberOfButtons { get; set; }
        public MountTypeEnum? MountType { get; set; } // Only applicable for Enclosure
        public int Quantity { get; set; }
    }
}
