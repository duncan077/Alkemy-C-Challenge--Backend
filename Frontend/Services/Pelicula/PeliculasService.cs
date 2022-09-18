using Blazored.LocalStorage;
using Frontend.Services.Base;

namespace Frontend.Services
{
    public class PeliculasService : BaseHttpService, IPeliculasService
    {
        private readonly IClient client;

        public PeliculasService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.client = client;
        }
        public async Task<Response<List<PeliculaSimpleDTO>>> GetPeliculas(string name, int genre, string order)
        {
            Response<List<PeliculaSimpleDTO>> response;
            try
            {
                await GetBearerToken();
                var data = await client.MoviesAllAsync(name, genre, order);
                response = new Response<List<PeliculaSimpleDTO>>
                {
                    Result = data.ToList(),
                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<List<PeliculaSimpleDTO>>(ex);
            }
            return response;
        }
        public async Task<Response<PeliculaFullDTO>> GetPelicula(int id)
        {
            Response<PeliculaFullDTO> response;
            try
            {
                await GetBearerToken();
                var data = await client.MoviesGETAsync(id);
                response = new Response<PeliculaFullDTO>
                {
                    Result = data,
                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<PeliculaFullDTO>(ex);
            }
            return response;
        }
        public async Task<Response<PeliculaModelDTO>> AddPelicula(PeliculaModelDTO peliculaModelDTO)
        {
            Response<PeliculaModelDTO> response;
            try
            {
                await GetBearerToken();
                var data = await client.MoviesPOSTAsync(peliculaModelDTO);
                response = new Response<PeliculaModelDTO>
                {
                    Result = data,
                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<PeliculaModelDTO>(ex);
            }
            return response;
        }
        public async Task<Response<PeliculaModelDTO>> UpdatePelicula(PeliculaModelDTO peliculaModelDTO, int id)
        {
            Response<PeliculaModelDTO> response;
            try
            {
                await GetBearerToken();
                await client.MoviesPUTAsync(id, peliculaModelDTO);
                response = new Response<PeliculaModelDTO>
                {

                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<PeliculaModelDTO>(ex);
            }
            return response;
        }
        public async Task<Response<PeliculaModelDTO>> DeletePelicula(int id)
        {
            Response<PeliculaModelDTO> response;
            try
            {
                await GetBearerToken();
                await client.MoviesDELETEAsync(id);
                response = new Response<PeliculaModelDTO>
                {

                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<PeliculaModelDTO>(ex);
            }
            return response;
        }
    }
}
