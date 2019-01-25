using car_dealership.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace car_dealership.Repo.Manufacturer_Repo
{
    public class ManufacturerRepo
    {
        private CarDealershipContext db;

        public ManufacturerRepo(CarDealershipContext db)
        {
            this.db = db;
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            var mList = db.Manufacturer
                .Select(m => new Manufacturer()
                {
                    Make = m.Make,
                    Origin = m.Origin
                });
            return mList;
        }

        public Manufacturer Get(string id)
        {
            var make = db.Manufacturer
                .Where(m => m.Make == id)
                .FirstOrDefault();
            return make;
        }

        public bool Create(Manufacturer m)
        {
            Manufacturer make = db.Manufacturer
                .Where(manu => manu.Make == m.Make)
                .FirstOrDefault();
            if(make == null)
            {
                make = new Manufacturer
                {
                    Make = m.Make,
                    Origin = m.Origin
                };
                db.Manufacturer.Add(make);
                db.SaveChanges();
                return true;
            } else
            {
                return false;
            }
        }

        public bool Update(Manufacturer m)
        {
            Manufacturer make = db.Manufacturer
                .Where(man => man.Make == m.Make)
                .FirstOrDefault();

            make.Make = m.Make;
            make.Origin = m.Origin;

            db.SaveChanges();
            return true;
        }

        public bool Delete(string id)
        {
            Manufacturer make = db.Manufacturer
                .Where(m => m.Make == id)
                .FirstOrDefault();
            if(make != null)
            {
                db.Manufacturer.Remove(make);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
