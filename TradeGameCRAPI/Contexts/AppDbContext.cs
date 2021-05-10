using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TradeGameCRAPI.Entities;

namespace TradeGameCRAPI.Contexts
{
    public class AppDbContext: BaseAppDbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { }
    }
}
