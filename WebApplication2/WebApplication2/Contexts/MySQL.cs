using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;

namespace WebApplication2.Contexts
{
    public class MySQL : DbContext
    {
        public DbSet<BalanceEntity> BalanceEntity { get; set; } // таблица класса BalanceEntity
        
        public MySQL(DbContextOptions<MySQL> option) : base(option)
        {
            Database.EnsureCreated();
        }
    }
}