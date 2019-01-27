using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using car_dealership.Models;
using car_dealership.Repo.Manufacturer_Repo;
using Microsoft.AspNetCore.Mvc;

namespace car_dealership.Controllers
{
    public class ManufacturerController : Controller
    {
        private CarDealershipContext db;

        public ManufacturerController (CarDealershipContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            ManufacturerRepo mRepo = new ManufacturerRepo(db);
            List<Manufacturer> mList = mRepo.GetAll();
            return View(mList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Manufacturer m)
        {
            bool success = false;
            if(ModelState.IsValid)
            {
                success = new ManufacturerRepo(db).Create(m);
            }
            if(success)
            {
                return RedirectToAction("Index");
            } else
            {
                return RedirectToAction("Error", "Index");
            }
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            ManufacturerRepo mRepo = new ManufacturerRepo(db);
            Manufacturer make = mRepo.Get(id);
            return View(make);
        }

        [HttpPost]
        public IActionResult Edit(Manufacturer m)
        {
            bool success = false;
            if(ModelState.IsValid)
            {
                success = new ManufacturerRepo(db).Update(m);
            }
            if (success)
            {
                return RedirectToAction("Index");
            } else
            {
                return RedirectToAction("Error", "Index");
            }
        }

        public IActionResult Details(string id)
        {
            ManufacturerRepo mRepo = new ManufacturerRepo(db);
            var make = mRepo.Get(id);
            return View(make);
        }

        
        public IActionResult Delete(string id)
        {
            bool success;
            success = new ManufacturerRepo(db).Delete(id);
            if(success)
            {
                return RedirectToAction("Index");
            } else
            {
                return RedirectToAction("Error", "Index");
            }
        }

    }
}