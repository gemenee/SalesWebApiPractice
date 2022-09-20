using System.Collections.Generic;

namespace CleverWashWebApiTestTask.Model
{
	public class BuyResult
	{
		public SaleResult SaleResult { get; set; }
		public List<Product> ProductsOutOfStock { get; set; } = new List<Product>();
	}

	public enum SaleResult
	{
		SaleComplete,
		SaleFailed,
		SalePartiallyIncomplete
	}
}