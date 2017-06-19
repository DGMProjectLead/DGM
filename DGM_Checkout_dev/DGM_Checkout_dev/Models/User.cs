using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;    

namespace DGM_Checkout_dev.Models
{
    public class User
    {
        public int UserID { get; set; }                       //set a primary key as int, mvc reads ID or <class name>ID as primary key automatically

        [Required]
        [Display(Name = "UVID")]
        public int UVID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters")]   //Set the max string length, and generate error message for long strings
        public string UserFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters")]
        public string UserLastName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Please enter a valid phone number")]             //'Phone' should set phone format to xxx-xxx-xxxx or (xxx) xxx-xxxx, need to check
        public string UserPhone { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]     //'EmailAddress' should validate for email addresses like '@xxxxx.yyy' 
        public string UserEmail { get; set; }

        [Display(Name = "Notes")]
        [StringLength(200)]
        public string UserNotes { get; set; }

        public string UserFullInfo
        {
            get
            {
                return UVID + " - " + UserFirstName + " " + UserLastName;
            }
        }

        public ICollection<Rental> Rentals { get; set; }    //Navigation property to link students to multiple rentals, also used for one-to-many or many-to-many relationships
    }
}
