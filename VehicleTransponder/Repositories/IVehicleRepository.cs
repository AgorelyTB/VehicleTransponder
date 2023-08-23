using VehicleTransponder.Contracts;

namespace VehicleTransponder.Repositories
{
    public interface IVehicleRepository
    {
        Vehicle Create(Vehicle vehicle);
    }
}
