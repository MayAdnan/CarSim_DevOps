using DataLogicLibrary.DTO;

namespace DataLogicLibrary.DirectionStrategies.Interfaces
{
    public interface IDirectionStrategy
    {
        StatusDTO Execute(StatusDTO currentStatus);
    }
}
