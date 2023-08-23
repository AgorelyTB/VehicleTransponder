using VehicleTransponder.Contracts;

namespace VehicleTransponder
{
    public class VehicleEventArgs : EventArgs
    {
        public Vehicle Vehicle { get; set; }
    }
}
