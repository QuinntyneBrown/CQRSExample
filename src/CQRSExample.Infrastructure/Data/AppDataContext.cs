using CQRSExample.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSExample.Infrastructure.Data
{
    public interface IAppDataContext {
        DbSet<Customer> Customers { get; set; }
        DbSet<Dashboard> Dashboards { get; set; }
        DbSet<DashboardTile> DashboardTiles { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Tile> Tiles { get; set; }
        DbSet<User> Users { get; set; }
        Guid TenantId { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken, string username);
    }

    public class AppDataContext:DbContext, IAppDataContext
    {
        public AppDataContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<DashboardTile> DashboardTiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tile> Tiles { get; set; }
        public Guid TenantId { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken, string username)
        {

            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries().Where(
                e => e.State == EntityState.Added && e.Metadata.GetProperties().Any(p => p.Name == "TenantId")))
            {
                item.CurrentValues["TenantId"] = TenantId;
            }

            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.CurrentValues["IsDeleted"] = true;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        private static IList<Type> _entityTypeCache = default(IList<Type>);
        private static IList<Type> GetEntityTypes()
        {
            if (_entityTypeCache != default(IList<Type>))
            {
                return _entityTypeCache.ToList();
            }

            _entityTypeCache = (from a in GetReferencingAssemblies()
                                from t in a.DefinedTypes
                                where t.BaseType == typeof(BaseModel)
                                select t.AsType()).ToList();

            return _entityTypeCache;
        }

        private static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies;
        }

        static readonly MethodInfo SetGlobalQueryMethod = typeof(AppDataContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DashboardTile>()
            .HasOne(dt => dt.Settings)
            .WithOne(dts => dts.DashboardTile)
            .HasForeignKey<DashboardTileSettings>(dts => dts.DashboardTileId);

            modelBuilder.Entity<CustomerDashboardTile>()
            .HasOne(dt => dt.Settings)
            .WithOne(dts => dts.DashboardTile)
            .HasForeignKey<CustomerDashboardTileSettings>(dts => dts.DashboardTileId);

            foreach (var type in GetEntityTypes())
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { modelBuilder });
            }
            base.OnModelCreating(modelBuilder);
        }

        public void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseModel
        {
            builder.Entity<T>()
                .HasQueryFilter(e => EF.Property<Guid>(e, "TenantId") == TenantId && !e.IsDeleted);
        }

    }
}
