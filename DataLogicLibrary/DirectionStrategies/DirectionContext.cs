using DataLogicLibrary.DirectionStrategies.Interfaces;
using DataLogicLibrary.DTO;

namespace DataLogicLibrary.DirectionStrategies
{
    public class DirectionContext : IDirectionContext
    {
        private IDirectionStrategy _strategy;

        public void SetStrategy(IDirectionStrategy strategy)
        {
            _strategy = strategy;
        }

        public StatusDTO ExecuteStrategy(StatusDTO currentStatus)
        {
            return _strategy.Execute(currentStatus);
        }

    }
}
