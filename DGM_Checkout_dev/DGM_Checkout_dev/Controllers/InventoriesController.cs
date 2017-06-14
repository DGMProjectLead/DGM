using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DGM_Checkout_dev.Data;
using DGM_Checkout_dev.Models;

namespace DGM_Checkout_dev.Controllers
{
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
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationID", "LocationID");
            ViewData["RentalID"] = new SelectList(_context.Rental, "RentalID", "RentalID");
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID");
            ViewData["TypeID"] = new SelectList(_context.Type, "TypeID", "TypeID");
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventoryID,InventorySerialNumber,InventoryName,InventoryMake,InventoryModel,InventoryNotes,InventoryCost,TypeID,LocationID,StatusID,RentalID")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationID", "LocationID", inventory.LocationID);
            ViewData["RentalID"] = new SelectList(_context.Rental, "RentalID", "RentalID", inventory.RentalID);
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID", inventory.StatusID);
            ViewData["TypeID"] = new SelectList(_context.Type, "TypeID", "TypeID", inventory.TypeID);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory.SingleOrDefaultAsync(m => m.InventoryID == id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationID", "LocationID", inventory.LocationID);
            ViewData["RentalID"] = new SelectList(_context.Rental, "RentalID", "RentalID", inventory.RentalID);
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID", inventory.StatusID);
            ViewData["TypeID"] = new SelectList(_context.Type, "TypeID", "TypeID", inventory.TypeID);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventoryID,InventorySerialNumber,InventoryName,InventoryMake,InventoryModel,InventoryNotes,InventoryCost,TypeID,LocationID,StatusID,RentalID")] Inventory inventory)
        {
            if (id != inventory.InventoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.InventoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationID", "LocationID", inventory.LocationID);
            ViewData["RentalID"] = new SelectList(_context.Rental, "RentalID", "RentalID", inventory.RentalID);
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID", inventory.StatusID);
            ViewData["TypeID"] = new SelectList(_context.Type, "TypeID", "TypeID", inventory.TypeID);
            return View(inventory);
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
