using VehicleTransponder.Contracts;

namespace VehicleTransponder.Services
{
    public interface ITransponderService
    {
        Transponder Create(Vehicle vehicle);
        void OnVehicleCreated(object sender, VehicleEventArgs vehicleEventArgs);
    }
}
