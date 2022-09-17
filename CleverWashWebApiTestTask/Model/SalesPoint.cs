using System.Collections.Generic;
using System.Linq;

namespace CleverWashWebApiTestTask.Model
{
	public class SalesPoint
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<ProvidedProducts> ProvidedProducts { get; set; }
		public IEnumerable<Sale> Sales { get; set; }

		public bool IsAvailable(Product product, int quantity)
		{
			return ProvidedProducts.Any(pp => pp.ProductId == product.Id && pp.ProductQuantity >= quantity);
		}
	}
}