using Microsoft.EntityFrameworkCore;
using SampleProject_API.Models;

namespace SampleProject_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
       
    }
}
