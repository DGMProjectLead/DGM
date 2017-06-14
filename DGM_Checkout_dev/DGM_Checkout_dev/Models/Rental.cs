using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DGM_Checkout_dev.Models
{
    public class Rental
    {
        public int RentalID { get; set; }

        [Display(Name = "Rental Name")]         //Rental name to store information in place of composite primary key
        [StringLength(20)]
        public string RentalName { get; set; }

        [Display(Name = "Checkout Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = false)]        //set format of the date to display on the page
        public DateTime RentalCheckoutDate { get; set; }                                           //Set ApplyFormatInEditMode to false to save values in Edit View

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MM-dd-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime RentalDueDate { get; set; }

        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? RentalReturnDate { get; set; }                                             //Make ReturnDate nullable upon creation

        [Display(Name = "Notes")]
        [StringLength(200)]
        public string RentalNotes { get; set; }

        //TODO
        //Get datatype for boolean checkboxes
        [Display(Name = "Late Fee Due")]
        public bool RentalLateFee { get; set; }

        [Display(Name = "Late Fee Paid")]
        public bool RentalLateFeePaid { get; set; }

        [Display(Name = "Location")]
        [StringLength(25)]
        public string RentalLocation { get; set; }

        [Display(Name = "Rented to: ")]
        public int UserID { get; set; }

        //[Display(Name = "Item")]
        //public int InventoryID { get; set; }

        //TODO
        //Need to add foreign keys for user and inventory

        public User User { get; set; }
        //public Inventory Inventory { get; set; }

        public ICollection<Inventory> Inventory { get; set; }

    }
}
