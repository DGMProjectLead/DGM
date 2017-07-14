using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGM_Checkout_dev.Controllers
{
    public class latefees
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var user = from r in _context.User
                         .Include(r => r.Rental)
                         select r;

            return View(await user.AsNoTracking().ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //linked the rental to inventory with .include
            var users = await _context.User
                .Include(r => r.Rental)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.RentalID == id);
            users = users.where(i.rentalreturndate > i.rentalreturndate || i.rentallatefeepaid == false);

            return View(users);
        }

    }
}
