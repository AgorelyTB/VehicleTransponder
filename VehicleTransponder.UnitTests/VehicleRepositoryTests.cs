using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTransponder.Contracts;
using VehicleTransponder.Repositories;

namespace VehicleTransponder.UnitTests
{
    [TestClass]
    public class VehicleRepositoryTests
    {
        [TestMethod]
        public void VehicleRepository_Create()
        {
            IVehicleRepository vehicleRepository = new DummyVehicleRepository();
            var vehicleToCreate = new Vehicle()
            {
                Id = 0,
                Make = "Dodge",
                Model = "Caravan",
                Year = "2005"
            };

            vehicleRepository.Create(vehicleToCreate);
            Assert.IsTrue(vehicleToCreate.Id > 0);
        }
    }
}
