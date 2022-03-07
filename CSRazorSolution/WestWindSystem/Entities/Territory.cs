using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace WestWindSystem.Entities
{
    public class Territory
    {
        [Key]
        public string TerritoryID { get; set; }
        [Required(ErrorMessage = "Region Description Required")]
        [StringLength(50, ErrorMessage = "Region description is limited to 50 characters.")]
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }

    }
}
