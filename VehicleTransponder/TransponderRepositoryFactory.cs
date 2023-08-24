using VehicleTransponder.Repositories;

namespace VehicleTransponder
{
    public class TransponderRepositoryFactory : ITransponderRepositoryFactory
    {
        private const int YEAR_CUTOFF = 25;

        public ITransponderRepository GetTransponderRepository(int year)
        {
            int currentYear = DateTimeOffset.Now.Year;
            if (currentYear - year < YEAR_CUTOFF)
            {
                return new ModernTransponderRepository();
            }

            return new ClassicTransponderRepository();
        }
    }
}
