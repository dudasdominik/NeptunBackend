using Microsoft.EntityFrameworkCore;
using NeptunBackend.Models;

namespace NeptunBackend.Data;

public class NeptunDbContext : DbContext
{
    public NeptunDbContext(DbContextOptions<NeptunDbContext> options) : base(options) { }
    
    public DbSet<Student?> Students { get; set; }
    public DbSet<Teacher?> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    
    
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
        
    }
}