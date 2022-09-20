using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CleverWashWebApiTestTask.Model
{
	[Owned]
	public class ProvidedProduct
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int ProductQuantity { get; set; }

		[JsonIgnore]
		public SalesPoint SalesPoint { get; set; }
	}
}