using Microsoft.EntityFrameworkCore;
using ReportManager.DataAccess.Data;

namespace ReportManager.DataAccess.Repository.Implementation;
public class Repository<T> : IRepository<T> where T: class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> _dbSet;
    public Repository(ApplicationDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<T>();
    }
    
    public void Add(T obj)
    {
        var s = _dbSet.Add(obj);
    }

    public IEnumerable<T> GetAll(string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return query;
    }
}