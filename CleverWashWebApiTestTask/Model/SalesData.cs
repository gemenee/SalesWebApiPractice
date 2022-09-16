namespace CleverWashWebApiTestTask.Model
{
	public class SalesData
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int ProductQuantity { get; set; }
		public int ProductIdAmount { get; set; }

		public Sale Sale { get; set; }
	}
}