using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace car_dealership.Models
{
    public partial class Model
    {
        public Model()
        {
            Inventory = new HashSet<Inventory>();
        }
        [DisplayName("Model")]
        public string ModelName { get; set; }
        [DisplayName("Year")]
        public int ModelYear { get; set; }
        public int? Horsepower { get; set; }
        public int? Torque { get; set; }
        public string Category { get; set; }
        public decimal? Price { get; set; }
        public string ManufacturerMake { get; set; }

        [DisplayName("Make")]
        public Manufacturer ManufacturerMakeNavigation { get; set; }
        public ICollection<Inventory> Inventory { get; set; }
    }
}
