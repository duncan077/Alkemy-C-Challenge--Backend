using Frontend.Services.Base;

namespace Frontend.Services
{
    public interface IGenerosService
    {
        Task<Response<GeneroDTO>> AddGenero(GeneroDTO memeAddDTO);
        Task<Response<GeneroDTO>> GetGenero(int id);
        Task<Response<List<GeneroDTO>>> GetGeneros();
        Task<Response<GeneroDTO>> UpdateGenero(GeneroDTO generoDTO, int id);
    }
}