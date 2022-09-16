using System;
using System.Collections.Generic;
using System.Linq;

namespace CleverWashWebApiTestTask.Model
{
	public class Sale
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public DateTime Time { get; set; }
		public int SalesPointId { get; set; }
		public int BuyerId { get; set; }
		public IEnumerable<SalesData> SalesData { get; set; }
		public double TotalAmount { get => SalesData.Sum(sd => sd.ProductIdAmount); }
		public Buyer Buyer { get; set; }
		public SalesPoint SalesPoint { get; set; }
	}
}