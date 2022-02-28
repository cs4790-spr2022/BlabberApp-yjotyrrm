using System.Collections;

namespace BlabberApp.Domain.Common.Interfaces
{
    public interface IRepository<T>
    {
        public void Add(T entity);
        public void Remove(T entity);
        public void RemoveAll();
        public void Update(T entity);
        public IEnumerable<T> GetAll();
        public T GetById(Guid Id);
    }
}