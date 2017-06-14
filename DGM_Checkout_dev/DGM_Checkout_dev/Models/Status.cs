using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGM_Checkout_dev.Models
{
    public enum StatusEntry
    {
        Active, Repair, Surplus, MIA
    }

    public class Status
    {
        public int StatusID { get; set; }
        public StatusEntry StatusEntry { get; set; }
    }
}
