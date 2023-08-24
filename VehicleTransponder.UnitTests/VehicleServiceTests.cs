using Moq;
using VehicleTransponder.Contracts;
using VehicleTransponder.Repositories;
using VehicleTransponder.Services;

namespace VehicleTransponder.UnitTests
{
    [TestClass]
    public class VehicleServiceTests
    {
        [TestMethod]
        public void CreateVehicle_EnsureEventRaised()
        {
            var vehicleRepositoryMock = new Mock<IVehicleRepository>();
            var createdVehicle = new Vehicle()
            {
                Make = "Honda",
                Model = "Accord",
                Year = "2016"
            };

            vehicleRepositoryMock.Setup(r => r.Create(It.IsAny<Vehicle>())).Returns(It.IsAny<Vehicle>()).Verifiable();
            bool eventRaised = false;
            IVehicleService vehicleService = new VehicleService(vehicleRepositoryMock.Object);
            vehicleService.VehicleCreated += delegate (object? sender, VehicleEventArgs e)
            {
                eventRaised = true;
            };

            vehicleService.Create(createdVehicle);
            Assert.IsTrue(eventRaised);
            vehicleRepositoryMock.Verify();

        }

        [TestMethod]
        public void VehicleService_EnsureTransponderIsCreated()
        {
            var vehicleRepositoryMock = new Mock<IVehicleRepository>();
            var transponderRepositoryFactoryMock = new Mock<ITransponderRepositoryFactory>();
            var transponderRepositoryMock = new Mock<ITransponderRepository>();
            
            var createdVehicle = new Vehicle()
            {
                Make = "Honda",
                Model = "Accord",
                Year = "2016",
                Id = 100
            };

            var expectedTransponder = new Transponder()
            {
                VehicleId = createdVehicle.Id,
                Id = 99
            };


            vehicleRepositoryMock.Setup(r => r.Create(It.IsAny<Vehicle>())).Returns(It.IsAny<Vehicle>()).Verifiable();
            transponderRepositoryMock.Setup(r => r.Create(It.IsAny<Vehicle>())).Returns(expectedTransponder).Verifiable();
            transponderRepositoryFactoryMock.Setup(f => f.GetTransponderRepository(It.IsAny<int>())).Returns(transponderRepositoryMock.Object);

            IVehicleService vehicleService = new VehicleService(vehicleRepositoryMock.Object);
            ITransponderService transponderService = new TransponderService(transponderRepositoryFactoryMock.Object);
            vehicleService.VehicleCreated += transponderService.OnVehicleCreated;

            vehicleService.Create(createdVehicle);
            transponderRepositoryMock.Verify();
            vehicleRepositoryMock.Verify();
        }
    }
}