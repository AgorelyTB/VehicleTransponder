using VehicleTransponder.Contracts;
using VehicleTransponder.Repositories;

namespace VehicleTransponder.Services
{
    public class VehicleService : IVehicleService
    {
        public event EventHandler<VehicleEventArgs> VehicleCreated;

        private IVehicleRepository _vehicleRepository;

        private IVehicleRepository VehicleRepository
        {
            get
            {
                if (_vehicleRepository == null)
                {
                    throw new InvalidOperationException("VehicleRepository has not been initialized.");
                }

                return _vehicleRepository; 
            }
        }
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public Vehicle Create(Vehicle vehicle)
        {
            var vehicleToReturn = this.VehicleRepository.Create(vehicle);
            VehicleEventArgs eventArgs = new VehicleEventArgs();
            eventArgs.Vehicle = vehicle;
            EventHandler<VehicleEventArgs> handler = this.VehicleCreated;
            if (handler != null)
            {
                handler(this, eventArgs);
            }

            return vehicleToReturn;
        }
    }
}
