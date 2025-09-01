using DataLogicLibrary.DirectionStrategies;
using DataLogicLibrary.Infrastructure.Enums;
using DataLogicLibrary.Services;
using static DataLogicLibrary.Services.SimulationLogicService;

namespace DataLogicLibrary_Tests.Services
{
    
    public class SimulationLogicServiceTests
    {
        private readonly SimulationLogicService _sut;
        private readonly DirectionContext _directionContext;
        private readonly DirectionStrategyResolver _directionStrategyResolver;

        public SimulationLogicServiceTests()
        {
            _directionContext = new DirectionContext();
            _directionStrategyResolver = movementAction =>
            {
                switch (movementAction)
                {
                    case MovementAction.Left:
                        return new TurnLeftStrategy();
                    case MovementAction.Right:
                        return new TurnRightStrategy();
                    case MovementAction.Forward:
                        return new DriveForwardStrategy();
                    case MovementAction.Backward:
                        return new ReverseStrategy();
                    default:
                        throw new KeyNotFoundException();
                }
            };
            _sut = new SimulationLogicService(_directionContext, _directionStrategyResolver);

        }

    }
}
