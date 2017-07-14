using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGM_Checkout_dev.Controllers
{
    public class userrent
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string userSearch)
        {
            ViewData["userSearch"] = userSearch;
            
            var users = from u in _context.User
                        .Include(r => r.rental)
                        .AsNoTracking()
                        .singleOrDefaultAsync(r => r.RentalID == id)
                        select u;
            users = users.Where(u => u.UVID.Contains(userSearch));
            return View(await users.AsNoTracking().ToListAsync());
        }
}
