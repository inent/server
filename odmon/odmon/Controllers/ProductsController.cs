using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odmon.Models;

namespace odmon.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly DeviceContext _context;

		public ProductsController(DeviceContext context)
		{
			_context = context;
		}

		// POST: api/Products
		[HttpPost]
		public async Task<ActionResult<IEnumerable<Product>>> list()
		{
			return await _context.Products.ToListAsync();
		}

		[HttpPost]
		public async Task<IActionResult> info(Product req)
		{
			if (req.id == null)
			{
				return BadRequest();
			}

			var buf = await _context.Products.Where(a => a.id == req.id).FirstOrDefaultAsync();

			if (buf == null)
			{
				return NotFound();
			}

			return Ok(buf);
		}

		[HttpPost]
		public async Task<IActionResult> update(Product req)
		{
			if (req.id == null)
			{
				return BadRequest();
			}

			var buf = await _context.Products.Where(a => a.id == req.id).FirstOrDefaultAsync();

			if (buf == null)
			{
				return NotFound();
			}

			buf.name = req.name;
			buf.company = req.company;
			buf.regist = req.regist;
			buf.release = req.release;
			buf.purpose = req.purpose;
			buf.note = req.note;

			_context.Entry(buf).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok(new { result = "update success" });
		}

		[HttpPost]
		public async Task<ActionResult<Product>> register(Product req)
		{
			_context.Products.Add(req);

			await _context.SaveChangesAsync();

			return Ok(new { result = "create" });
		}

		[HttpPost]
		public async Task<ActionResult<Product>> remove(Product req)
		{
			var buf = await _context.Products.FindAsync(req.id);
			if (buf == null)
			{
				return NotFound();
			}

			_context.Products.Remove(buf);
			await _context.SaveChangesAsync();

			return Ok(new { result = "removed" });
		}

	}
}
