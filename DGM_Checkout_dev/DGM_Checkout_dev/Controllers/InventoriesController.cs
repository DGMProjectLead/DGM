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
        /// <summary>
        /// Added search functions to Index method
        /// Search can be moved to it's own Search method or partial view to eliminate clutter from Index
        /// </summary>
        /// <param name="serialNumberSearch"></param>
        /// <param name="nameSearch"></param>
        /// <param name="makeSearch"></param>
        /// <param name="modelSearch"></param>
        /// <param name="costSearch"></param>
        /// <param name="locationSearch"></param>
        /// <param name="rentalSearch"></param>
        /// <param name="statusSearch"></param>
        /// <param name="typeSearch"></param>
        /// <param name="costRadio"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string serialNumberSearch, string nameSearch, string makeSearch, string modelSearch, int costSearch, string locationSearch, string rentalSearch, string statusSearch, string typeSearch, string costRadio)
        {
            ViewData["serialNumberSearch"] = serialNumberSearch;
            ViewData["nameSearch"] = nameSearch;
            ViewData["makeSearch"] = makeSearch;
            ViewData["modelSearch"] = modelSearch;
            ViewData["costSearch"] = costSearch;
            ViewData["locationSearch"] = locationSearch;
            ViewData["rentalSearch"] = rentalSearch;
            ViewData["statusSearch"] = statusSearch;
            ViewData["typeSearch"] = typeSearch;

            var inventory = from i in _context.Inventory
                            .Include(i => i.Location)
                            .Include(i => i.Rental)
                            .Include(i => i.Status)
                            .Include(i => i.Type)
                            select i;

            if(!String.IsNullOrEmpty(serialNumberSearch))
            {
                inventory = inventory.Where(i => i.InventorySerialNumber.Contains(serialNumberSearch));
            }
            if (!String.IsNullOrEmpty(nameSearch))
            {
                inventory = inventory.Where(i => i.InventoryName.Contains(nameSearch));
            }
            if (!String.IsNullOrEmpty(makeSearch))
            {
                inventory = inventory.Where(i => i.InventoryMake.Contains(makeSearch));
            }
            if (!String.IsNullOrEmpty(modelSearch))
            {
                inventory = inventory.Where(i => i.InventoryModel.Contains(modelSearch));
            }
            if (!String.IsNullOrEmpty(locationSearch))
            {
                inventory = inventory.Where(i => i.Location.LocationEntry.Contains(locationSearch));
            }
            if (!String.IsNullOrEmpty(rentalSearch))
            {
                inventory = inventory.Where(i => i.Rental.RentalName.Contains(rentalSearch));
            }
            if (!String.IsNullOrEmpty(typeSearch))
            {
                inventory = inventory.Where(i => i.Type.TypeEntry.Contains(typeSearch));
            }
            if(costRadio is "greater")
            {
                inventory = inventory.Where(i => i.InventoryCost > costSearch);
            }
            if(costRadio is "less")
            {
                inventory = inventory.Where(i => i.InventoryCost < costSearch);
            }
            if(costRadio is "equal")
            {
                inventory = inventory.Where(i => i.InventoryCost.Equals(costSearch));
            }     
            return View(await inventory.AsNoTracking().ToListAsync());
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
        /// <summary>
        /// Edited the Create method to prevent overposting to InventoryID
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
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

        public async Task<IActionResult> AddItems()
        {

            var inventory = from i in _context.Inventory
                .Include(i => i.Location)
                .Include(i => i.Rental)
                .Include(i => i.Status)
                .Include(i => i.Type)
                            where i.RentalID == null
                           select i;
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }




        public async Task<IActionResult> Add(int? id)
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
        /// <summary>
        /// EditPost now only updates the fields listed in the TryUpdateModelAsync statement
        /// Prevents overposting and changing values of any attribute not listed in the rentalUpdate list
        /// see https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/crud for details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var inventoryUpdate = await _context.Inventory.SingleOrDefaultAsync(i => i.InventoryID == id);

            if (await TryUpdateModelAsync<Inventory>(inventoryUpdate, "", i => i.RentalID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("AddItems");
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
    }
}
