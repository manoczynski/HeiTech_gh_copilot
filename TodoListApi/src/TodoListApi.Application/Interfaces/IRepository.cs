using System;
using System.Collections.Generic;

namespace TodoListApi.Application.Interfaces
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Get(Guid id);
        IEnumerable<T> GetAll();
        T Update(T entity);
        void Delete(Guid id);
    }
}