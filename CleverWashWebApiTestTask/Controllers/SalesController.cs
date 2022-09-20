using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleverWashWebApiTestTask.Model;
using CleverWashWebApiTestTask.Persistance;

namespace CleverWashWebApiTestTask.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SalesController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly BuyService _buyService;

		public SalesController(AppDbContext context, BuyService buyService)
		{
			_context = context;
			_buyService = buyService;
		}

		// GET: api/Sales
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
		{
			return await _context.Sales.ToListAsync();
		}

		// GET: api/Sales/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Sale>> GetSale(int id)
		{
			var sale = await _context.Sales.FindAsync(id);

			if (sale == null)
			{
				return NotFound();
			}

			return sale;
		}

		// PUT: api/Sales/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutSale(int id, Sale sale)
		{
			if (id != sale.Id)
			{
				return BadRequest();
			}

			_context.Entry(sale).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!SaleExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Sales
		[HttpPost]
		public async Task<ActionResult<Sale>> PostSale(Sale sale)
		{
			_context.Sales.Add(sale);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
		}

		// DELETE: api/Sales/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSale(int id)
		{
			var sale = await _context.Sales.FindAsync(id);
			if (sale == null)
			{
				return NotFound();
			}

			_context.Sales.Remove(sale);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		/// <summary>
		/// Place order to buy products
		/// </summary>
		/// <param name="order"></param>
		/// <returns>BuyResult</returns>
		//POST: api/Sales/buy
		[HttpPost("buy")]
		public async Task<IActionResult> Buy(Order order)
		{
			var result = await _buyService.BuyAsync(order);

			if (result.SaleResult == SaleResult.SaleComplete)
				return Ok(result);
			else
				return StatusCode(409, result);
		}

		private bool SaleExists(int id)
		{
			return _context.Sales.Any(e => e.Id == id);
		}
	}
}