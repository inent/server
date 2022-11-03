using System;
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
	public class AttribsController : ControllerBase
	{
		private readonly DeviceContext _context;

		public AttribsController(DeviceContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<Attrib>>> AllList()
		{
			return await _context.Attribs.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<Attrib>>> list(Attrib req)
		{
			return await _context.Attribs.Where(a => a.productid == req.productid).ToListAsync();
		}

		//[HttpPost]
		//public async Task<ActionResult<Attrib>> GetDeviceAttr(int id)
		//{
		//	var deviceAttr = await _context.Attribs.FindAsync(id);

		//	if (deviceAttr == null)
		//	{
		//		return NotFound();
		//	}

		//	return deviceAttr;
		//}

		[HttpPost]
		public async Task<ActionResult<Attrib>> info(Attrib req)
		{
			var deviceAttr = await _context.Attribs.FindAsync(req.id);

			if (deviceAttr == null)
			{
				return NotFound();
			}

			//dynamic buf = castType(deviceAttr);
			return Ok(deviceAttr);
		}

		[HttpPost]
		public async Task<IActionResult> update(Attrib req)
		{
			if (req.id < 1)
			{
				return BadRequest();
			}

			_context.Entry(req).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!DeviceAttrExists(req.id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return Ok(new { result = "update success"});
		}

		[HttpPost]
		public async Task<ActionResult<Attrib>> register(Attrib req)
		{
			req.id = 0;

			_context.Attribs.Add(req);
			await _context.SaveChangesAsync();

			return Ok(new { result = "create" } );
		}

		[HttpPost]
		public async Task<ActionResult<Attrib>> Remove(Attrib _buf)
		{
			var deviceAttr = await _context.Attribs.FindAsync(_buf.id);
			if (deviceAttr == null)
			{
				return NotFound();
			}

			_context.Attribs.Remove(deviceAttr);
			await _context.SaveChangesAsync();

			return Ok(new { result = "removed" });
		}

		private bool DeviceAttrExists(int id)
		{
			return _context.Attribs.Any(e => e.id == id);
		}


		//private dynamic castType (Attrib attr)
		//{
		//	switch (attr.type)
		//	{
		//		case "Sensor":
		//			return (new
		//			{
		//				attr.id,
		//				attr.productid,
		//				attr.type,
		//				attr.alias,
		//				attr.name,
		//				attr.onoff,
		//				attr.label,
		//				attr.chemiunit,
		//				attr.threshold,
		//				attr.min,
		//				attr.max,
		//				attr.elecunit
		//			});
		//		case "Actuator":
		//			return (new
		//			{
		//				attr.id,
		//				attr.productid,
		//				attr.type,
		//				attr.alias,
		//				attr.name,
		//				attr.label,
		//				attr.chemiunit,
		//				attr.threshold,
		//				attr.min,
		//				attr.max
		//			});
		//	}

		//	return "ERROR";
		//}
	}
}
