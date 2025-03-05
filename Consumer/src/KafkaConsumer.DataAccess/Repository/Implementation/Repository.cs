using KafkaConsumer.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace KafkaConsumer.DataAccess.Repository.Implementation;
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
    public void Update(T obj)
    {
        _dbSet.Update(obj);
    }
}