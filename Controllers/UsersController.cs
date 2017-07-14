using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
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
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public UsersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        /// <summary>
        ///Added search functions to Index method
        /// Search can be moved to it's own Search method or partial view to eliminate clutter from Index
        /// </summary>
        /// <param name="nameSearch"></param>
        /// <param name="uvidSearch"></param>
        /// <param name="emailSearch"></param>
        /// <param name="phoneSearch"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string nameSearch, string uvidSearch, string emailSearch, string phoneSearch)
        {
            ViewData["nameSearch"] = nameSearch;
            ViewData["uvidSearch"] = uvidSearch;
            ViewData["emailSearch"] = emailSearch;
            ViewData["phoneSearch"] = phoneSearch;

            var users = from u in _context.User
                        select u;

            if(!String.IsNullOrEmpty(nameSearch))
            {

                users = users.Where(u => u.UserFirstName.Contains(nameSearch) ||  u.UserLastName.Contains(nameSearch));
            }
            if(!String.IsNullOrEmpty(uvidSearch))
            {
                users = users.Where(u => u.UVID.Contains(uvidSearch));
            }
            if(!String.IsNullOrEmpty(emailSearch))
            {
                users = users.Where(u => u.UserEmail.Contains(emailSearch));
            }
            if(!String.IsNullOrEmpty(phoneSearch))
            {
                users = users.Where(u => u.UserPhone.Contains(phoneSearch));
            }
            return View(await users.AsNoTracking().ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Rentals)
                .SingleOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create     
        /// <summary>
        /// Edited the Create method to prevent overposting to UserID
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UVID,UserFirstName,UserLastName,UserPhone,UserEmail,UserNotes")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
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
            var userToUpdate = await _context.User.SingleOrDefaultAsync(u => u.UserID == id);

            if (await TryUpdateModelAsync<User>(userToUpdate, "", u => u.UVID, u => u.UserFirstName, u => u.UserLastName, u => u.UserPhone, u => u.UserEmail, u => u.UserNotes)) 
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* */)
                {
                    ModelState.AddModelError("", "Unable to save changes.  Try again and if problems continue call IT support.");
                }
            }
            return View(userToUpdate);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .SingleOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.UserID == id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserID == id);
        }

        [HttpPost]
        public JsonResult UVIDExists (string UVIDNumber)
        {
            return Json(!_context.User.Any(u => u.UVID == UVIDNumber));
        }
    }
}
