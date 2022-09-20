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
			Database.EnsureCreated();
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Sale> Sales { get; set; }
		public DbSet<Buyer> Buyers { get; set; }
		public DbSet<SalesPoint> SalesPoints { get; set; }
		public DbSet<SalesData> SalesData { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>(p =>
			{
				p.HasKey(p => p.Id);
				p.Property(p => p.Name);
				p.Property(p => p.Price).HasColumnType("money");
			});

			modelBuilder.Entity<Product>().HasData(
				new Product() { Id = 1, Name = "Product1", Price = 100.1 },
				new Product() { Id = 2, Name = "Product2", Price = 200.1 },
				new Product() { Id = 3, Name = "Product3", Price = 50.1 },
				new Product() { Id = 4, Name = "Product4", Price = 400.1 });

			modelBuilder.Entity<Buyer>(b =>
			{
				b.HasKey(b => b.Id);
				b.Property(b => b.Name).IsRequired();
				b.Ignore(b => b.SalesIds);
			});

			modelBuilder.Entity<Buyer>().HasData(
				new Buyer() { Id = 1, Name = "Buyer1" },
				new Buyer() { Id = 2, Name = "Buyer2" }
				);

			modelBuilder.Entity<Sale>(s =>
			{
				s.HasKey(s => s.Id);
				s.Property(s => s.Date);
				s.Property(s => s.Time);
				s.HasOne(s => s.Buyer)
					.WithMany(b => b.Sales)
					.HasForeignKey(s => s.BuyerId);
				s.HasMany(s => s.SalesData)
					.WithOne(sd => sd.Sale);
				s.Property(s => s.TotalAmount);
			});

			var providedProduct1 = new { Id = 1, ProductId = 1, ProductQuantity = 2, SalesPointId = 1 };
			var providedProduct2 = new { Id = 2, ProductId = 2, ProductQuantity = 2, SalesPointId = 2 };
			var providedProduct3 = new { Id = 3, ProductId = 3, ProductQuantity = 2, SalesPointId = 1 };
			var providedProduct4 = new { Id = 4, ProductId = 4, ProductQuantity = 2, SalesPointId = 2 };

			modelBuilder.Entity<SalesPoint>(sp =>
			{
				sp.HasKey(sp => sp.Id);
				sp.Property(sp => sp.Name);
				sp.OwnsMany(sp => sp.ProvidedProducts).HasData(
					providedProduct1,
					providedProduct2,
					providedProduct3,
					providedProduct4);
			});

			modelBuilder.Entity<SalesPoint>().HasData(
				new SalesPoint()
				{
					Id = 1,
					Name = "SalesPoint1",
				},
				new SalesPoint()
				{
					Id = 2,
					Name = "SalesPoint2",
				});
		}
	}
}