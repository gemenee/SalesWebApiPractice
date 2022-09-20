using System.Collections.Generic;

namespace CleverWashWebApiTestTask.Model
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public virtual ICollection<SalesData> SalesData { get; set; }
	}
}