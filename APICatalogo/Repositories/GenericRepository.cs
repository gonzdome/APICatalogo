using APICatalogo.Context;
using APICatalogo.Repositories.Interfaces;
using System.Linq.Expressions;

namespace APICatalogo.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T? Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return SaveChanges(entity);
        }
        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return SaveChanges(entity);
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return SaveChanges(entity);
        }

        private T SaveChanges(T entity)
        {
            _context.SaveChanges();
            return entity;
        }
    }
}
