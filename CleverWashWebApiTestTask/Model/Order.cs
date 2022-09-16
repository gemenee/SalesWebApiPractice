using System.Collections.Generic;

namespace CleverWashWebApiTestTask.Model
{
	public class Order
	{
		public int Id { get; set; }
		public Dictionary<Product, int> Products { get; set; }
	}
}