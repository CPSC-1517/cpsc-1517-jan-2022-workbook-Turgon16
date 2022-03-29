﻿#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WestWindSystem.Entities
{
    [Index(nameof(CategoryID), Name = "CategoriesProducts")]
    [Index(nameof(CategoryID), Name = "CategoryID")]
    [Index(nameof(ProductName), Name = "ProductName")]
    [Index(nameof(SupplierID), Name = "SupplierID")]
    [Index(nameof(SupplierID), Name = "SuppliersProducts")]
    public partial class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        [Required]
        [StringLength(20)]
        public string QuantityPerUnit { get; set; }
        public short? MinimumOrderQuantity { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }

        [ForeignKey(nameof(CategoryID))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(SupplierID))]
        [InverseProperty("Products")]
        public virtual Supplier Supplier { get; set; }
    }
}