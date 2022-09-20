using System.Collections.Generic;

namespace CleverWashWebApiTestTask.Model
{
	public class Buyer
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<int> SalesIds { get; set; }

		public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
	}
}