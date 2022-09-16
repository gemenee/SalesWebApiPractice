using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CleverWashWebApiTestTask.Model
{
	public class Buyer
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public IEnumerable<int> SalesIds
		{
			get => Sales.Select(s => s.Id);
		}

		public IEnumerable<Sale> Sales { get; set; } = Enumerable.Empty<Sale>();
	}
}