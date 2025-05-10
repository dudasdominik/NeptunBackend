using Microsoft.EntityFrameworkCore;
using NeptunBackend.Models;

namespace NeptunBackend.Data;

public class NeptunDbContext : DbContext
{
    public NeptunDbContext(DbContextOptions<NeptunDbContext> options) : base(options) { }
    
    public DbSet<Student?> Students { get; set; }
    public DbSet<Teacher?> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Exam> Exams { get; set; }  
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<ExamRegistration> ExamRegistrations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Course>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        
        
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Teacher)
            .WithMany(t => t.TeacherCourses)
            .HasForeignKey("TeacherNeptunCode") // vagy "NeptunCode" ha így nevezted
            .HasPrincipalKey(t => t.NeptunCode);
        
        modelBuilder.Entity<ExamRegistration>()
            .HasKey(er => er.Id);

        modelBuilder.Entity<ExamRegistration>()
            .HasOne(er => er.Student)
            .WithMany(s => s.ExamRegistrations)
            .HasForeignKey(er => er.StudentNeptunCode);

        modelBuilder.Entity<ExamRegistration>()
            .HasOne(er => er.Exam)
            .WithMany(e => e.ExamRegistrations)
            .HasForeignKey(er => er.ExamId);

        modelBuilder.Entity<Exam>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Exams)
            .HasForeignKey("CourseId"); // vagy a pontos property neve ha explicit van

    }
}