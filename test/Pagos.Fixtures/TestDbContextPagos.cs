using Pagos.Infrastructure.ESEntityFramework;
using Microsoft.EntityFrameworkCore;


namespace Pagos.Fixtures;

	public class TestDbContextPagos : DbContextPagos 
	{
		public TestDbContextPagos(DbContextOptions<DbContextPagos> options) : base(options) 
		{ 
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}

