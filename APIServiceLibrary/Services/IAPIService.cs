using APIServiceLibrary.DTO;

namespace APIServiceLibrary.Services
{
    public interface IAPIService
    {
        Task<ResultsDTO> GetOneDriver();
    }
}
