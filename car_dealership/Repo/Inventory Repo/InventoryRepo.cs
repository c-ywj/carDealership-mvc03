using car_dealership.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace car_dealership.Repo.Inventory_Repo
{
    public class InventoryRepo
    {
        private CarDealershipContext _context;

        public InventoryRepo(CarDealershipContext _context)
        {
            this._context = _context;
        }


        public IEnumerable<Inventory> GetAll(string sortOrder, string searchString)
        {
            IEnumerable<Inventory> inventoryItems;

            if(!String.IsNullOrEmpty(searchString))
            {
                inventoryItems = _context.Inventory.Where(i => i.ModelModelName.Contains(searchString));

            } else
            {
                inventoryItems = _context.Inventory;
            }

            switch (sortOrder)
            {
                case "model_asc":
                    inventoryItems =
                        inventoryItems.OrderBy(i => i.ModelModelName);
                    break;
                case "model_desc":
                    inventoryItems =
                        inventoryItems.OrderByDescending(i => i.ModelModelName);
                    break;
                case "year_desc":
                    inventoryItems =
                  inventoryItems.OrderByDescending(i => i.ModelModelYear);
                    break;
                default:
                    inventoryItems =
                        inventoryItems.OrderBy(i => i.ModelModelYear);
                    break;
            };
            return inventoryItems;


        }
    }
}
