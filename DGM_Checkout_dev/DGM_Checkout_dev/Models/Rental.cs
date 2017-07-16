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

        [Display(Name = "Rental Name")]        
        [StringLength(20)]
        public string RentalName { get; set; }

        [Display(Name = "Checkout Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = false)]   
        public DateTime RentalCheckoutDate { get; set; }                                          

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MM-dd-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime RentalDueDate { get; set; }


        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? RentalReturnDate { get; set; }                                             

        [Display(Name = "Notes")]
        [StringLength(200)]
        public string RentalNotes { get; set; }
        
        [Display(Name = "Late Fee Due")]
        public bool RentalLateFee { get; set; }

        [Display(Name = "Late Fee Paid")]
        public bool RentalLateFeePaid { get; set; }

        [Display(Name = "Location")]
        [StringLength(25)]
        public string RentalLocation { get; set; }

        [Display(Name = "Renting Student")]
        public int UserID { get; set; }

        public User User { get; set; }

        public ICollection<Inventory> Inventory { get; set; }

        

    }
}
