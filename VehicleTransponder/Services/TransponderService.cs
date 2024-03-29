﻿using VehicleTransponder.Contracts;
using VehicleTransponder.Repositories;
using VehicleTransponder.Utilities;

namespace VehicleTransponder.Services
{
    public class TransponderService : ITransponderService
    {
        private ITransponderRepositoryFactory _repositoryFactory;
        private ILogger<TransponderService> _logger;

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

        public TransponderService(ITransponderRepositoryFactory transponderRepositoryFactory, ILogger<TransponderService> logger) 
        { 
            _logger = logger;
            _repositoryFactory = transponderRepositoryFactory;
        }

        public Transponder Create(Vehicle vehicle)
        {
            ITransponderRepository transponderRepository = GetRepository(vehicle);
            return transponderRepository.Create(vehicle);
        }

        public void OnVehicleCreated(object sender, VehicleEventArgs vehicleEventArgs)
        {
            var transponder = Create(vehicleEventArgs.Vehicle);
            _logger.LogInformation($"Transponder created with vehicleId {transponder.VehicleId}");
        }

        private ITransponderRepository GetRepository(Vehicle vehicle)
        {
            int vehicleYear = ParsingUtility.GetIntFromString(vehicle.Year);
            ITransponderRepository transponderRepo = this.TransponderRepositoryFactory.GetTransponderRepository(vehicleYear);

            return transponderRepo;
        }
    }
}
