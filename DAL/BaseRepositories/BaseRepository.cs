using BOS.Entities;
using BOS.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BaseRepositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IdentityContext _context;
        private DbSet<T> entities;
        public BaseRepository(IdentityContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public void Delete(T entity)
        {
            entities.Remove(entity);
            _context.SaveChanges();
        }
        public T Get(long id)
        {
            return entities.SingleOrDefault(s => s.ID == id);
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public void Insert(T entity)
        {
            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
