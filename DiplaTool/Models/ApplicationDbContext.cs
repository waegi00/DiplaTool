using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DiplaTool.Models
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<ApplicationUser> Users { get; set; }

        public virtual DbSet<ApplicationRole> Roles { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new DbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Entity<Subject>().HasMany(x => x.Roles).WithMany(x => x.Subjects);
        }
    }
}