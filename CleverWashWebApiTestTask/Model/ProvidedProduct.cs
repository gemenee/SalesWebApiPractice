using System.Collections.Generic;

namespace CleverWashWebApiTestTask.Model
{
	public class ProvidedProducts
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int ProductQuantity { get; set; }
		public SalesPoint SalesPoint { get; set; }
	}
}