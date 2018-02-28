using CQRSExample.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSExample.Infrastructure.Data
{
    public interface IAppDataContext {
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }
        Guid TenantId { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken, string username);
    }

    public class AppDataContext:DbContext, IAppDataContext
    {
        public AppDataContext()
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public Guid TenantId { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken, string username)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
