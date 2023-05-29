using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public partial class PermissionsContext : DbContext
    {
        public PermissionsContext()
        {
        }

        public PermissionsContext(DbContextOptions<PermissionsContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<PermissionType> PermissionTypes { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().ToTable("Permission");
            modelBuilder.Entity<PermissionType>().ToTable("PermissionType");
        }
    }
}
