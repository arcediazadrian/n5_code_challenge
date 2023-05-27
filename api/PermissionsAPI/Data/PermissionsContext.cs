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

            modelBuilder.Entity<PermissionType>().HasData(new PermissionType() { Id = 1, Description = "Employee" }, new PermissionType() { Id = 2, Description = "Manager" });
            modelBuilder.Entity<Permission>().HasData(new Permission() { Id = 1, EmployeeFirstName = "Adrian", EmployeeLastName = "Arce", GrantedDate = DateTime.Now, PermissionTypeId = 1 }, new Permission() { Id = 2, EmployeeFirstName = "Alejandro", EmployeeLastName = "Diaz", GrantedDate = DateTime.Now, PermissionTypeId = 2 });
        }
    }
}
