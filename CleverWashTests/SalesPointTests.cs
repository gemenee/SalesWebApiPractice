using System.Collections.Generic;
using CleverWashWebApiTestTask.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleverWashTests
{
	[TestClass]
	public class SalesPointTests
	{
		private readonly Product _availableProduct = new() { Id = 0, Name = "A", Price = 1 };
		private readonly Product _unavailableProduct = new() { Id = 1, Name = "B", Price = 2 };

		private List<ProvidedProduct> _providedProducts = new();
		private SalesPoint _salesPoint = new();

		[TestInitialize]
		public void TestInitialize()
		{
			_providedProducts = new()
			{
				new ProvidedProduct() { ProductId = _availableProduct.Id, ProductQuantity = 1 }
			};

			_salesPoint.ProvidedProducts = _providedProducts;
		}

		[TestMethod]
		public void IsAvailable_NoSuchProduct_False()
		{
			Assert.IsFalse(_salesPoint.IsAvailable(_unavailableProduct.Id, 1));
		}

		[TestMethod]
		public void IsAvailable_InsufficientQuantityOfAvailableProduct_False()
		{
			Assert.IsFalse(_salesPoint.IsAvailable(_availableProduct.Id, 2));
		}

		[TestMethod]
		public void IsAvailable_AvailableProduct_True()
		{
			Assert.IsTrue(_salesPoint.IsAvailable(_availableProduct.Id, 1));
		}
	}
}