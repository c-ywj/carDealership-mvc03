using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace car_dealership.Models
{
    public partial class Inventory
    {
        public int StockId { get; set; }
        [DisplayName("Model Name")]
        public string ModelModelName { get; set; }
        [DisplayName("Model Year")]
        public int? ModelModelYear { get; set; }
        public int? Qty { get; set; }

        public Model ModelModel { get; set; }
    }
}
