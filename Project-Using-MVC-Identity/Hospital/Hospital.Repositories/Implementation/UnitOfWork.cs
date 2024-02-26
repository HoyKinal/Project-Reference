using Hospital.Repositories.Implementation;
using Hospital.Repositories.Interfaces;
using Hospital.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> GetGenericRepository<T>() where T : class
    {
        IGenericRepository<T> repo = new GenericRepository<T>(_context);
        return repo;
    }

    // Implement the GenericRepository<T> method
    public IGenericRepository<T> GenericRepository<T>() where T : class
    {
        return GetGenericRepository<T>();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    private bool disposed = false;

    protected void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
