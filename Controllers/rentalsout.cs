using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGM_Checkout_dev.Controllers
{
    public class rentalsout
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var rental = from r in _context.Rental
                         .Include(r => r.User)
                       select r;

            return View(await rental.AsNoTracking().ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //linked the rental to inventory with .include
            var rental = await _context.Rental
                .Include(r => r.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.RentalID == id);
            rental = rental.where(r.rentalduedate >= datetime.now.date || r.rentalreturndate == false);

            return View(rental);
        }
    }
}
