using VehicleTransponder.Contracts;

namespace VehicleTransponder.Services
{
    public interface IVehicleService
    {
        event EventHandler<VehicleEventArgs> VehicleCreated;
        Vehicle Create(Vehicle vehicle);
    }
}
