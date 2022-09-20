using System.Collections.Generic;
using System.Linq;

namespace CleverWashWebApiTestTask.Model
{
	public class SalesPoint
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<ProvidedProduct> ProvidedProducts { get; set; } = new List<ProvidedProduct>();

		public bool IsAvailable(int productId, int quantity)
		{
			return ProvidedProducts.Any(pp => pp.ProductId == productId && pp.ProductQuantity >= quantity);
		}
	}
}