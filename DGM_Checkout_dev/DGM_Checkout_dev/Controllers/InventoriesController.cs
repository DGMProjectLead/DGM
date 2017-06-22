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
    public class InventoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Inventory.Include(i => i.Location).Include(i => i.Rental).Include(i => i.Status).Include(i => i.Type);
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .Include(i => i.Location)
                .Include(i => i.Rental)
                .Include(i => i.Status)
                .Include(i => i.Type)
                .SingleOrDefaultAsync(m => m.InventoryID == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationID", "LocationEntry");
            ViewData["RentalID"] = new SelectList(_context.Rental, "RentalID", "RentalName");
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusEntry");
            ViewData["TypeID"] = new SelectList(_context.Type, "TypeID", "TypeEntry");
            return View();
        }

        // POST: Inventories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventorySerialNumber,InventoryName,InventoryMake,InventoryModel,InventoryNotes,InventoryCost,TypeID,LocationID,StatusID")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationID", "LocationEntry", inventory.LocationID);
            //ViewData["RentalID"] = new SelectList(_context.Rental, "RentalID", "RentalName", inventory.RentalID);
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusEntry", inventory.StatusID);
            ViewData["TypeID"] = new SelectList(_context.Type, "TypeID", "TypeEntry", inventory.TypeID);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .Include(i => i.Location)
                .Include(i => i.Status)
                .Include(i => i.Rental)
                .Include(i => i.Type)
                .SingleOrDefaultAsync(m => m.InventoryID == id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationID", "LocationEntry", inventory.LocationID);
            ViewData["RentalID"] = new SelectList(_context.Rental, "RentalID", "RentalName", inventory.RentalID);
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusEntry", inventory.StatusID);
            ViewData["TypeID"] = new SelectList(_context.Type, "TypeID", "TypeEntry", inventory.TypeID);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }
            var inventoryUpdate = await _context.Inventory.SingleOrDefaultAsync( i => i.InventoryID == id);

            if(await TryUpdateModelAsync<Inventory>( inventoryUpdate, "", i => i.InventoryNotes, i => i.LocationID, i => i.StatusID, i => i.RentalID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again and if problems continue contact IT support.");
                }
            }
            
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationID", "LocationEntry", inventoryUpdate.LocationID);
            ViewData["RentalID"] = new SelectList(_context.Rental, "RentalID", "RentalName", inventoryUpdate.RentalID);
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusEntry", inventoryUpdate.StatusID);
            ViewData["TypeID"] = new SelectList(_context.Type, "TypeID", "TypeEntry", inventoryUpdate.TypeID);
            return View(inventoryUpdate);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .Include(i => i.Location)
                .Include(i => i.Rental)
                .Include(i => i.Status)
                .Include(i => i.Type)
                .SingleOrDefaultAsync(m => m.InventoryID == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventory.SingleOrDefaultAsync(m => m.InventoryID == id);
            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.InventoryID == id);
        }
    }
}
