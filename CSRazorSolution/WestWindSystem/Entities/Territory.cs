﻿#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WestWindSystem.Entities
{
    public partial class Territory
    {
        [Key]
        [StringLength(20)]
        public string TerritoryID { get; set; }
        [Required]
        [StringLength(50)]
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }

        [ForeignKey(nameof(RegionID))]
        [InverseProperty("Territories")]
        public virtual Region Region { get; set; }
    }
}