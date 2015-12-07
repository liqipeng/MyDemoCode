
namespace Demo1.Interface
{
    public interface IDAL<T, TKey>
    {
        T FindByKey(TKey key);

        TKey Insert<T>(T entity);
    }
}
