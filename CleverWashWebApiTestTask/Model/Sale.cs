using System.Collections.Generic;

namespace CleverWashWebApiTestTask.Model
{
	public class Sale
	{
		public int Id { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public int SalesPointId { get; set; }
		public int? BuyerId { get; set; }
		public virtual ICollection<SalesData> SalesData { get; set; }
		public double TotalAmount { get; set; }
		public virtual Buyer Buyer { get; set; }
		public virtual SalesPoint SalesPoint { get; set; }
	}
}