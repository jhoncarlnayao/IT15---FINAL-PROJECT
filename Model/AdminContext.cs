using Microsoft.EntityFrameworkCore;

namespace IT15_FINALPROJECT.Model
{
    public class AdminContext : DbContext
    {
        public DbSet<Admin> AdminAccount { get; set; }

        public AdminContext(DbContextOptions<AdminContext> options) : base(options) // ✅ Fix here
        {
        }
    }
}
