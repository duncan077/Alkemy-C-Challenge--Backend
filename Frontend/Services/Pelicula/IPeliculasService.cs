using Frontend.Services.Base;

namespace Frontend.Services
{
    public interface IPeliculasService
    {
        Task<Response<PeliculaModelDTO>> AddPelicula(PeliculaModelDTO peliculaModelDTO);
        Task<Response<PeliculaModelDTO>> DeletePelicula(int id);
        Task<Response<PeliculaFullDTO>> GetPelicula(int id);
        Task<Response<List<PeliculaSimpleDTO>>> GetPeliculas(string name, int genre, string order);
        Task<Response<PeliculaModelDTO>> UpdatePelicula(PeliculaModelDTO peliculaModelDTO, int id);
    }
}