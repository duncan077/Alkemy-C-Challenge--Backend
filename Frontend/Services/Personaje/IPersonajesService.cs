using Frontend.Services.Base;

namespace Frontend.Services
{
    public interface IPersonajesService
    {
        Task<Response<PersonajeModelDTO>> AddPersonaje(PersonajeModelDTO personajeModelDTO);
        Task<Response<PersonajeModelDTO>> DeletePersonaje(int id);
        Task<Response<PersonajeFullDTO>> GetPersonaje(int id);
        Task<Response<List<PersonajeSimpleDTO>>> GetPersonajes(string name, int age, int movies);
        Task<Response<PersonajeModelDTO>> UpdatePersonaje(PersonajeModelDTO personajeModelDTO, int id);
    }
}