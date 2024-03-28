using Inventario.Domain.AggregatesRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;

namespace Inventario.Infrastructure.ESEntityFramework
{
    [ExcludeFromCodeCoverage]
	public class DbContextInventario : DbContext
	{
		public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Articulo> Articulo { get; set; }
		public DbContextInventario(DbContextOptions<DbContextInventario> options)  : base(options)
		{
		}

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            IDbContextTransaction dbContextTransaction = null;

            if (Database.IsSqlServer() && Database.CurrentTransaction == null)
                dbContextTransaction = Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

            await SaveChangesAsync(cancellationToken);
            dbContextTransaction?.Commit();

            return true;
        }
    }
}
