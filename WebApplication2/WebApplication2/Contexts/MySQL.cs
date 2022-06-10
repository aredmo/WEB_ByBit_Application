using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;

namespace WebApplication2.Contexts
{
    public class MySQL : DbContext
    {
        public DbSet<InfoWallet> InfoWallets { get; set; } // таблица класса InfoWallet
        
        public MySQL(DbContextOptions<MySQL> option) : base(option)
        {
            Database.EnsureCreated();
        }
    }
}