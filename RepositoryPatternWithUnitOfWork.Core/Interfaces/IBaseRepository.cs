﻿using RepositoryPatternWithUnitOfWork.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static RepositoryPatternWithUnitOfWork.Core.Consts.OrderBy;

namespace RepositoryPatternWithUnitOfWork.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task <T> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        T Find(Expression<Func<T, bool>> match, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int take, int skip);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int? take, int skip, Expression<Func<T, object>> orderBy = null, string OrderByDirection = OrderBy.Ascending);

        T Add(T entity);
        
         IEnumerable<T> AddRange(IEnumerable<T> entities);


    }
}
