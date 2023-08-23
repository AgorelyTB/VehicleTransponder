using VehicleTransponder.Repositories;

namespace VehicleTransponder
{
    public interface ITransponderRepositoryFactory
    {
        public ITransponderRepository GetTransponderRepository(int year);
    }
}
