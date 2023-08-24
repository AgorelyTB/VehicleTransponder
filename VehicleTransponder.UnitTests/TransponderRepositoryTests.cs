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
    public class TransponderRepositoryTests
    {
        [TestMethod]
        public void ClassicTransponderRepository_Create()
        {
            long expectedVehicleId = 1000;
            var repository = new ClassicTransponderRepository();
            var hic = new Vehicle();
            hic.Year = "2011";
            hic.Make = "Kia";
            hic.Model = "Optima";
            hic.Id = expectedVehicleId;
            Transponder transponder = repository.Create(hic);

            Assert.IsTrue(transponder.Id > 0);
            Assert.IsTrue(transponder.VehicleId == expectedVehicleId);
        }

        [TestMethod]
        public void ModernTransponderRepository_Create()
        {
            long expectedVehicleId = 99;
            var repository = new ClassicTransponderRepository();
            var hic = new Vehicle();
            hic.Year = "2011";
            hic.Make = "Kia";
            hic.Model = "Optima";
            hic.Id = expectedVehicleId;
            Transponder transponder = repository.Create(hic);

            Assert.IsTrue(transponder.Id > 0);
            Assert.IsTrue(transponder.VehicleId == expectedVehicleId);
        }

        [TestMethod]
        public void TransponderRepositoryFactory_GetClassicRepository()
        {
            Type expectedRepositoryType = typeof(ClassicTransponderRepository);
            ITransponderRepositoryFactory factory = new TransponderRepositoryFactory();
            int vehicleYear = DateTimeOffset.Now.Year - 26;
            ITransponderRepository repository = factory.GetTransponderRepository(vehicleYear);
            Assert.AreEqual(expectedRepositoryType, repository.GetType());

        }

        [TestMethod]
        public void TransponderRepositoryFactory_GetClassicRepository_25YearsOld()
        {
            Type expectedRepositoryType = typeof(ClassicTransponderRepository);
            ITransponderRepositoryFactory factory = new TransponderRepositoryFactory();
            int vehicleYear = DateTimeOffset.Now.Year - 25;
            ITransponderRepository repository = factory.GetTransponderRepository(vehicleYear);
            Assert.AreEqual(expectedRepositoryType, repository.GetType());

        }

        [TestMethod]
        public void TransponderRepositoryFactory_GetModernRepository()
        {
            Type expectedRepositoryType = typeof(ModernTransponderRepository);
            ITransponderRepositoryFactory factory = new TransponderRepositoryFactory();
            int vehicleYear = DateTimeOffset.Now.Year - 24;
            ITransponderRepository repository = factory.GetTransponderRepository(vehicleYear);
            Assert.AreEqual(expectedRepositoryType, repository.GetType());
        }
    }
}
