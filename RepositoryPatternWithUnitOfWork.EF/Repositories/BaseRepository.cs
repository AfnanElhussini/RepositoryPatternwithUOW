using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using RepositoryPatternWithUnitOfWork.Core.Consts;
using RepositoryPatternWithUnitOfWork.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUnitOfWork.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class

    {
        protected ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {

            return await _context.Set<T>().FindAsync(id);
        }

        public T Find(System.Linq.Expressions.Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable <T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return _context.Set<T>().SingleOrDefault(match);
        }
        public IEnumerable<T> FindAll(System.Linq.Expressions.Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return _context.Set<T>().Where(match).ToList();
        }
        public IEnumerable<T> FindAll(System.Linq.Expressions.Expression<Func<T, bool>> match, int take, int skip)
        {
            return _context.Set<T>().Where(match).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<T> FindAll(System.Linq.Expressions.Expression<Func<T, bool>> match, int? take, int skip, System.Linq.Expressions.Expression<Func<T, object>> orderBy = null, string OrderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(match);
            if (orderBy != null)
            {
                if (OrderByDirection == OrderBy.Ascending)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }
            }
            if (take.HasValue)
            {
                query = query.Skip(skip).Take(take.Value);
            }
            return query.ToList();
        }

        
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }
    }


}
