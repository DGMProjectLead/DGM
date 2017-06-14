using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DGM_Checkout_dev.Models
{
    public class Type
    {
        public int TypeID { get; set; }

        [Display(Name = "Type")]
        [StringLength(25, ErrorMessage = "Type cannot be more than 25 characters.")]
        public string TypeEntry { get; set; }
    }
}
