using Lesson04.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lesson04.Data
{
    public class UniversityDbContext : DbContext
    {
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<CourseAssignment> CourseAssignments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseAssignment>().ToTable(nameof(CourseAssignment));
            modelBuilder.Entity<CourseAssignment>().HasKey(x => x.Id);
            modelBuilder.Entity<CourseAssignment>().Property(x => x.Room)
                .HasMaxLength(15)
                .IsRequired();
            modelBuilder.Entity<CourseAssignment>()
                .HasOne(x => x.Instructor)
                .WithMany(i => i.Assignments)
                .HasForeignKey(x => x.InstructorId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CourseAssignment>()
                .HasOne(x => x.Course)
                .WithMany(c => c.Assignments)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
