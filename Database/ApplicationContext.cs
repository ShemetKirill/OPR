using CourseOPR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseOPR.Database
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Faculty> Faculty { get; set; } = null!;
        public DbSet<Speciality> Speciality { get; set; } = null!;
        public DbSet<Group> Group { get; set; } = null!;
        public DbSet<Student> Student { get; set; } = null!;
        public DbSet<Subject> Subject { get; set; } = null!;
        public DbSet<Score> Score { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Speciality>()
                .HasOne(u => u.Faculty)
                .WithMany(c => c.Specialities)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Group>()
                .HasOne(u=>u.Speciality)
                .WithMany(c=>c.Groups)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Student>()
                .HasOne(u => u.Group)
                .WithMany(c => c.Students)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Subject>()
                .HasOne(u => u.Speciality)
                .WithMany(c => c.Subjects)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Score>()
                .HasOne(u => u.Student)
                .WithMany(c => c.Scores)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
