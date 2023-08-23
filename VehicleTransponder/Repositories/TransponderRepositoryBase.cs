using VehicleTransponder.Contracts;

namespace VehicleTransponder.Repositories
{
    public abstract class TransponderRepositoryBase : ITransponderRepository
    {
        public virtual Transponder Create(Vehicle vehicle)
        {
            var rand = new Random();

            return new Transponder()
            {
                VehicleId = vehicle.Id,
                Id = rand.NextInt64()
            };
        }

        public virtual void Save(Transponder transponder)
        {
            // dummy method for saving the transponder. could be a database request
        }
    }
}
