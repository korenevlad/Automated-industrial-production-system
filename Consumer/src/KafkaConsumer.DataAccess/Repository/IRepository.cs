namespace KafkaConsumer.DataAccess.Repository;

public interface IRepository<T> where T: class
{
    void Add(T obj);
    void Update(T obj);
}