using Microsoft.EntityFrameworkCore;
using IT15_FINALPROJECT.Model;

namespace IT15_FINALPROJECT.Model
{
    public class TenantContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        public TenantContext(DbContextOptions options ) : base(options)
        {

        }   
    }
}
