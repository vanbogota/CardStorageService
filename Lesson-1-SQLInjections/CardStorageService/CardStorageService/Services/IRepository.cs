namespace CardStorageService.Services
{
    public interface IRepository<T,TKey>
    {
        IList<T> GetAll();
        T GetById(TKey id);
        TKey Create(T data);
        int Update(T data);
        int Delete(TKey id);
    }
}
