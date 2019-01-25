using System;
using System.Collections.Generic;

namespace car_dealership.Models
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Model = new HashSet<Model>();
        }

        public string Make { get; set; }
        public string Origin { get; set; }

        public ICollection<Model> Model { get; set; }
    }
}
