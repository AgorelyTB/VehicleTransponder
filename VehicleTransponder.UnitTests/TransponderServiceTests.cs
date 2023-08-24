using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTransponder.Contracts;
using VehicleTransponder.Repositories;
using VehicleTransponder.Services;
using VehicleTransponder.UnitTests.TestClasses;

namespace VehicleTransponder.UnitTests
{
    [TestClass]
    public class TransponderServiceTests
    {
        [TestMethod]
        public void CreateTransponderUsingClassicRepository()
        {
            var testLogger = new TestLogger<TransponderService>();
            var vehicle = new Vehicle()
            {
                Id = 99,
                Make = "Yugo",
                Model = "GVL",
                Year = "1988"
            };

            var factoryMock = new Mock<ITransponderRepositoryFactory>();
            factoryMock.Setup(m => m.GetTransponderRepository(It.IsAny<int>())).Returns(new ClassicTransponderRepository()).Verifiable();

            ITransponderService service = new TransponderService(factoryMock.Object, testLogger);
            Transponder transponder = service.Create(vehicle);
            factoryMock.Verify(m => m.GetTransponderRepository(It.IsAny<int>()));
            Assert.IsTrue(transponder.VehicleId ==  vehicle.Id);
            Assert.IsTrue(transponder.Id > 0);
        }

        [TestMethod]
        public void CreateTransponderUsingModernRepository()
        {
            var testLogger = new TestLogger<TransponderService>();
            var vehicle = new Vehicle()
            {
                Id = 99,
                Make = "Ford",
                Model = "F350",
                Year = "2019"
            };

            var factoryMock = new Mock<ITransponderRepositoryFactory>();
            factoryMock.Setup(m => m.GetTransponderRepository(It.IsAny<int>())).Returns(new ModernTransponderRepository()).Verifiable();

            ITransponderService service = new TransponderService(factoryMock.Object, testLogger);
            Transponder transponder = service.Create(vehicle);
            factoryMock.Verify(m => m.GetTransponderRepository(It.IsAny<int>()));
            Assert.IsTrue(transponder.VehicleId == vehicle.Id);
            Assert.IsTrue(transponder.Id > 0);
        }
    }
}
