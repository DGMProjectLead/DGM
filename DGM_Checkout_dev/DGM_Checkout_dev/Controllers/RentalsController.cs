using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DGM_Checkout_dev.Data;
using DGM_Checkout_dev.Models;
using Microsoft.AspNetCore.Authorization;

namespace DGM_Checkout_dev.Controllers
{
    [Authorize]
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rental.Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.User)
                .SingleOrDefaultAsync(m => m.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserFullInfo");
            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalName,RentalCheckoutDate,RentalDueDate,RentalReturnDate,RentalNotes,RentalLateFee,RentalLateFeePaid,RentalLocation,UserID")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserFullInfo", rental.UserID);
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental.SingleOrDefaultAsync(m => m.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserFullInfo", rental.UserID);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }
            var rentalUpdate = await _context.Rental.SingleOrDefaultAsync(r => r.RentalID == id);

            if(await TryUpdateModelAsync<Rental>( rentalUpdate, "", r => r.RentalName, r => r.RentalCheckoutDate, r => r.RentalDueDate, r => r.RentalReturnDate, r => r.RentalNotes, r => r.RentalLateFee, r => r.RentalLateFeePaid, r => r.RentalLocation, r => r.UserID ))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes.  Try again and if problems continue call IT support.");
                }

            }

            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserFullInfo", rentalUpdate.UserID);
            return View(rentalUpdate);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.User)
                .SingleOrDefaultAsync(m => m.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rental.SingleOrDefaultAsync(m => m.RentalID == id);
            _context.Rental.Remove(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RentalExists(int id)
        {
            return _context.Rental.Any(e => e.RentalID == id);
        }
    }
}
