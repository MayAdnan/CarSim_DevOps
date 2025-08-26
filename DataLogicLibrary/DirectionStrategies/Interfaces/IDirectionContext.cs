using DataLogicLibrary.DTO;

namespace DataLogicLibrary.DirectionStrategies.Interfaces
{
    public interface IDirectionContext
    {
        void SetStrategy(IDirectionStrategy strategy);

        StatusDTO ExecuteStrategy(StatusDTO currentStatus);

    }
}
