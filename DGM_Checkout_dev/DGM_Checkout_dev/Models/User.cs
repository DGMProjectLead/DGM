using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace DGM_Checkout_dev.Models
{
    public class User
    {
        public int UserID { get; set; }    

        [Required]
        [Display(Name = "UVID")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Please enter an 8 digit UVID number.")]
        public string UVID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters")]   
        public string UserFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters")]
        public string UserLastName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Phone number required for a new student.")]   
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Please enter a phone number with dashes: xxx-xxx-xxxx ")]
        public string UserPhone { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email is required for a new student.")] 
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

        public string UserFullName
        {
            get
            {
                return UserFirstName + " " + UserLastName;
            }
        }

        public ICollection<Rental> Rentals { get; set; }   
    }
}
