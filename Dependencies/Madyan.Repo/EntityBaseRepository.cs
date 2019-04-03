
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Madyan.Data.Context;
using LinqKit;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query;

namespace Madyan.Repo.Abstract
{
    //public class EfRepository<TDbContext, TEntity> : IRepository<TEntity>
    //where TDbContext : DbContext
    //where TEntity : class, IEntity
    //{
    //    public EfRepository(IDbContextProvider<TDbContext> dbContextProvider)
    //    {
    //    }
    //}
    public class EntityBaseRepository<TDbContext, T> : IEntityBaseRepository<T>
        where TDbContext : DbContext
            where T : class, IEntityBase, new()
    {

        private TDbContext _context;

        #region Properties
        public EntityBaseRepository(TDbContext context)
        {
            try
            {

                _context = context;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }
        //public IEnumerable<T> FullTextSearch(string code)
        //{
        //    IQueryable<T> query = _context.Set<T>();
        //    query.Where(x => EF.Functions.Contains(x.KeySearchField, code)).ToList();
        //    return _context.Set<T>.Where(x => EF.Functions.Contains(x.KeySearchField, code)).ToList();
        //}
        public virtual int Count()
        {
            try
            {
                return _context.Set<T>().Count();
            }
            catch (Exception ex) { throw ex; }
        }
        public virtual IQueryable<T> Query(bool eager = false)
        {
            try
            {
                var query = _context.Set<T>().AsQueryable();
                if (eager)
                {
                    foreach (var property in _context.Model.FindEntityType(typeof(T)).GetNavigations())
                        query = query.Include(property.Name);
                }
                return query;
            }
            catch (Exception ex) { throw ex; }
        }
        public static object GetPropertyValue(object src, string propName)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }

        



    private static Expression<Func<T,bool>> GetDynamicExpresionTrees(string propertyName, string val,bool isEqual)
        {
            //x =>  
            var param = Expression.Parameter(typeof(T), "x");
            var propertyOrField = propertyName.Split('.').Aggregate((Expression)param, Expression.PropertyOrField);
            //var member = Expression.Property(param, propertyOrField.ToString());
            //val ("Curry")  
            var valExpression = Expression.Constant(val, typeof(string));
            //x.LastName == "Curry"  
            Expression body = null;
            if (isEqual)
            { body = Expression.Equal(propertyOrField, valExpression); }
            else
            {
                body = Expression.NotEqual(propertyOrField, valExpression);
            }
            //x => x.LastName == "Curry"  
            var final = Expression.Lambda<Func<T,
                bool>>(body: body, parameters: param);
            //compiles the expression tree to a func delegate  
            // return final.Compile();
            return final;
        }
        //params Expression<Func<T, object>>[] includeProperties)
        //{
        //    try
        //    {
        //        IQueryable<T> query = _context.Set<T>();
        //        foreach (var includeProperty in includeProperties)
        //        {
        //            query = query.Include(includeProperty);
        //        }
        public IQueryable<T> BuildDynamicExpression(List<Property> properties, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                //var properties = JsonConvert.DeserializeObject<Property[]>(jsonString);
                var predicate = PredicateBuilder.New<T>();
              
                var type = typeof(T);
                foreach (Property objProperty in properties)
                {
                    
                    if (objProperty.logical_operator == "and")
                    {
                        switch (objProperty.action)
                        {
                            case "contains":
                                predicate = predicate.And(GetDynamicExpresionTrees(objProperty.property, objProperty.value,true));

                                break;
                            case "doesn't contain":
                                predicate = predicate.And(GetDynamicExpresionTrees(objProperty.property, objProperty.value, false));

                                break;
                            default: break;
                        }
                    }
                    else
                    {
                        switch (objProperty.action)
                        {
                            case "contains":
                                predicate = predicate.Or(GetDynamicExpresionTrees(objProperty.property, objProperty.value, true));

                                break;
                            case "doesn't contain":
                                predicate = predicate.Or(GetDynamicExpresionTrees(objProperty.property, objProperty.value, true));

                                break;
                            default: break;
                        }
                    }

                }



                //below always yields false as the output of invocation
               // var matchPredicate = predicate.DefaultExpression;

                IQueryable<T> query = _context.Set<T>();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                var ef = query.Where(predicate);
                var sql = ef.ToSql();

                return query.Where(predicate);
            }
            catch (Exception ex) { throw ex; }
        }
       
        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.AsEnumerable();
            }
            catch (Exception ex) { throw ex; }
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                return query.Where(predicate).FirstOrDefault();
            }
            catch (Exception ex) { throw ex; }
        }



        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return query.Where(predicate).FirstOrDefault();
            }
            catch (Exception ex) { throw ex; }
        }
        public virtual T GetSingleAll(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var query = _context.Set<T>().Where(predicate);

                foreach (var property in _context.Model.FindEntityType(typeof(T)).GetNavigations())
                    query = query.Include(property.Name);

                return query.FirstOrDefault();
            }
            catch (Exception ex) { throw ex; }
        }
        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>().Where(predicate);
            }
            catch (Exception ex) { throw ex; }
        }
        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return query.Where(predicate).AsEnumerable();
            }
            catch (Exception ex) { throw ex; }
            //return _context.Set<T>().Where(predicate);
        }
        public virtual void Add(T entity)
        {
            try
            {
                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                _context.Set<T>().Add(entity);
            }
            catch (Exception ex) { throw ex; }
        }
        public void BulkInsert(IEnumerable<T> entity)
        {
            try
            {
                foreach (T ent in entity)
                {
                    EntityEntry dbEntityEntry = _context.Entry<T>(ent);
                    _context.Set<T>().Add(ent);
                }
            }
            catch (Exception ex) { throw ex; }
        }
        public virtual void AddRange(T entity)
        {
            try
            {
                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                //_context.Set<T>().Add(entity);
                _context.Set<T>().AddRange(entity);
            }
            catch (Exception ex) { throw ex; }
        }
        public virtual void Update(T entity)
        {
            try
            {
                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                dbEntityEntry.State = EntityState.Modified;
            }
            catch (Exception ex) { throw ex; }
        }
        public virtual void UpdateRange(T entity)
        {
            try
            {
                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                dbEntityEntry.State = EntityState.Modified;
                _context.Set<T>().UpdateRange(entity);
            }
            catch (Exception ex) { throw ex; }
        }
        public virtual void Delete(T entity)
        {
            try
            {
                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                dbEntityEntry.State = EntityState.Deleted;
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IEnumerable<T> entities = _context.Set<T>().Where(predicate);

                foreach (var entity in entities)
                {
                    _context.Entry<T>(entity).State = EntityState.Deleted;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


    }
    public static class StringExtensions
    {
       
        public static Expression<Func<T, object>> ToMemberOf<T>(this string name) where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            var propertyOrField = name.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);
            var unaryExpression = Expression.MakeUnary(ExpressionType.Convert, propertyOrField, typeof(object));

            return Expression.Lambda<Func<T, object>>(unaryExpression, parameter);
        }
    }
        public static class IQueryableExtensions
    {
        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");

        private static readonly FieldInfo QueryModelGeneratorField = QueryCompilerTypeInfo.DeclaredFields.First(x => x.Name == "_queryModelGenerator");

        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly PropertyInfo DatabaseDependenciesField = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");

        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var queryCompiler = (QueryCompiler)QueryCompilerField.GetValue(query.Provider);
            var modelGenerator = (QueryModelGenerator)QueryModelGeneratorField.GetValue(queryCompiler);
            var queryModel = modelGenerator.ParseQuery(query.Expression);
            var database = (IDatabase)DataBaseField.GetValue(queryCompiler);
            var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesField.GetValue(database);
            var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
            var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            var sql = modelVisitor.Queries.First().ToString();

            return sql;
        }
    }
}
