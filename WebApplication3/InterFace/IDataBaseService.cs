using System.Linq.Expressions;

namespace WebApplication3.InterFace
{
    public interface IDataBaseService<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Save(T entity);
        Task<List<T>> SaveRange(List<T> entity);

        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria);

        Task<T> Find(Expression<Func<T, bool>> criteria);

        Task<T> Update(T entity);


        Task<bool> Delete(int id);

        //List<T> Compare<T, TKey>(List<T> oldEntities, List<T> newEntities, Expression<Func<T, TKey>> propertySelector);
        List<T> Compare<TKey>(List<T> oldEntities, List<T> newEntities, Expression<Func<T, TKey>> propertySelector);
        Task<List<T>> DeleteRange(List<T> entities);

        Task<bool> DeleteCondation(Expression<Func<T, bool>> Filter);

        Task<List<T>> GetInclude(params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetIncludeWithCondition<TProperty>(Expression<Func<T, bool>> filter,params Expression<Func<T, TProperty>>[] includes);

        public  Task<string> SaveImageAsync(IFormFile imageFile, string savePath);


        //List<T> Compare<T, TKey>(List<T> oldEntities, List<T> newEntities, Expression<Func<T, TKey>> propertySelector);




    }
}
