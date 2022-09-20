namespace CleverWashWebApiTestTask.Model
{
	public class SalesData
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int ProductQuantity { get; set; }
		public double ProductIdAmount { get; set; }

		public virtual Sale Sale { get; set; }
	}
}