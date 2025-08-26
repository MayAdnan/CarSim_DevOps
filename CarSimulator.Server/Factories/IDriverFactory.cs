using APIServiceLibrary.DTO;
using CarSimulator.Server.Models;

namespace CarSimulator.Server.Factories
{
    public interface IDriverFactory
    {
        Driver CreateDriver(ResultsDTO resultDTO);
    }
}
