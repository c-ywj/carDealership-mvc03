using car_dealership.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace car_dealership.Repo.Model_Repo
{
    public class ModelRepo
    {
        private CarDealershipContext db;

        public ModelRepo (CarDealershipContext db)
        {
            this.db = db;
        }

        public bool Create(Model m)
        {
            Model model = db.Model
                .Where(mod => mod.ModelName == m.ModelName && mod.ModelYear == m.ModelYear)
                .FirstOrDefault();
            if (model == null)
            {
                model = new Model
                {
                    ModelName = m.ModelName,
                    ModelYear = m.ModelYear,
                    Horsepower = m.Horsepower,
                    Torque = m.Torque,
                    Category = m.Category,
                    Price = m.Price,
                    ManufacturerMake = m.ManufacturerMake
                };
                db.Model.Add(model);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Model Get(string id, int id2)
        {
            var model = db.Model
                .Where(m => m.ModelName == id && m.ModelYear == id2)
                .FirstOrDefault();
            return model;
        }

        public bool Update(Model m)
        {
            Model model = db.Model
                .Where(mod => mod.ModelName == m.ModelName && mod.ModelYear == m.ModelYear)
                .FirstOrDefault();

            model.ModelName = m.ModelName;
            model.ModelYear = m.ModelYear;
            model.Horsepower = m.Horsepower;
            model.Torque = m.Torque;
            model.Category = m.Category;
            model.Price = m.Price;
            model.ManufacturerMake = m.ManufacturerMake;

            db.SaveChanges();
            return true;
        }

        public bool Delete(string id, int id2)
        {
            Model model = db.Model
                .Where(m => m.ModelName == id && m.ModelYear == id2)
                .FirstOrDefault();
            db.Model.Remove(model);
            db.SaveChanges();
            return true;
        }
    }
}
