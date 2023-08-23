using VehicleTransponder.Contracts;

namespace VehicleTransponder.Repositories
{
    public class DummyVehicleRepository : IVehicleRepository
    {
        public DummyVehicleRepository() { }

        public Vehicle Create(Vehicle vehicle)
        {
            var rand = new Random();
            vehicle.Id = rand.NextInt64();

            return vehicle;
        }
    }
}
