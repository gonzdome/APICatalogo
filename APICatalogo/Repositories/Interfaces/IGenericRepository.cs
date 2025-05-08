﻿using System.Linq.Expressions;

namespace APICatalogo.Repositories.Interfaces;

public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(Expression<Func<T, bool>> predicate);
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(T entity);
}
