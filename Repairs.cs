using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGM_Checkout_dev.Controllers
{
    public class Repairs
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var repair = from r in _context.Inventory
                         .Include(r => r.Status)
                       select r;

            return View(await repair.AsNoTracking().ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //linked the rental to inventory with .include
            var repair = await _context.Inventory
                .Include(r => r.Status)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.InventoryID == id);
            repair = repair.where(i.rentalreturndate > i.rentalreturndate || i.rentallatefeepaid == false);

            return View(repair);
        }
    }
}
