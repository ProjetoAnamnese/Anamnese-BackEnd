using Anamnese.API.ORM.Context;
using Anamnese.API.ORM.Interface;
using Microsoft.EntityFrameworkCore;

namespace Anamnese.API.ORM.Repository
{
    public class BaseRepository<T> :IBaseRepository<T> where T : class
    {
        protected AnamneseDbContext _context;

        public BaseRepository(AnamneseDbContext context)
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
        

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }


    }
}
