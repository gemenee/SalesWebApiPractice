using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleverWashWebApiTestTask.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CleverWashWebApiTestTask.Model
{
	public class BuyService
	{
		private readonly AppDbContext _appDbContext;

		public BuyService(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<BuyResult> BuyAsync(Order order)
		{
			var buyer = (await _appDbContext.Buyers.FirstOrDefaultAsync(b => b.Id == order.BuyerId));
			var salesPoint = (await _appDbContext.SalesPoints.FirstOrDefaultAsync(sp => sp.Id == order.SalesPointId));
			var salesDataList = new List<SalesData>();

			var sale = new Sale()
			{
				SalesPointId = order.SalesPointId,
				BuyerId = order.BuyerId,
				TotalAmount = 0
			};

			var buyResult = new BuyResult() { SaleResult = SaleResult.SaleComplete };

			foreach (var position in order.Positions)
			{
				var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == position.productId);
				if (!salesPoint.IsAvailable(position.productId, position.quantity))
				{
					buyResult.SaleResult = SaleResult.SalePartiallyIncomplete;
					buyResult.ProductsOutOfStock.Add(product);
					continue;
				}

				var providedProduct = salesPoint.ProvidedProducts.FirstOrDefault(pp => pp.ProductId == position.productId);
				providedProduct.ProductQuantity -= position.quantity;
				var productAmount = product.Price * position.quantity;

				salesDataList.Add(new SalesData()
				{
					ProductId = position.productId,
					ProductQuantity = position.quantity,
					ProductIdAmount = productAmount
				});

				sale.TotalAmount += productAmount;
			}

			sale.SalesData = salesDataList;
			if (!sale.SalesData.Any()) buyResult.SaleResult = SaleResult.SaleFailed;

			if (sale.TotalAmount > 0)
			{
				var dateTime = DateTime.UtcNow;
				sale.Date = dateTime.ToShortDateString();
				sale.Time = dateTime.ToLongTimeString();
				await _appDbContext.Sales.AddAsync(sale);
				await _appDbContext.SaveChangesAsync();
			}
			return buyResult;
		}
	}
}