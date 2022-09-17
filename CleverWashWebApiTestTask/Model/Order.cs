using System.Collections.Generic;

namespace CleverWashWebApiTestTask.Model
{
	public class Order
	{
		public Buyer Buyer { get; set; }
		public SalesPoint SalesPoint { get; set; }
		public List<(Product product, int quantity)> ProductsQuantity { get; set; }
	}
}