using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data
{
    public interface IGenericRepository<TEntity>
       where TEntity : global::Account.Core.BaseEntity
    {
        void Delete(Func<TEntity, bool> where);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        bool Exists(object primaryKey);
        TEntity FirstOrDefault(Func<TEntity, bool> where);
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(object id);
        TEntity GetFirst(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> GetMany(Func<TEntity, bool> where);
        IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where);
        IQueryable<TEntity> GetQueryable();
        IQueryable<TEntity> ReadOnly();
        TEntity GetSingle(Func<TEntity, bool> predicate);
        IQueryable<TEntity> GetWithInclude(global::System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);

        IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);
    }
}
