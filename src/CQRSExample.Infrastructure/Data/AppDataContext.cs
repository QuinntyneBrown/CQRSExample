using CQRSExample.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CQRSExample.Infrastructure.Data
{
    public interface IAppDataContext {
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }
        Guid TenantId { get; set; }
    }

    public class AppDataContext: IAppDataContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public Guid TenantId { get; set; }
    }
}
