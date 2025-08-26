using CarSimulator.Server.Models;

namespace CarSimulator.Server.Factories
{
    public interface ICarFactory
    {
        Car CreateCar();
    }
}
