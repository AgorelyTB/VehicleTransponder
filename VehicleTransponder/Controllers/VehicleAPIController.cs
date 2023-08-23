using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
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
            _vehicleService = vehicleService;
            _transponderService = transponderService;
            _logger = logger;

            InitializeServices();
        }

        // POST api/<VehicleAPIController>
        [HttpPost]
        public void Create([FromBody] VehicleDto vehicle)
        {
            var vehicleToSave = new Vehicle()
            {
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year
            };

            this.VehicleService.Create(vehicleToSave);
        }

        private void InitializeServices()
        {
            this.VehicleService.VehicleCreated += this.TransponderService.OnVehicleCreated;
        }
    }
}
