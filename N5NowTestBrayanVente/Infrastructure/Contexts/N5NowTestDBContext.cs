using Microsoft.EntityFrameworkCore;
using N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models;

namespace N5NowTestBrayanVente.Infrastructure.Contexts
{
    public class N5NowTestDBContext : DbContext
    {
        public N5NowTestDBContext(DbContextOptions<N5NowTestDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionTypes> PermissionTypes { get; set; }
    }
}
