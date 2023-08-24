using VehicleTransponder.Contracts;
using VehicleTransponder.Repositories;
using VehicleTransponder.Utilities;

namespace VehicleTransponder.Services
{
    public class TransponderService : ITransponderService
    {
        private ITransponderRepositoryFactory _repositoryFactory;

        private ITransponderRepositoryFactory TransponderRepositoryFactory
        {
            get
            {
                if (_repositoryFactory == null)
                {
                    // alternatively we could supply a default implementation here
                    throw new InvalidOperationException("TransponderRepositoryFactory has not been initialized");
                }

                return _repositoryFactory;
            }
        }

        public TransponderService(ITransponderRepositoryFactory transponderRepositoryFactory) 
        { 
            _repositoryFactory = transponderRepositoryFactory;
        }

        public Transponder Create(Vehicle vehicle)
        {
            ITransponderRepository transponderRepository = GetRepository(vehicle);
            return transponderRepository.Create(vehicle);
        }

        public void OnVehicleCreated(object sender, VehicleEventArgs vehicleEventArgs)
        {
            Create(vehicleEventArgs.Vehicle);
        }

        private ITransponderRepository GetRepository(Vehicle vehicle)
        {
            int vehicleYear = ParsingUtility.GetIntFromString(vehicle.Year);
            ITransponderRepository transponderRepo = this.TransponderRepositoryFactory.GetTransponderRepository(vehicleYear);

            return transponderRepo;
        }
    }
}
