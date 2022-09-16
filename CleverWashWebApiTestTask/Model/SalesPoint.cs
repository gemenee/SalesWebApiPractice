using System.Collections.Generic;

namespace CleverWashWebApiTestTask.Model
{
	public class SalesPoint
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<ProvidedProducts> ProvidedProducts { get; set; }
		public IEnumerable<Sale> Sales { get; set; }
	}
}