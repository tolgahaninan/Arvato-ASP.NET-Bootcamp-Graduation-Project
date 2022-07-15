using Microsoft.EntityFrameworkCore;
using GradBootcamp_Tolgahaninan.Models;

namespace GradBootcamp_Tolgahaninan.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) // DbContext
        {
           
        }

        public DbSet<User> users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> genres { get; set; }
        public DbSet<MovieView> movieViews { get; set; }

  

    }
}
