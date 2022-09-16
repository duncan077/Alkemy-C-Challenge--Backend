using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DisneyApi.Data
{
    public class DisneyContext: IdentityDbContext
    {
        public DisneyContext(DbContextOptions options) : base(options)
        {
        }
    }
}
