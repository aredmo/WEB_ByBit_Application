using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;

namespace WebApplication2.Repositories
{
    public class BalanceRepository : IRepository<BalanceEntity>

    {
        DbContext _context;
        DbSet<BalanceEntity> _dbSet;

        public BalanceRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<BalanceEntity>();
        }

        public int Create(BalanceEntity item)
        {
            _dbSet.Add(item);
            return _context.SaveChanges();
        }

        public List<BalanceEntity> Read(BalanceEntity item)
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public int Update(BalanceEntity item)
        {
            _context.Entry(item);
            return _context.SaveChanges();
        }

        public int Delete(BalanceEntity item)
        {
            _dbSet.Remove(item);
            return _context.SaveChanges();
        }
    }
}