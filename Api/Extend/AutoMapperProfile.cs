using AutoMapper;
using DisneyApi.Data;
using DisneyApi.Model.Genero;
using DisneyApi.Model.Pelicula;
using DisneyApi.Model.Personaje;

namespace DisneyApi.Extend
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Personaje, PersonajeModelDTO>();
            CreateMap<Personaje, PersonajeFullDTO>();
            CreateMap<Personaje, PersonajeSimpleDTO>();
            CreateMap<Pelicula, PeliculaSimpleDTO>();
            CreateMap<Pelicula, PeliculaFullDTO>();
            CreateMap<Pelicula, PeliculaModelDTO>();
            CreateMap<Genero, GeneroDTO>();
          
        }
    }
}
