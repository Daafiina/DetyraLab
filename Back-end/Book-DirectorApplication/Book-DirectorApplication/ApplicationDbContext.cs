using Book_DirectorApplication.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Book_DirectorApplication
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Book> books { get; set; }
        public DbSet<Author> authors { get; set; }
    }
}
