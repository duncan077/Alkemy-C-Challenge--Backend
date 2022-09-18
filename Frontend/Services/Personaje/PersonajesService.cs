using Blazored.LocalStorage;
using Frontend.Services.Base;

namespace Frontend.Services
{
    public class PersonajesService : BaseHttpService, IPersonajesService
    {
        private readonly IClient client;

        public PersonajesService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.client = client;
        }
        public async Task<Response<List<PersonajeSimpleDTO>>> GetPersonajes(string name, int age, int movies)
        {
            Response<List<PersonajeSimpleDTO>> response;
            try
            {
                await GetBearerToken();
                var data = await client.CharactersAllAsync(name, age, movies);
                response = new Response<List<PersonajeSimpleDTO>>
                {
                    Result = data.ToList(),
                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<List<PersonajeSimpleDTO>>(ex);
            }
            return response;
        }
        public async Task<Response<PersonajeFullDTO>> GetPersonaje(int id)
        {
            Response<PersonajeFullDTO> response;
            try
            {
                await GetBearerToken();
                var data = await client.CharactersGETAsync(id);
                response = new Response<PersonajeFullDTO>
                {
                    Result = data,
                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<PersonajeFullDTO>(ex);
            }
            return response;
        }
        public async Task<Response<PersonajeModelDTO>> AddPersonaje(PersonajeModelDTO personajeModelDTO)
        {
            Response<PersonajeModelDTO> response;
            try
            {
                await GetBearerToken();
                var data = await client.CharactersPOSTAsync(personajeModelDTO);
                response = new Response<PersonajeModelDTO>
                {
                    Result = data,
                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<PersonajeModelDTO>(ex);
            }
            return response;
        }
        public async Task<Response<PersonajeModelDTO>> UpdatePersonaje(PersonajeModelDTO personajeModelDTO, int id)
        {
            Response<PersonajeModelDTO> response;
            try
            {
                await GetBearerToken();
                await client.CharactersPUTAsync(id, personajeModelDTO);
                response = new Response<PersonajeModelDTO>
                {

                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<PersonajeModelDTO>(ex);
            }
            return response;
        }
        public async Task<Response<PersonajeModelDTO>> DeletePersonaje(int id)
        {
            Response<PersonajeModelDTO> response;
            try
            {
                await GetBearerToken();
                await client.CharactersDELETEAsync(id);
                response = new Response<PersonajeModelDTO>
                {

                    IsSuccess = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<PersonajeModelDTO>(ex);
            }
            return response;
        }
    }
}
