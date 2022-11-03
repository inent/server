using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odmon.Helpers;
using odmon.Models;
using odmon.Services;

namespace odmon.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AlertsController : ControllerBase
	{
		private readonly DeviceContext _context;

		public AlertsController(DeviceContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<Alert>>> list()
		{
			return await _context.Alerts.ToListAsync();
		}

		[HttpPost]
		public async Task<IActionResult> info(Alert req)
		{
			if (req.id < 1)
			{
				return BadRequest();
			}

			var buf = await _context.Alerts.Where(a => a.id == req.id).FirstOrDefaultAsync();

			if (buf == null)
			{
				return NotFound();
			}

			return Ok(buf);
		}

		[HttpPost]
		public async Task<IActionResult> update(Alert req)
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
				if (!DeviceExists(req.id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return Ok(new { result = "update success" });
		}

		[HttpPost]
		public async Task<ActionResult> register(Alert req)
		{
			req.id = 0;
			_context.Alerts.Add(req);
			await _context.SaveChangesAsync();

			return Ok(new { result = "create" });
		}

		[HttpPost]
		public async Task<ActionResult<Alert>> remove(Alert req)
		{
			var buf = await _context.Alerts.FindAsync(req.id);
			if (buf == null)
			{
				return NotFound();
			}

			_context.Alerts.Remove(buf);

			//var arr = await _context.AlertUsers.Where(a => a.alertid == req.id).ToListAsync();
			//_context.AlertUsers.RemoveRange(arr);

			await _context.SaveChangesAsync();

			return Ok(new { result = "removed" });
		}

		private bool DeviceExists(int id)
		{
			return _context.Alerts.Any(e => e.id == id);
		}

		[Authorize]
		[HttpPost]
		public async Task<ActionResult> todoList()
		{
			//var alerts = await _context.AlertUsers
			//	.Where(a => a.userid == User.Identity.Name && a.type == "web")
			//	.Join(
			//		_context.AlertLists.GroupBy(a=> a.alertid).Max()
			//		,
			//		u => u.alertid,
			//		a => a.alertid,
			//		(u, a) => new { u.userid, u.type, a }
			//		)
			//	.Where(a => a.userid == User.Identity.Name && a.type == "web")
			//	.OrderByDescending(a => a.a.times)
			//	.ToListAsync();

			var arr = await _context.AlertLists
				.Where(a => a.userid == User.Identity.Name && a.times > DateTime.Now.AddSeconds(-10))
				.OrderByDescending(a => a.times)
				.ToListAsync();

			return Ok(arr);
		}

		//[Authorize]
		//[HttpPost]
		//public async Task<ActionResult> todoList()
		//{
		//	var alerts = await _context.AlertLists
		//		.Where(a => a.userid == User.Identity.Name && a.type == "web" && a.confirm == null)
		//		.Select(a => new { a.idx, a.content })
		//		.ToListAsync();

		//	return Ok(alerts);
		//}

		//[Authorize]
		//[HttpPost]
		//public async Task<ActionResult> Confirm(AlertList req)
		//{
		//	var alert = await _context.AlertLists.Where(a => a.userid == User.Identity.Name && a.id == req.id).FirstOrDefaultAsync();

		//	if (alert == null)
		//	{
		//		return BadRequest();
		//	}

		//	alert.confirm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

		//	_workLogService.Add(new WorkLog
		//	{
		//		userid = User.Identity.Name,
		//		part = "Alert_Confirm",
		//		level = "Normal",
		//		content = "success"
		//	});

		//	return Ok(new { result = "confirmed" });
		//}

		public async Task<IActionResult> History(Paging paging)
		{
			if (String.IsNullOrEmpty(User.Identity.Name))
			{
				return BadRequest("Login First");
			}

			var records = from r in _context.AlertLists
						  select r;

			if (!String.IsNullOrEmpty(paging.search))
			{
				records = records.Where(r => r.status.Contains(paging.search)
										|| r.type.Contains(paging.search)
										|| r.name.Contains(paging.search)
										|| r.kind.Contains(paging.search)
										|| r.content.Contains(paging.search)
									   );
			}

			if (!String.IsNullOrEmpty(paging.fromDate) &&
				!String.IsNullOrEmpty(paging.toDate))
			{
				var fromDT = Convert.ToDateTime(paging.fromDate);
				var toDT = Convert.ToDateTime(paging.toDate);

				records = records.Where(w => w.times >= fromDT && w.times <= toDT);
			}

			bool isDesc = false;
			if (paging.sortOrder == "desc")
				isDesc = true;

			switch (paging.sortField)
			{
				case "status":
					if (isDesc)
						records = records.OrderByDescending(w => w.status);
					else
						records = records.OrderBy(w => w.status);
					break;
				case "type":
					if (isDesc)
						records = records.OrderByDescending(w => w.type);
					else
						records = records.OrderBy(w => w.type);
					break;
				case "name":
					if (isDesc)
						records = records.OrderByDescending(w => w.name);
					else
						records = records.OrderBy(w => w.name);
					break;
				case "kind":
					if (isDesc)
						records = records.OrderByDescending(w => w.kind);
					else
						records = records.OrderBy(w => w.kind);
					break;
				case "content":
					if (isDesc)
						records = records.OrderByDescending(w => w.content);
					else
						records = records.OrderBy(w => w.content);
					break;
				case "times":
					if (isDesc)
						records = records.OrderByDescending(w => w.times);
					else
						records = records.OrderBy(w => w.times);
					break;
				default:
					if (isDesc)
						records = records.OrderByDescending(w => w.id);
					else
						records = records.OrderBy(w => w.id);
					break;
			}

			var totalSize = records.Count();

			return Ok(new
			{
				totalSize,
				PagedList = await PaginatedList<AlertList>.CreateAsync(records.AsNoTracking(), paging.page, paging.sizePerPage)
			});
		}

		[HttpPost]
		public async Task<ActionResult> userlist()
		{
			return Ok(await _context.AlertUsers.ToListAsync());
		}

		[HttpPost]
		public async Task<ActionResult> useradd(AlertUser req)
		{
			req.id = 0;
			_context.AlertUsers.Add(req);
			await _context.SaveChangesAsync();

			return Ok(new { result = "add" });
		}

		[HttpPost]
		public async Task<ActionResult> userdel(AlertUser req)
		{
			var buf = await _context.AlertUsers.FindAsync(req.id);
			if (buf == null)
			{
				return NotFound();
			}

			_context.AlertUsers.Remove(buf);
			await _context.SaveChangesAsync();

			return Ok(new { result = "removed" });

		}

	}
}
