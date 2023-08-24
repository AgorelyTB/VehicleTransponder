
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using VehicleTransponder.Contracts;
using VehicleTransponder.Controllers;
using VehicleTransponder.Services;
using VehicleTransponder.UnitTests.TestClasses;

namespace VehicleTransponder.UnitTests
{
    [TestClass]
    public class VehicleAPIControllerTests
    {
        [TestMethod]
        public void ConstructAPIController_NullTransponderService()
        {
            var vehicleServiceMock = new Mock<IVehicleService>();
            var testLogger = new TestLogger<VehicleAPIController>();
            ITransponderService transponderService = null;
            string expectedExceptionMessage = "TransponderService has not been initialized.";

            try
            {
                var apiController = new VehicleAPIController(vehicleServiceMock.Object, transponderService, testLogger);
                Assert.Fail("Exception should be thrown here due to null transponderService object");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.GetType() == typeof(InvalidOperationException));
                Assert.AreEqual(expectedExceptionMessage, e.Message);
            }
        }

        [TestMethod]
        public void ConstructAPIController_NullVehicleService()
        {
            IVehicleService vehicleServiceMock = null;
            var testLogger = new TestLogger<VehicleAPIController>();
            var transponderServiceMock = new Mock<ITransponderService>();
            string expectedExceptionMessage = "VehicleService has not been initialized.";

            try
            {
                var apiController = new VehicleAPIController(vehicleServiceMock, transponderServiceMock.Object, testLogger);
                Assert.Fail("Exception should be thrown here due to null transponderService object");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.GetType() == typeof(InvalidOperationException));
                Assert.AreEqual(expectedExceptionMessage, e.Message);
            }
        }

        [TestMethod]
        public void ConstructAPIController_NullLogger()
        {
            var vehicleServiceMock = new Mock<IVehicleService>();
            ILogger<VehicleAPIController> nullLogger = null;
            var transponderServiceMock = new Mock<ITransponderService>();
            
            try
            {
                var apiController = new VehicleAPIController(vehicleServiceMock.Object, transponderServiceMock.Object, nullLogger);
                Assert.Fail("Exception should be thrown here due to null transponderService object");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.GetType() == typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ConstructAPIController_Success()
        {
            var vehicleServiceMock = new Mock<IVehicleService>();
            var testLogger = new TestLogger<VehicleAPIController>();
            var transponderServiceMock = new Mock<ITransponderService>();

            try
            {
                var apiController = new VehicleAPIController(vehicleServiceMock.Object, transponderServiceMock.Object, testLogger);
            }
            catch (Exception e)
            {
                Assert.Fail("Constructor should succeed");
            }
        }

        [TestMethod]
        public void VehicleAPIController_Create_Success()
        {
            VehicleDto vehicle = new VehicleDto()
            {
                Make = "Toyota",
                Model = "Tundra",
                Year = "1999"
            };

            var expectedVehicleToReturn = new Vehicle()
            {
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Id = 1
            };

            var vehicleServiceMock = new Mock<IVehicleService>();
            var testLogger = new TestLogger<VehicleAPIController>();
            var transponderServiceMock = new Mock<ITransponderService>();

            vehicleServiceMock.Setup(v => v.Create(It.IsAny<Vehicle>())).Returns(expectedVehicleToReturn).Verifiable();

            var apiController = new VehicleAPIController(vehicleServiceMock.Object, transponderServiceMock.Object, testLogger);
            apiController.Create(vehicle);
            vehicleServiceMock.Verify();
        }

        [TestMethod]
        public void VehicleAPIController_Create_Fail_NullVehicle()
        {
            VehicleDto vehicle = null;

            var vehicleServiceMock = new Mock<IVehicleService>();
            var testLogger = new TestLogger<VehicleAPIController>();
            var transponderServiceMock = new Mock<ITransponderService>();

            var apiController = new VehicleAPIController(vehicleServiceMock.Object, transponderServiceMock.Object, testLogger);
            try
            {
                apiController.Create(vehicle);
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(ArgumentNullException), e.GetType());
            }
        }
    }
}
