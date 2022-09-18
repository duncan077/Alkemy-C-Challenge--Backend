using DisneyApi.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DisneyApi.Data
{
    public class DisneyContext: IdentityDbContext
    {
        public DisneyContext(DbContextOptions options) : base(options)
        {
        }
        DbSet<DisneyUser> disneyUsers { get; set; }
        public DbSet<Pelicula> peliculas { get; set; }
        public DbSet<Personaje> personajes { get; set; }
        public DbSet<Genero> generos { get; set; }
    }
   
}
