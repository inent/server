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
	public class DevicesController : ControllerBase
	{
		private readonly DeviceContext _context;

		public DevicesController(DeviceContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<Device>>> alllist()
		{
			return await _context.Devices.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<Device>>> list(Device req)
		{
			return await _context.Devices.Where(a => a.productid == req.productid).ToListAsync();
		}

		//[HttpPost]
		//private async Task<ActionResult<Device>> GetDevice(int id)
		//{
		//	var device = await _context.Devices.FindAsync(id);

		//	if (device == null)
		//	{
		//		return NotFound();
		//	}

		//	return device;
		//}

		//[HttpPost]
		//public async Task<ActionResult<IEnumerable<Device>>> AllList()
		//{
		//	var devices = await _context.Devices.ToListAsync();
		//	var deviceAttr = await _context.Attribs.ToListAsync();

		//	List<Object> arr = new List<Object>();

		//	foreach (Device bufdev in devices)
		//	{
		//		var listAttr = await _context.Attribs.Where(a => a.productid == bufdev.productid).ToListAsync();
		//		var devdto = new
		//		{
		//			bufdev.id,
		//			bufdev.productid,
		//			bufdev.name,
		//			bufdev.control,
		//			bufdev.macaddr,
		//			bufdev.addr1,
		//			bufdev.addr2,
		//			bufdev.lati,
		//			bufdev.longi,
		//			bufdev.status,
		//			bufdev.memo,
		//			bufdev.firmware,
		//			listAttr
		//		};
		//		arr.Add(devdto);
		//	}

		//	return Ok(arr);
		//}

		[HttpPost]
		public async Task<IActionResult> info(Device req)
		{

			var dev = await _context.Devices.Where(a => a.id == req.id).FirstOrDefaultAsync();
			if (dev == null)
			{
				return NotFound();
			}

			dynamic res = await getInfo(dev);

			return Ok(res);
		}

		[HttpPost]
		//public async Task<IActionResult> update(Device req)
		//{
		//	var bufdev = await _context.Devices.Where(a => a.id == req.id).FirstOrDefaultAsync();

		//	if (bufdev == null)
		//	{
		//		return NotFound();
		//	}

		//	bufdev.productid = req.productid;
		//	bufdev.name = req.name;
		//	bufdev.control = req.control;
		//	bufdev.macaddr = req.macaddr;
		//	bufdev.addr = req.addr;
		//	bufdev.geocode = req.geocode;
		//	bufdev.lati = req.lati;
		//	bufdev.longi = req.longi;
		//	bufdev.status = req.status;
		//	bufdev.memo = req.memo;
		//	bufdev.firmware = req.firmware;
		//	bufdev.serverip = req.serverip;
		//	bufdev.serverport = req.serverport;
		//	//bufdev.measect = req.measect;
		//	//bufdev.meacycle = req.meacycle;
		//	//bufdev.flushsect = req.flushsect;
		//	//bufdev.restsect = req.restsect;

		//	_context.Entry(bufdev).State = EntityState.Modified;

		//	try
		//	{
		//		await _context.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!DeviceExists(req.id))
		//		{
		//			return NotFound();
		//		}
		//		else
		//		{
		//			throw;
		//		}
		//	}

		//	return Ok(new { result = "update success" });
		//}

		public async Task<IActionResult> update(ReqDevice req)
		{
			var bufdev = await _context.Devices.Where(a => a.id == req.id).FirstOrDefaultAsync();

			if (bufdev == null)
			{
				return NotFound();
			}

			var dev = new Device();

			switch (req.company)
			{
				case "insys":
					var idev = new IDevice();

					pickIDevice(req, ref dev, ref idev);

					_context.Entry(bufdev).CurrentValues.SetValues(dev);

					var bufidev = await _context.IDevices.Where(a => a.id == req.id).FirstOrDefaultAsync();

					if (bufidev == null)
					{
						_context.IDevices.Add(idev);
					}
					else
					{
						_context.Entry(bufidev).CurrentValues.SetValues(idev);
					}
					break;

				case "jtron":
					var jdev = new JDevice();

					pickJDevice(req, ref dev, ref jdev);

					_context.Entry(bufdev).CurrentValues.SetValues(dev);

					var bufjdev = await _context.JDevices.Where(a => a.id == req.id).FirstOrDefaultAsync();

					if (bufjdev == null)
					{
						_context.JDevices.Add(jdev);
					}
					else
					{
						_context.Entry(bufjdev).CurrentValues.SetValues(jdev);
					}
					break;
				default:
					return BadRequest();
			}


			await _context.SaveChangesAsync();

			return Ok(new { result = "update success" });
		}

		[HttpPost]
		//public async Task<ActionResult<Device>> register(Device req)
		//{
		//	var bufdev = await _context.Devices.Where(a => a.id == req.id).FirstOrDefaultAsync();

		//	if (bufdev != null)
		//	{
		//		return Ok(new { result = "already exist" });
		//	}

		//	_context.Devices.Add(req);
		//	await _context.SaveChangesAsync();

		//	return Ok(new { result = "create" });
		//}

		public async Task<ActionResult> register(ReqDevice req)
		{
			var bufdev = await _context.Devices.Where(a => a.id == req.id).FirstOrDefaultAsync();

			if (bufdev != null)
			{
				return Ok(new { result = "already exist" });
			}

			var dev = new Device();

			switch (req.company)
			{
				case "insys":
					var idev = new IDevice();

					pickIDevice(req, ref dev, ref idev);

					_context.Devices.Add(dev);
					_context.IDevices.Add(idev);
					break;

				case "jtron":
					var jdev = new JDevice();

					pickJDevice(req, ref dev, ref jdev);

					_context.Devices.Add(dev);
					_context.JDevices.Add(jdev);

					break;

				default:
					return BadRequest();
			}


			await _context.SaveChangesAsync();

			return Ok(new { result = "create" });
		}



		[HttpPost]
		public async Task<ActionResult<Device>> remove(Device req)
		{
			var device = await _context.Devices.FindAsync(req.id);
			if (device == null)
			{
				return NotFound();
			}

			_context.Devices.Remove(device);

			var idev = await _context.IDevices.FindAsync(req.id);
			if (idev != null)
			{
				_context.IDevices.Remove(idev);
			}

			var jdev = await _context.JDevices.FindAsync(req.id);
			if (jdev != null)
			{
				_context.JDevices.Remove(jdev);
			}

			await _context.SaveChangesAsync();

			return Ok(new { result = "removed" });
		}

		private bool DeviceExists(string id)
		{
			return _context.Devices.Any(e => e.id == id);
		}

		private async Task<dynamic> getInfo(Device dev)
		{
			dynamic res = dev;

			var idev = await _context.IDevices.Where(a => a.id == dev.id).FirstOrDefaultAsync();
			if (idev != null)
			{
				res = new
				{
					dev.id,
					dev.productid,
					company = "insys",
					dev.name,
					dev.macaddr,
					dev.addr,
					dev.depart,
					dev.geocode,
					dev.lati,
					dev.longi,
					dev.firmware,
					dev.serverip,
					dev.serverport,
					dev.memo,
					dev.control,
					dev.status,
					dev.on_nh3,
					dev.on_h2s,
					dev.on_odor,
					dev.on_voc,
					dev.on_indol,
					dev.on_temp,
					dev.on_humi,
					dev.on_sen1,
					dev.on_sen2,
					dev.on_sen3,

					idev.measect,
					idev.meacycle,
					idev.flushsect,
					idev.restsect,
					idev.multiple,
					idev.ratio,
					idev.constant,
					idev.resolution,
					idev.deci
				};
			}

			var jdev = await _context.JDevices.Where(a => a.id == dev.id).FirstOrDefaultAsync();
			if (jdev != null)
			{
				res = new
				{
					dev.id,
					dev.productid,
					company = "jtron",
					dev.name,
					dev.macaddr,
					dev.addr,
					dev.depart,
					dev.geocode,
					dev.lati,
					dev.longi,
					dev.firmware,
					dev.serverip,
					dev.serverport,
					dev.memo,
					dev.control,
					dev.status,
					dev.on_nh3,
					dev.on_h2s,
					dev.on_odor,
					dev.on_voc,
					dev.on_indol,
					dev.on_temp,
					dev.on_humi,
					dev.on_sen1,
					dev.on_sen2,
					dev.on_sen3,

					jdev.rex_nh3,
					jdev.rex_h2s,
					jdev.rex_odor,
					jdev.rex_voc,
					jdev.rex_ou,
					jdev.min1,
					jdev.min2,
					jdev.min3,
					jdev.min4,
					jdev.min5,
					jdev.rsvtime,
					jdev.rsvproc,
					jdev.odorlev,
					jdev.autoproc
				};

			}

			return res;
		}

		private void pickIDevice(ReqDevice req, ref Device dev, ref IDevice idev)
		{
			dev = new Device
			{
				id = req.id,
				productid = req.productid,
				name = req.name,
				macaddr = req.macaddr,
				addr = req.addr,
				depart = req.depart,
				geocode = req.geocode,
				lati = req.lati,
				longi = req.longi,
				firmware = req.firmware,
				serverip = req.serverip,
				serverport = req.serverport,
				memo = req.memo,
				control = req.control,
				status = req.status,
				on_nh3 = req.on_nh3,
				on_h2s = req.on_h2s,
				on_odor = req.on_odor,
				on_voc = req.on_voc,
				on_indol = req.on_indol,
				on_temp = req.on_temp,
				on_humi = req.on_humi,
				on_sen1 = req.on_sen1,
				on_sen2 = req.on_sen2,
				on_sen3 = req.on_sen3
			};

			idev = new IDevice
			{
				id = req.id,
				measect = req.measect,
				meacycle = req.meacycle,
				flushsect = req.flushsect,
				restsect = req.restsect,
				multiple = req.multiple,
				ratio = req.ratio,
				constant = req.constant,
				resolution = req.resolution,
				deci = req.deci
			};

		}

		private void pickJDevice(ReqDevice req, ref Device dev, ref JDevice jdev)
		{
			dev = new Device
			{
				id = req.id,
				productid = req.productid,
				name = req.name,
				macaddr = req.macaddr,
				addr = req.addr,
				depart = req.depart,
				geocode = req.geocode,
				lati = req.lati,
				longi = req.longi,
				firmware = req.firmware,
				serverip = req.serverip,
				serverport = req.serverport,
				memo = req.memo,
				control = req.control,
				status = req.status,
				on_nh3 = req.on_nh3,
				on_h2s = req.on_h2s,
				on_odor = req.on_odor,
				on_voc = req.on_voc,
				on_indol = req.on_indol,
				on_temp = req.on_temp,
				on_humi = req.on_humi,
				on_sen1 = req.on_sen1,
				on_sen2 = req.on_sen2,
				on_sen3 = req.on_sen3
			};

			jdev = new JDevice
			{
				id = req.id,
				rex_nh3 = req.rex_nh3,
				rex_h2s = req.rex_h2s,
				rex_odor = req.rex_odor,
				rex_voc = req.rex_voc,
				rex_ou = req.rex_ou,
				min1 = req.min1,
				min2 = req.min2,
				min3 = req.min3,
				min4 = req.min4,
				min5 = req.min5,
				rsvtime = req.rsvtime,
				rsvproc = req.rsvproc,
				odorlev = req.odorlev,
				autoproc = req.autoproc
			};

		}

	}
}
