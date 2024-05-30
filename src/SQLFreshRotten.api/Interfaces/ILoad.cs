namespace SQLFreshRotten.api.Interfaces
{
    public interface ILoad<T> where T : class
    {
        Task InsertRange(List<T> entitys);
        Task InsertOne(T entity);
    }
}
