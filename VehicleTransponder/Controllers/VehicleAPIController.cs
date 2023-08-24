using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using VehicleTransponder.Contracts;
using VehicleTransponder.Repositories;
using VehicleTransponder.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleTransponder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAPIController : ControllerBase
    {
        private IVehicleService _vehicleService;
        private ITransponderService _transponderService;
        private ILogger _logger;

        private IVehicleService VehicleService
        {
            get
            {
                if (_vehicleService == null)
                {
                    throw new InvalidOperationException("VehicleService has not been initialized.");
                    // could also provide default implementation
                }

                return _vehicleService;
            }
        }

        private ITransponderService TransponderService
        {
            get
            {
                if (_transponderService == null)
                {
                    throw new InvalidOperationException("TransponderService has not been initialized.");
                    // could also provide default implementation
                }

                return _transponderService;
            }
        }

        public VehicleAPIController(IVehicleService vehicleService, ITransponderService transponderService, ILogger<VehicleAPIController> logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _vehicleService = vehicleService;
            _transponderService = transponderService;
            _logger = logger;
            try
            {
                InitializeServices();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        // POST api/<VehicleAPIController>
        [HttpPost]
        public void Create([FromBody] VehicleDto vehicle)
        {
            try
            {
                if (vehicle == null)
                {
                    throw new ArgumentNullException(nameof(vehicle));
                }
                var vehicleToSave = new Vehicle()
                {
                    Make = vehicle.Make,
                    Model = vehicle.Model,
                    Year = vehicle.Year
                };

                var createdVehicle = this.VehicleService.Create(vehicleToSave);
                _logger.LogInformation($"Vehicle created. \nId: {createdVehicle.Id} \nMake: {createdVehicle.Make} \nModel: {createdVehicle.Model} \nYear: {createdVehicle.Year}");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        private void InitializeServices()
        {
            this.VehicleService.VehicleCreated += this.TransponderService.OnVehicleCreated;
        }
    }
}
