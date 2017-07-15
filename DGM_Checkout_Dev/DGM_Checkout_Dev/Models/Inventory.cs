using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DGM_Checkout_dev.Models
{
    public class Inventory
    {
        public int InventoryID { get; set; }

        [Required]
        [Display(Name = "Serial Number")]
        [StringLength(20)]
        public string InventorySerialNumber { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(70, ErrorMessage = "Name cannot be more than 30 characters")]
        public string InventoryName { get; set; }

        [Display(Name = "Make")]
        [StringLength(30)]
        public string InventoryMake { get; set; }

        [Display(Name = "Model")]
        [StringLength(30)]
        public string InventoryModel { get; set; }

        [Display(Name = "Notes")]
        [StringLength(200)]
        public string InventoryNotes { get; set; }

        [Display(Name = "Cost")]
        [DataType(DataType.Currency)]
        public decimal InventoryCost { get; set; }

        //Foreign keys for Type, Location, and Status entities
        //One-to-One relationships
        [Display(Name = "Type of Item")]
        public int TypeID { get; set; }

        [Display(Name = "Location")]
        public int LocationID { get; set; }

        [Display(Name ="Item Status")]
        public int StatusID { get; set; }

        [Display(Name ="Rental")]
        public int? RentalID { get; set; }

        public Type Type { get; set; }
        public Location Location { get; set; }
        public Status Status { get; set; }
        public Rental Rental { get; set; }
    }
}
