using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGM_Checkout_dev.Models.ViewModels
{
    public class RentalInventoryData
    {
        public IEnumerable<Rental> Rental { get; set; }
        public IEnumerable<Inventory> Inventory { get; set; }
    }
}