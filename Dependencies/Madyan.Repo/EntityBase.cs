
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace Madyan.Repo.Abstract
{

    public interface IEntityBase
    {
        
    }
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        IQueryable<T> Query(bool eager);
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetAll();
        int Count();
        IQueryable<T> BuildDynamicExpression(List<Property> properties, params Expression<Func<T, object>>[] includeProperties);
        T GetSingle(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        //IEnumerable<T> FullTextSearch(string code);
        T GetSingleAll(Expression<Func<T, bool>> predicate);
        
        void Add(T entity);
        void BulkInsert(IEnumerable<T> entity);
        void AddRange(T entity);
        void Update(T entity);
        void UpdateRange(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        void Commit();
    }
    
}
