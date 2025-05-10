using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeptunBackend.Data;
using NeptunBackend.Models;
using NeptunBackend.Models.DTO;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Services.Implementation;

public class TeacherService : NeptunService, ITeacherService
{

    public TeacherService(NeptunDbContext context) : base (context)
    {
    }

    public async Task<Teacher?> GetTeacherByNeptunCode(string neptunCode)
    {
        return await _context.Teachers
            .FirstOrDefaultAsync(t => t.NeptunCode == neptunCode);
    }

    public Task<List<Teacher?>> GetAllTeachers()
    {
        return _context.Teachers.ToListAsync();
    }

    public async Task<Teacher> AddTeacher(CreateTeacherDTO teacher)
    {
        IPasswordHasher<Teacher> passwordHasher = new PasswordHasher<Teacher>();
        var convertedTeacher = teacher.ToTeacher();
        convertedTeacher.Password = passwordHasher.HashPassword(convertedTeacher, convertedTeacher.Password);

        _context.Teachers.Add(convertedTeacher);
        await _context.SaveChangesAsync();
        return convertedTeacher;
    }
}