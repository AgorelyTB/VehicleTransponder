using VehicleTransponder.Contracts;

namespace VehicleTransponder.Repositories
{
    public interface ITransponderRepository
    {
        Transponder Create(Vehicle vehicle);
        void Save(Transponder transponder);
    }
}
