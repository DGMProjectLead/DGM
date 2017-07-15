using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DGM_Checkout_dev.Models
{
    public class Location
    {
        public int LocationID { get; set; }

        [Display(Name = "Location")]
        [StringLength(10, ErrorMessage = "Location cannot be more than 10 characters.")]
        public string LocationEntry { get; set; }
    }
}
