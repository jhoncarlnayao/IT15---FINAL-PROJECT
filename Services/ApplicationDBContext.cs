using Microsoft.EntityFrameworkCore;
using IT15_FINALPROJECT.Model;

namespace IT15_FINALPROJECT.Services
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        {
        }
        public DbSet<Admin> AdminAccount { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

    }
}
