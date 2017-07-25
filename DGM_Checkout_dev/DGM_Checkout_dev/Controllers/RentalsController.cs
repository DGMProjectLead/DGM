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
using DGM_Checkout_dev.Models.ViewModels;

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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
                .Include(r => r.Inventory)
                .AsNoTracking()
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
        /// <summary>
        /// Edited the Create method to prevent overposting to RentalID
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalName,RentalCheckoutDate,RentalDueDate,RentalNotes,RentalLocation,UserID")] Rental rental)
        {

            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddItems/" + rental.RentalID);  //"Edit/" + rental.RentalID);
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

            var rental = await _context.Rental
                .Include(r => r.User)
                .SingleOrDefaultAsync(m => m.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserFullInfo", rental.UserID);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        /// <summary>
        /// EditPost now only updates the fields listed in the TryUpdateModelAsync statement
        /// Prevents overposting and changing values of any attribute not listed in the rentalUpdate list
        /// see https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/crud for details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }
            var rentalUpdate = await _context.Rental.SingleOrDefaultAsync(r => r.RentalID == id);

            if(await TryUpdateModelAsync<Rental>( rentalUpdate, "", r => r.RentalReturnDate, r => r.RentalNotes, r => r.RentalLateFee, r => r.RentalLateFeePaid, r => r.RentalLocation ))
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

        public async Task<IActionResult> Search(string nameSearch, string userSearch, string locationSearch, string checkoutSearch, string dueSearch, string returnSearch, bool feePaid, bool feeDue)
        {
            ViewData["nameSearch"] = nameSearch;
            ViewData["userSearch"] = userSearch;
            ViewData["locationSearch"] = locationSearch;
            ViewData["checkoutSearch"] = checkoutSearch;
            ViewData["dueSearch"] = dueSearch;
            ViewData["returnSearch"] = returnSearch;

            var rental = from r in _context.Rental
                         .Include(r => r.User)
                         select r;
            if (!String.IsNullOrEmpty(nameSearch))
            {
                rental = rental.Where(r => r.RentalName.Contains(nameSearch));
            }
            if (!String.IsNullOrEmpty(userSearch))
            {
                rental = rental.Where(r => r.User.UVID.Contains(userSearch));
            }
            if (!String.IsNullOrEmpty(locationSearch))
            {
                rental = rental.Where(r => r.RentalLocation.Contains(locationSearch));
            }
            if (!String.IsNullOrEmpty(checkoutSearch))
            {
                DateTime checkoutConvert = Convert.ToDateTime(checkoutSearch);
                rental = rental.Where(r => r.RentalCheckoutDate == checkoutConvert);
            }
            if (!String.IsNullOrEmpty(dueSearch))
            {
                DateTime dueConvert = Convert.ToDateTime(dueSearch);
                rental = rental.Where(r => r.RentalDueDate == dueConvert);
            }
            if (!String.IsNullOrEmpty(returnSearch))
            {
                DateTime returnConvert = Convert.ToDateTime(returnSearch);
                rental = rental.Where(r => r.RentalReturnDate == returnConvert);
            }
            if (feePaid == true)
            {
                rental = rental.Where(r => r.RentalLateFeePaid == true);
            }
            if (feeDue == true)
            {
                rental = rental.Where(r => r.RentalLateFee == true);
            }

            return View(await rental.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> AddItems(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.User)
                .Include(r => r.Inventory)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.RentalID == id);
            
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }
        /// <summary>
        /// Report on latefee
        /// </summary>
        /// <returns></returns>

        public async Task<IActionResult> LateFeeNotPaid()
        {
            //pull all of the rentals
            var rental = from r in _context.Rental
                         .Include(r => r.User)
                         select r;
            //query only the rentals that have are late and do not have a late fee that is payed
            rental = rental.Where(r => r.RentalReturnDate > r.RentalDueDate && r.RentalLateFeePaid == false || r.RentalDueDate < DateTime.Today && r.RentalReturnDate == null && r.RentalLateFeePaid == false);
            return View(rental);
        }


        /// <summary>
        /// Report on Rentals that have not been returned yet
        /// </summary>
        public async Task<IActionResult> RentalsOut()
        {
            //pull all of the rentals into rental 
            var rental = from r in _context.Rental
                         .Include(r => r.User)
                         select r;

            //query only the rentals that are still not returned.
            rental = rental.Where(r => r.RentalReturnDate == null);
            return View(rental);

        }


        /// <summary>
        /// Report on Rentals organized by user
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RentalUser()
        {
            //pull all of the rentals into rental and organize them by user
            var rental = from r in _context.Rental
                         .Include(r => r.User)
                         orderby r.User
                         select r;

            return View(rental);
        }

        public async Task<IActionResult> Test(int? id, bool selectedCourse)
        {
            var viewModel = new RentalInventoryData();

            viewModel.Inventory = from i in _context.Inventory
                                  .Include(i => i.Rental)
                                  .Include(i => i.Location)
                                  where i.RentalID == null
                                  select i;

            var rental = await _context.Rental
                .Include(r => r.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.RentalID == id);

            if (id == null)
            {
                return NotFound();
            }

            ViewData["RentalName"] = rental.RentalName;
            ViewData["RentalReturn"] = rental.RentalReturnDate;
            ViewData["RentalCheckout"] = rental.RentalCheckoutDate;
            ViewData["RentalNotes"] = rental.RentalNotes;
            var rentalID = rental.RentalID;

            return View(viewModel);
        }

        [HttpPost, ActionName("Test")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestPost(int? id, bool selectedCourse)
        {
            if(id == null)
            {
                return NotFound();
            }
            var viewModel = new RentalInventoryData();

            viewModel.Inventory = from i in _context.Inventory
                                  .Include(i => i.Rental)
                                  .Include(i => i.Location)
                                  where i.RentalID == null
                                  select i;

            foreach (var x in viewModel.Inventory)
            {
                if (selectedCourse == true)
                {
                    _context.Database.ExecuteSqlCommand("UPDATE Inventory SET RentalID={0} WHERE InventoryID={1}", id, x.InventoryID);
                }
            }
            return RedirectToAction("Details/" + id);
        }



    }
}
