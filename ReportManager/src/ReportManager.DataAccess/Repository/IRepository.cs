using System.Linq.Expressions;

namespace ReportManager.DataAccess.Repository;

public interface IRepository<T> where T: class
{
    void Add(T obj);
    IEnumerable<T> GetAll(string? includeProperties = null);
    T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
}