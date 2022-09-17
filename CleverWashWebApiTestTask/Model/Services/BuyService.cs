using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleverWashWebApiTestTask.Persistance;

namespace CleverWashWebApiTestTask.Model
{
	public class BuyService
	{
		private readonly AppDbContext _appDbContext;

		public BuyService(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task BuyAsync(Order order)
		{
			var buyer = (await _appDbContext.Buyers.FindAsync(order.Buyer));
			var salesPoint = (await _appDbContext.SalesPoints.FindAsync(order.SalesPoint));
			var salesDataList = new List<SalesData>();
			var sale = new Sale()
			{
				SalesPointId = order.SalesPoint.Id,
				BuyerId = order.Buyer.Id,
				TotalAmount = 0
			};
			foreach (var position in order.ProductsQuantity)
			{
				if (!salesPoint.IsAvailable(position.product, position.quantity)) continue;

				var providedProduct = salesPoint.ProvidedProducts.FirstOrDefault(pp => pp.ProductId == position.product.Id);
				providedProduct.ProductQuantity -= position.quantity;
				var productAmount = position.product.Price * position.quantity;
				salesDataList.Add(new SalesData()
				{
					ProductId = position.product.Id,
					ProductQuantity = position.quantity,
					ProductIdAmount = productAmount
				});
				sale.TotalAmount += productAmount;
			}
			if (sale.TotalAmount > 0)
			{
				await _appDbContext.Sales.AddAsync(sale);
				await _appDbContext.SaveChangesAsync();
			}
		}
	}
}