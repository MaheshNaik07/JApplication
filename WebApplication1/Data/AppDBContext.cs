using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {    
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}
