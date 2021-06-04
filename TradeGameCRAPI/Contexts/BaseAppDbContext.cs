using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TradeGameCRAPI.Entities;

namespace TradeGameCRAPI.Contexts
{
    public abstract class BaseAppDbContext : IdentityDbContext<User, Role, int>
    {
        public BaseAppDbContext(DbContextOptions options) : base(options) { }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSave();

            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));
        }

        private void OnBeforeSave()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            {
                                trackable.UpdatedAt = utcNow;

                                // mark property as "don't touch"
                                // we don't want to update on a Modify operation
                                entry.Property("CreatedAt").IsModified = false;

                                break;
                            }

                        case EntityState.Added:
                            {
                                trackable.CreatedAt = utcNow;
                                trackable.UpdatedAt = utcNow;

                                break;
                            }
                    }
                }
            }
        }
    }
}
