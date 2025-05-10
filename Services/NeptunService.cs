using NeptunBackend.Data;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Services;

public class NeptunService
{
    protected readonly NeptunDbContext _context;
    
    public NeptunService(NeptunDbContext context)
    {
        _context = context;
    }
    
}