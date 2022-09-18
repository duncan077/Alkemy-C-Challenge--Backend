using Blazored.LocalStorage;
using Frontend.Services.Base;

namespace Frontend.Services
{
    public class GenerosService : BaseHttpService, IGenerosService
    {
        private readonly IClient client;

        public GenerosService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.client = client;
        }
        public async Task<Response<List<GeneroDTO>>> GetGeneros()
        {
            Response<List<GeneroDTO>> response;
            try
            {
                await GetBearerToken();
                var data = await client.GenerosAllAsync();
                response = new Response<List<GeneroDTO>>
                {
                    Result = data.ToList(),
                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<List<GeneroDTO>>(ex);
            }
            return response;
        }
        public async Task<Response<GeneroDTO>> GetGenero(int id)
        {
            Response<GeneroDTO> response;
            try
            {
                await GetBearerToken();
                var data = await client.GenerosGETAsync(id);
                response = new Response<GeneroDTO>
                {
                    Result = data,
                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<GeneroDTO>(ex);
            }
            return response;
        }
        public async Task<Response<GeneroDTO>> AddGenero(GeneroDTO generoDTO)
        {
            Response<GeneroDTO> response;
            try
            {
                await GetBearerToken();
                var data = await client.GenerosPOSTAsync(generoDTO);
                response = new Response<GeneroDTO>
                {
                    Result = data,
                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<GeneroDTO>(ex);
            }
            return response;
        }
        public async Task<Response<GeneroDTO>> UpdateGenero(GeneroDTO generoDTO, int id)
        {
            Response<GeneroDTO> response;
            try
            {
                await GetBearerToken();
                await client.GenerosPUTAsync(id, generoDTO);
                response = new Response<GeneroDTO>
                {

                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<GeneroDTO>(ex);
            }
            return response;
        }
        public async Task<Response<GeneroDTO>> DeleteGenero(int id)
        {
            Response<GeneroDTO> response;
            try
            {
                await GetBearerToken();
                await client.GenerosDELETEAsync(id);
                response = new Response<GeneroDTO>
                {
                   
                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<GeneroDTO>(ex);
            }
            return response;
        }
    }
}
