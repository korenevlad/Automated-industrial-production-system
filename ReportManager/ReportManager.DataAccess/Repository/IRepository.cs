namespace ReportManager.DataAccess.Repository;

public interface IRepository<T> where T: class
{
    void Add(T obj);
    IEnumerable<T> GetAll(string? includeProperties = null);
}