using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using car_dealership.Models;
using car_dealership.Repo.Model_Repo;

namespace car_dealership.Controllers
{
    public class ModelController : Controller
    {
        private readonly CarDealershipContext db;

        public ModelController(CarDealershipContext db)
        {
            this.db = db;
        }

        // GET: Model
        public async Task<IActionResult> Index()
        {
            var carDealershipContext = db.Model.Include(m => m.ManufacturerMakeNavigation);
            return View(await carDealershipContext.ToListAsync());
        }

        public IActionResult Details(string id, int id2)
        {
            ModelRepo modRepo = new ModelRepo(db);
            var model = modRepo.Get(id, id2);
            return View(model);
        }

        // GET: Model/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerMake"] = new SelectList(db.Manufacturer, "Make", "Make");
            return View();
        }

        // POST: Model/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelName,ModelYear,Horsepower,Torque,Category,Price,ManufacturerMake")] Model model)
        {
            if (ModelState.IsValid)
            {
                db.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerMake"] = new SelectList(db.Manufacturer, "Make", "Make", model.ManufacturerMake);
            return View(model);
        }

        // GET: Model/Edit/5
        public async Task<IActionResult> Edit(string id, int id2)
        {
            ViewData["ManufacturerMake"] = new SelectList(db.Manufacturer, "Make", "Make");
            ModelRepo modRepo = new ModelRepo(db);
            Model model = modRepo.Get(id, id2);
            return View(model);
        }

        // POST: Model/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Model mod)
        {
            bool success = false;
            if (ModelState.IsValid)
            {
                success = new ModelRepo(db).Update(mod);
            }
            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error", "Index");
            }
        }

        // GET: Model/Delete/5
        public async Task<IActionResult> Delete(string id, int id2)
        {
            if (id == null && id2 == null)
            {
                return NotFound();
            }

            var model = await db.Model
                .Include(m => m.ManufacturerMakeNavigation)
                .FirstOrDefaultAsync(m => m.ModelName == id && m.ModelYear == id2);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Model/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, int id2)
        {
            bool success;
            success = new ModelRepo(db).Delete(id, id2);
            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error", "Index");
            }
        }

        private bool ModelExists(string id)
        {
            return db.Model.Any(e => e.ModelName == id);
        }
    }
}
