using Microsoft.EntityFrameworkCore;

namespace IT15_FINALPROJECT.Model
{
    public class TenantContext : DbContext
    {
        public TenantContext(DbContextOptions<TenantContext> options)
            : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; } // Example table
    }
}
