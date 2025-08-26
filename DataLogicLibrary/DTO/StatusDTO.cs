using DataLogicLibrary.Infrastructure.Enums;

namespace DataLogicLibrary.DTO
{
    public class StatusDTO
    {
        public CardinalDirection CardinalDirection { get; set; }
        public MovementAction MovementAction { get; set; }
        public int GasValue { get; set; }
        public int EnergyValue { get; set; }

    }
}
