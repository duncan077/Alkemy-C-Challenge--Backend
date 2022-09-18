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
            CreateMap<Personaje, PersonajeModelDTO>().ForPath(s => s.peliculas, opt => opt.Ignore()).ReverseMap().ForPath(s => s.peliculas, opt => opt.Ignore()); 
            CreateMap<Personaje, PersonajeFullDTO>();
            CreateMap<Personaje, PersonajeSimpleDTO>() ;
            CreateMap<Pelicula, PeliculaSimpleDTO>() ;
            CreateMap<Pelicula, PeliculaFullDTO>() ;
            CreateMap<Pelicula, PeliculaModelDTO>().ForPath(s => s.generos, opt => opt.Ignore()).ForPath(s => s.personajes, opt => opt.Ignore()).ReverseMap().ForPath(s => s.generos, opt => opt.Ignore()).ForPath(s => s.personajes, opt => opt.Ignore()); 
            CreateMap<Genero, GeneroDTO>().ReverseMap().ForPath(s => s.peliculas, opt => opt.Ignore()); ;
          
        }
    }
}
