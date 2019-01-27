using car_dealership.Controllers;
using car_dealership.Models;
using car_dealership.Repo.Manufacturer_Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace passion_project_xunit
{
    public class ControllerTests
    {
        private CarDealershipContext db;

        public ControllerTests()
        {
            this.db = new CarDealershipContext();
        }

        [Fact]
        public void firstTest()
        {
            var manufacturerRepo = new ManufacturerRepo(db);
            var controller = new ManufacturerController(db);
            int expected = 3;
            var viewResult = Assert.IsType<ViewResult>(controller.Index());
            var model = Assert.IsType<List<Manufacturer>>(viewResult.Model);
            int actual = model.Count;
            Assert.Equal(expected, actual);
        }

    }
}
