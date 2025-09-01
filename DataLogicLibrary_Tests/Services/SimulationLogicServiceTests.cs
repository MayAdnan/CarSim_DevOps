using DataLogicLibrary.DirectionStrategies;
using DataLogicLibrary.DTO;
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

        [Fact]
        public void ForwardAction_CardinalDirections_Remains_North_When_PreviousMovement_Is_Not_Backward()
        {
            // Arrange
            var currentStatus = new StatusDTO
            {
                CardinalDirection = CardinalDirection.North,
                MovementAction = MovementAction.Left,
                GasValue = 20,
                EnergyValue = 20

            };
            var userInput = 3;
            var expectedDirection = CardinalDirection.North;

            // Act
            var result = _sut.PerformAction(userInput, currentStatus);

            // Assert
            Assert.Equal(expectedDirection, result.CardinalDirection);
        }
        [Fact]
        public void LeftAction_CardinalDirections_ChangesTo_West_When_Previous_CardinalDirection_Was_North_And_Movement_Is_Not_Backward()
        {
            //Arrange
            var currentStatus = new StatusDTO
            {
                CardinalDirection = CardinalDirection.North,
                MovementAction = MovementAction.Left,
                GasValue = 20,
                EnergyValue = 20
            };
            var userInput = 1;
            var expectedDirection = CardinalDirection.West;

            //Act
            var result = _sut.PerformAction(userInput, currentStatus);

            //Assert
            Assert.Equal(expectedDirection, result.CardinalDirection);
        }
        [Fact]
        public void RightAction_CardinalDirections_ChangesTo_East_When_Previous_CardinalDirection_Was_North_And_Movement_Is_Not_Backward()
        {
            //Arrange
            var currentStatus = new StatusDTO
            {
                CardinalDirection = CardinalDirection.North,
                MovementAction = MovementAction.Left,
                GasValue = 20,
                EnergyValue = 20
            };
            var userInput = 2;
            var expectedDirection = CardinalDirection.East;

            //Act
            var result = _sut.PerformAction(userInput, currentStatus);

            //Assert
            Assert.Equal(expectedDirection, result.CardinalDirection);
        }
        [Fact]
        public void ReverseAction_CardinalDirections_ChangesTo_South_When_Previous_CardinalDirection_Was_North_And_Movement_Is_Not_Backward()
        {
            //Arrange
            var currentStatus = new StatusDTO
            {
                CardinalDirection = CardinalDirection.North,
                MovementAction = MovementAction.Left,
                GasValue = 20,
                EnergyValue = 20
            };
            var userInput = 4;
            var expectedDirection = CardinalDirection.South;
            //Act
            var result = _sut.PerformAction(userInput, currentStatus);
            //Assert
            Assert.Equal(expectedDirection, result.CardinalDirection);
        }
        [Fact]
        public void ForwardAction_CardinalDirections_Remains_East_When_PreviousMovement_Is_Not_Backward()
        {
            // Arrange
            var currentStatus = new StatusDTO
            {
                CardinalDirection = CardinalDirection.East,
                MovementAction = MovementAction.Left,
                GasValue = 20,
                EnergyValue = 20

            };
            var userInput = 3;
            var expectedDirection = CardinalDirection.East;

            // Act
            var result = _sut.PerformAction(userInput, currentStatus);

            // Assert
            Assert.Equal(expectedDirection, result.CardinalDirection);
        }
        [Fact]
        public void LeftAction_CardinalDirections_ChangesTo_North_When_Previous_CardinalDirection_Was_Éast_And_Movement_Is_Not_Backward()
        {
            //Arrange
            var currentStatus = new StatusDTO
            {
                CardinalDirection = CardinalDirection.East,
                MovementAction = MovementAction.Left,
                GasValue = 20,
                EnergyValue = 20
            };
            var userInput = 1;
            var expectedDirection = CardinalDirection.North;

            //Act
            var result = _sut.PerformAction(userInput, currentStatus);

            //Assert
            Assert.Equal(expectedDirection, result.CardinalDirection);
        }
        [Fact]
        public void RightAction_CardinalDirections_ChangesTo_South_When_Previous_CardinalDirection_Was_East_And_Movement_Is_Not_Backward()
        {
            //Arrange
            var currentStatus = new StatusDTO
            {
                CardinalDirection = CardinalDirection.East,
                MovementAction = MovementAction.Left,
                GasValue = 20,
                EnergyValue = 20
            };
            var userInput = 2;
            var expectedDirection = CardinalDirection.South;

            //Act
            var result = _sut.PerformAction(userInput, currentStatus);

            //Assert
            Assert.Equal(expectedDirection, result.CardinalDirection);
        }
        [Fact]
        public void ReverseAction_CardinalDirections_ChangesTo_West_When_Previous_CardinalDirection_Was_East_And_Movement_Is_Not_Backward()
        {
            //Arrange
            var currentStatus = new StatusDTO
            {
                CardinalDirection = CardinalDirection.East,
                MovementAction = MovementAction.Left,
                GasValue = 20,
                EnergyValue = 20
            };
            var userInput = 4;
            var expectedDirection = CardinalDirection.West;
            //Act
            var result = _sut.PerformAction(userInput, currentStatus);
            //Assert
            Assert.Equal(expectedDirection, result.CardinalDirection);
        }
        [Theory]
        [InlineData(1, CardinalDirection.South, CardinalDirection.East)]
        [InlineData(2, CardinalDirection.South, CardinalDirection.West)]
        [InlineData(3, CardinalDirection.South, CardinalDirection.South)]
        [InlineData(4, CardinalDirection.South, CardinalDirection.North)]
        [InlineData(1, CardinalDirection.West, CardinalDirection.South)]
        [InlineData(2, CardinalDirection.West, CardinalDirection.North)]
        [InlineData(3, CardinalDirection.West, CardinalDirection.West)]
        [InlineData(4, CardinalDirection.West, CardinalDirection.East)]
        public void South_CardinalDirection_Actions_Should_Return_Expected_Direction(int userInput, CardinalDirection previousDirection, CardinalDirection expectedDirection)
        {
            //Arrange
            var currentStatus = new StatusDTO
            {
                CardinalDirection = previousDirection,
                MovementAction = MovementAction.Left,
                GasValue = 20,
                EnergyValue = 20
            };
            //Act

            var result = _sut.PerformAction(userInput, currentStatus);

            //Assert
            Assert.Equal(expectedDirection, result.CardinalDirection);

        }     
    }
}
