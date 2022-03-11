using Common.StandardInfrastructure.Interface;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
// ReSharper disable PossibleMultipleEnumeration

namespace Common.StandardInfrastructure.Repository
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class, IBaseEntityModel
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> DbSet;
        public RepositoryAsync(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        private Expression<Func<T, bool>> GetPredicate(Expression<Func<T, bool>> predicate = null, bool isDelete = true)
        {
            ExpressionStarter<T> expr;
            if (predicate == null) expr = PredicateBuilder.New<T>(true); else expr = predicate;
            if (isDelete) expr = expr.And(q => !q.IsDelete);
            return expr;
        }


        public async Task<(IEnumerable<T>, int)> GetPagedListAsync(Expression<Func<T, bool>> predicate, PagingSortingDto pagingSortingDto, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = false, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, bool>> predicateUnion = null, bool isDelete = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking) query = query.AsNoTracking();
            query = query.Where(GetPredicate(predicate));
            query = query.OrderByWithDirection(pagingSortingDto.SortDirection, pagingSortingDto.SortField, orderBy);
            if (include != null) query = include(query).AsSplitQuery();
            var count = await query.CountAsync();
            var queryData = pagingSortingDto.Limit == 1 ? query : query.GetPaggedList(pagingSortingDto.Offset, pagingSortingDto.Limit ?? 10);
            if (predicateUnion == null) return (await queryData.ToListAsync(), count);
            IQueryable<T> queryUnion = DbSet;
            var unionData = queryUnion.Where(GetPredicate(predicateUnion, isDelete));
            if (include != null) unionData = include(unionData).AsSplitQuery();
            var result = queryData.Union(unionData);
            return (await result.ToListAsync(), count);
        }
        public (IEnumerable<T>, int) GetPagedList(Expression<Func<T, bool>> predicate, PagingSortingDto pagingSortingDto, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = false, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, bool>> predicateUnion = null, bool isDelete = true)
        {
            IQueryable<T> dbsetQuery = DbSet;
            if (include != null) dbsetQuery = include(dbsetQuery).AsSplitQuery();
            if (dbsetQuery != null)
            {
                var query = dbsetQuery.AsEnumerable();
                query = query.Where(GetPredicate(predicate).Compile());
                query = query.AsQueryable().OrderByWithDirection(pagingSortingDto.SortDirection, pagingSortingDto.SortField, orderBy);
                var count = query.Count();
                var queryData = pagingSortingDto.Limit == 1 ? query : query.AsQueryable().GetPaggedList(pagingSortingDto.Offset, pagingSortingDto.Limit ?? 10);
                if (predicateUnion == null) return (queryData.ToList(), count);
                IQueryable<T> queryUnion = DbSet;
                var unionData = queryUnion.Where(GetPredicate(predicateUnion, isDelete));
                if (include != null) unionData = include(unionData).AsSplitQuery();
                var result = queryData.Union(unionData);
                return (result.ToList(), count);
            }

            return (null, 0);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, PagingSortingDto pagingSortingDto = null,
            bool disableTracking = false, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool isDelete = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking) query = query.AsNoTracking();
            query = query.Where(GetPredicate(predicate, isDelete));
            if (pagingSortingDto != null)
                query = query.OrderByWithDirection(pagingSortingDto.SortDirection, pagingSortingDto.SortField);
            if (include != null) query = include(query).AsSplitQuery();
            return await query.ToListAsync();
        }


        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = false, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool isDelete = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query).AsSplitQuery();
            if (orderBy != null) query = orderBy(query);
            return await query.FirstOrDefaultAsync(GetPredicate(predicate, isDelete));
        }
        public async Task<T> GetAsync(params object[] id)
        {
            IQueryable<T> query = DbSet;
            return await query.FirstOrDefaultAsync(q => q.Id == new Guid(id[0].ToString()));
        }
        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = false, bool isDelete = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking) query = query.AsNoTracking();
            query = query.Where(GetPredicate(isDelete: isDelete));
            if (include != null) query = include(query).AsSplitQuery();
            return await query.ToListAsync();
        }

        public async Task<bool> IsExistAsync<TDto>(TDto dto, bool isDelete = true,  bool includeCountry = true) => await DbSet.AnyAsync(GetPredicate(Helper.GetPredicateIsExist<T, TDto>(dto, includeCountry), isDelete));
        public async Task<bool> IsExistAnyAsync(Expression<Func<T, bool>> predicate, bool isDelete = true) => await DbSet.AnyAsync(GetPredicate(predicate, isDelete));
        public async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate = null, bool isDelete = true) => predicate == null ? await DbSet.CountAsync(GetPredicate(isDelete: isDelete)) : await DbSet.CountAsync(GetPredicate(predicate, isDelete));


        public async Task<IEnumerable<TType>> FindSelectAsync<TType>(Expression<Func<T, bool>> predicate, Expression<Func<T, TType>> select, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, PagingSortingDto pagingSortingDto = null, bool disableTracking = false, bool isDelete = true) where TType : class
        {
            IQueryable<T> query = DbSet;
            if (disableTracking) query = query.AsNoTracking();
            query = query.Where(GetPredicate(predicate, isDelete));
            if (pagingSortingDto != null) query.OrderByWithDirection(pagingSortingDto.SortDirection, pagingSortingDto.SortField);
            if (include != null) query = include(query).AsSplitQuery();
            return await query.Select(select).ToListAsync();
        }

        public async Task<TType> FirstOrDefaultSelectAsync<TType>(Expression<Func<T, bool>> predicate, Expression<Func<T, TType>> select, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, PagingSortingDto pagingSortingDto = null, bool disableTracking = false, bool isDelete = true,  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where TType : class
        {
            IQueryable<T> query = DbSet;
            if (disableTracking) query = query.AsNoTracking();
            query = query.Where(GetPredicate(predicate, isDelete));
            if (pagingSortingDto != null) query.OrderByWithDirection(pagingSortingDto.SortDirection, pagingSortingDto.SortField);
            if (include != null) query = include(query).AsSplitQuery();
            if (orderBy != null) query = orderBy(query);
            return await query.Select(select).FirstOrDefaultAsync();
        }
        public async Task<(IEnumerable<TType>, int)> GetPagedListSelectAsync<TType>(Expression<Func<T, bool>> predicate, Expression<Func<T, TType>> select, PagingSortingDto pagingSortingDto, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = false, bool isDelete = true,  Expression<Func<T, bool>> predicateUnion = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where TType : class
        {
            IQueryable<T> query = DbSet;
            if (disableTracking) query = query.AsNoTracking();
            query = query.Where(GetPredicate(predicate));
            query = query.OrderByWithDirection(pagingSortingDto.SortDirection, pagingSortingDto.SortField, orderBy);
            if (include != null) query = include(query).AsSplitQuery();
            var count = await query.CountAsync();
            var queryData = pagingSortingDto.Limit == 1 ? query : query.GetPaggedList(pagingSortingDto.Offset, pagingSortingDto.Limit ?? 10);
            if (predicateUnion == null) return (await queryData.Select(select).ToListAsync(), count);
            IQueryable<T> queryUnion = DbSet;
            var unionData = queryUnion.Where(GetPredicate(predicateUnion, isDelete));
            if (include != null) unionData = include(unionData).AsSplitQuery();
            var result = queryData.Union(unionData);
            return (await result.Select(select).ToListAsync(), count);
        }
        public TResult ExecuteQuery<TResult>(Func<IQueryable<T>, TResult> queryFunction) => queryFunction(DbSet);
        public IList<TReturn> GetGrouped<TResult, TGroup, TReturn>(
            List<Expression<Func<T, bool>>> predicates,
            PagingSortingDto pagingSortingDto,
            Expression<Func<T, TResult>> firstSelector,
            Func<TResult, TGroup> groupSelector,
            Func<IGrouping<TGroup, TResult>, TReturn> selector,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var entities = DbSet.AsQueryable();
            if (include != null) entities = include(entities).AsSplitQuery();
            return predicates
                .Aggregate(entities, (current, predicate) => current.Where(predicate))
                .Select(firstSelector)
                .OrderByWithDirection(pagingSortingDto.SortDirection, pagingSortingDto.SortField)
                .GroupBy(groupSelector).Skip(--pagingSortingDto.Offset * pagingSortingDto.Limit ?? 10).Take(pagingSortingDto.Limit ?? 0)
                .Select(selector)
                .ToList();
        }
        public async Task<T> AddAsync(T entity)
        {
            return (await DbSet.AddAsync(entity)).Entity;
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
             await DbSet.AddRangeAsync(entities);            
        }
        public async Task RemoveAsync(T entity)
        {
            var task = Task.Factory.StartNew(() =>
            {
                    entity.IsDelete = true;            
            });
            await task;
        }
        public async Task RemovePhysicalAsync(T entity)
        {
            var task = Task.Factory.StartNew(() =>
            {
                    DbSet.Remove(entity);
            });
            await task;
        }
        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            var task = Task.Factory.StartNew(() =>
            {
                    DbSet.RemoveRange(entities);               
            });
            await task;
        }
        public async Task UpdateAsync(T entityNew, T entityOld)
        {
            var task = Task.Factory.StartNew(() =>
            {
                entityNew = Helper.EditOriginalValues(entityNew, entityOld);
                Context.Entry(entityOld).State = EntityState.Detached; // exception when trying to change the state
                Context.Entry(entityNew).State = EntityState.Modified; // exception when trying to change the state
                Context.Entry(entityOld).CurrentValues.SetValues(entityNew);
            });
            await task;
        }
        //Under Test
        public async Task UpdateRangeAsync(IEnumerable<T> entityNew, IEnumerable<T> entityOld)
        {
            await RemoveRangeAsync(entityOld);
            await AddRangeAsync(entityNew);
        }
        public async Task UpdateRangeAsync(IEnumerable<T> entityNew)
        {
            var task = Task.Factory.StartNew(() => Context.UpdateRange(entityNew));
            await task;
        }
        public IQueryable<T> GetAllAsQueryable(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = DbSet;
            if (include != null)
            {
                query = include(query).AsSplitQuery();
            }

            if (predicate != null)
            {
                query = query.Where(GetPredicate(predicate));
            }
            return query;
        }
    }


}
