using System.Collections;
using CleverWashWebApiTestTask.Model;
using Microsoft.EntityFrameworkCore;

namespace CleverWashWebApiTestTask.Persistance
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(
			DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Sale> Sales { get; set; }
		public DbSet<Buyer> Buyers { get; set; }
		public DbSet<SalesPoint> SalesPoints { get; set; }
		public DbSet<ProvidedProducts> ProvidedProducts { get; set; }
		public DbSet<SalesData> SalesData { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>(p =>
			{
				p.HasKey(p => p.Id);
				p.Property(p => p.Name);
				p.Property(p => p.Price).HasColumnType("money");
			});

			modelBuilder.Entity<Buyer>(b =>
			{
				b.HasKey(b => b.Id);
				b.Property(b => b.Name).IsRequired();
				b.Ignore(b => b.SalesIds);
			});

			modelBuilder.Entity<Sale>(s =>
			{
				s.HasKey(s => s.Id);
				s.Property(s => s.Date);
				s.Property(s => s.Time);
				s.HasOne(s => s.SalesPoint)
					.WithMany(sp => sp.Sales)
					.HasForeignKey(s => s.SalesPointId);
				s.HasOne(s => s.Buyer)
					.WithMany(b => b.Sales)
					.HasForeignKey(b => b.BuyerId);
				s.HasMany(b => b.SalesData)
					.WithOne(sd => sd.Sale);
				s.Property(s => s.TotalAmount);
			});

			modelBuilder.Entity<SalesPoint>(sp =>
			{
				sp.HasKey(sp => sp.Id);
				sp.Property(sp => sp.Name);
				sp.HasMany(sp => sp.ProvidedProducts)
					.WithOne(pp => pp.SalesPoint);
			});
		}
	}
}