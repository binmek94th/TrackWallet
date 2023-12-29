using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TrackWallet.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(string? includeProperties = null);
    T GetFirstOrDefault(Expression<Func<T, bool>> filter);
    T Get(Expression<Func<T, bool>> filter);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entity);
}