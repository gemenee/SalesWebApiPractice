using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CleverWashWebApiTestTask.Model
{
	public class Order
	{
		[JsonInclude]
		[Required]
		public int BuyerId { get; set; }

		[JsonInclude]
		[Required]
		public int SalesPointId { get; set; }

		[JsonInclude]
		[Required]
		public List<(int productId, int quantity)> Positions { get; set; }
	}
}