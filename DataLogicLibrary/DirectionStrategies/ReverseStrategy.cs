using DataLogicLibrary.DirectionStrategies.Interfaces;
using DataLogicLibrary.DTO;
using DataLogicLibrary.Infrastructure.Enums;

namespace DataLogicLibrary.DirectionStrategies
{
    public class ReverseStrategy : IDirectionStrategy
    {
        public StatusDTO Execute(StatusDTO currentStatus)
        {
            CardinalDirection newDirection;

            if (currentStatus.MovementAction != MovementAction.Backward)
            {
                switch (currentStatus.CardinalDirection)
                {
                    case CardinalDirection.North:
                        newDirection = CardinalDirection.South;
                        break;
                    case CardinalDirection.South:
                        newDirection = CardinalDirection.North;
                        break;
                    case CardinalDirection.East:
                        newDirection = CardinalDirection.West;
                        break;
                    case CardinalDirection.West:
                        newDirection = CardinalDirection.East;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(currentStatus.CardinalDirection), "Error: currentCardinalDirection not set");
                }
            }

            else
            {
                newDirection = currentStatus.CardinalDirection;
            }

            currentStatus.CardinalDirection = newDirection;
            currentStatus.MovementAction = MovementAction.Backward;

            return currentStatus;
        }

    }
}
