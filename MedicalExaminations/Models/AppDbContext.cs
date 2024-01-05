using MedicalExaminations.Models.PermissionManagers;
using Microsoft.EntityFrameworkCore;

namespace MedicalExaminations.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalCategory> AnimalCategories { get; set; }
        public DbSet<AnimalPhoto> AnimalPhotos { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<MedicalExamination> MedicalExaminations { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationAttribute> OrganizationAttributes { get; set; }
        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        public DbSet<OwnerSign> OwnerSigns { get; set; }
        public DbSet<PermissionManager> PermissionManagers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
