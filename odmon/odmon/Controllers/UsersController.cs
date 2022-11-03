using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odmon.Helpers;
using odmon.Models;
using odmon.Services;

namespace odmon.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class UsersController : ControllerBase
	{
		private readonly DeviceContext _context;
		private readonly IUserService _userService;
		private readonly IEmailService _emailService;
		private readonly IWebHostEnvironment _env;

		public UsersController(DeviceContext context, IUserService userService, IEmailService emailService, IWebHostEnvironment env)
		{
			_context = context;
			_userService = userService;
			_emailService = emailService;
			_env = env;
		}

		[AllowAnonymous]
		public ActionResult Login([FromBody] AuthenticateModel model)
		{
			var user = _userService.Authenticate(model.userid, model.userpw);

			if (user == null)
				return BadRequest(new { result = "Username or password is incorrect" });

			if (!String.IsNullOrEmpty(user.geocode))
			{
				RenewCoverage(user).Wait();
			}

			//_workLogService.Add(new WorkLog
			//{
			//	userid = model.userid,
			//	part = "Login",
			//	level = "Normal",
			//	content = "success"
			//});

			return Ok(new
			{
				user.userid,
				user.username,
				user.depart,
				user.position,
				user.email,
				user.phone,
				user.role,
				user.geocode,
				user.token,
				user.onweb,
				user.onmail,
				user.onsms,
				user.onpush
			});
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<User>>> UserList()
		{
			var users = await _context.Users.ToListAsync();
			List<Object> arr = new List<Object>();

			foreach (User person in users)
			{
				arr.Add(new
				{
					person.userid,
					person.username,
					person.depart,
					person.position,
					person.email,
					person.phone,
					person.role,
					person.geocode
				});
			}

			return Ok(arr);
		}

		public async Task<ActionResult<User>> Info(User person)
		{
			var user = await _context.Users.Where(a => a.userid == person.userid).FirstOrDefaultAsync();

			if (user == null)
			{
				return NotFound();
			}

			var isConfirm = user.userpw == "b4 confirm" ? false : true;

			return Ok(new
			{
				user.userid,
				user.username,
				user.depart,
				user.position,
				user.email,
				user.phone,
				user.role,
				user.geocode,
				user.onweb,
				user.onmail,
				user.onsms,
				user.onpush,
				isConfirm
			});
		}

		[HttpPost]
		public async Task<IActionResult> Update(User req)
		{
			if (String.IsNullOrEmpty(req.userpw) || String.IsNullOrEmpty(req.email))
			{
				return BadRequest();
			}

			var user = await _context.Users.Where(a => a.userid == User.Identity.Name).FirstOrDefaultAsync();
			if (user == null)
			{
				return NotFound();
			}

			//var workbuf = JsonSerializer.Serialize(new
			//{
			//	user.username,
			//	user.depart,
			//	user.position,
			//	user.email,
			//	user.phone,
			//	user.onweb,
			//	user.onmail,
			//	user.onsms,
			//	user.onpush
			//}, new JsonSerializerOptions
			//{
			//	Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			//});

			user.userpw = GenerateMySQLHash(req.userpw);
			user.username = req.username;
			user.depart = req.depart;
			user.position = req.position;
			user.email = req.email;
			user.phone = req.phone;
			user.onweb = req.onweb;
			user.onmail = req.onmail;
			user.onsms = req.onsms;
			user.onpush = req.onpush;

			_context.Entry(user).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserExists(user.userid))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			//_workLogService.Add(new WorkLog
			//{
			//	userid = User.Identity.Name,
			//	part = "Update",
			//	level = "Normal",
			//	content = workbuf
			//});


			return Ok(new { result = "changed." });
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult<User>> register(User user)
		{
			if (String.IsNullOrEmpty(user.userid) || String.IsNullOrEmpty(user.email))
			{
				return BadRequest();
			}

			if (UserExists(user.userid))
			{
				return BadRequest(new { result = user.userid + " is already registered." });
			}

			//user.userpw = RandomString(8);
			user.userpw = "b4 confirm";
			user.role = Role.User;

			try
			{
				_context.Users.Add(user);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				return BadRequest();
			}


			//_emailService.Send(
			//	from: user.email,
			//	to: "inent.dev@gmail.com",
			//	subject: "Odor Monitor 회원가입에 관한 건",
			//	html: $@"<h4>안녕하세요</h4><p>
			//			id : <strong>{user.userid}</strong><br>
			//			이름 : <strong>{user.username}</strong><br>
			//			email : <strong>{user.email}</strong><br>
			//			부서 : <strong>{user.depart}</strong><br>
			//			직책 : <strong>{user.position}</strong><br>
			//			핸드폰 : <strong>{user.phone}</strong><br>

			//			회원가입이 되었습니다.<br><a href=""http://175.208.89.113:4100/"">여기</a>에서 가입된 ID를 승인해주세요.</p>"
			//	);

			string bufform = System.IO.File.ReadAllText(System.IO.Path.Combine(_env.ContentRootPath, @"form/userregist.html"));

			_emailService.Send(
				from: user.email,
				to: "inent.dev@gmail.com",
				subject: "Odor Monitor 회원가입에 관한 건",
				html: String.Format(bufform, 
						user.userid,
						user.username,
						user.email,
						user.depart,
						user.position,
						user.phone)
				);

			//_workLogService.Add(new WorkLog
			//{
			//	userid = User.Identity.Name,
			//	part = "register",
			//	level = "Normal",
			//	content = JsonSerializer.Serialize(user)
			//});

			return Ok(new { id = user.userid });
		}

		[Authorize(Roles = Role.Super)]
		[HttpPost]
		public async Task<ActionResult<User>> confirm(User req)
		{
			var user = await _context.Users.Where(a => a.userid == req.userid).FirstOrDefaultAsync();
			if (user == null)
			{
				return NotFound();
			}

			if (user.userpw != "b4 confirm")
			{
				return BadRequest();
			}

			var newpw =  RandomString(8);
			user.userpw = GenerateMySQLHash(newpw);
			user.role = Role.User;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				return BadRequest();
			}


			//_emailService.Send(
			//	from: "inent.dev@gmail.com",
			//	to: user.email,
			//	subject: "Odor Monitor 회원승인에 관한 건",
			//	html: $@"<h4>안녕하세요</h4><p>
			//			id : <strong>{user.userid}</strong><br>
			//			password : <strong>{newpw}</strong><br><br>
			//			회원가입이 승인 되었습니다.<br><a href=""http://175.208.89.113:4100/"">여기</a>에서 가입된 ID를 확인해주세요.</p>"
			//	);

			string bufform = System.IO.File.ReadAllText(System.IO.Path.Combine(_env.ContentRootPath, @"form/userconfirm.html"));

			_emailService.Send(
				from: "inent.dev@gmail.com",
				to: user.email,
				subject: "Odor Monitor 회원승인에 관한 건",
				html: String.Format(bufform, 
						user.userid,
						newpw)
				);

			//_workLogService.Add(new WorkLog
			//{
			//	userid = User.Identity.Name,
			//	part = "confirm",
			//	level = "Normal",
			//	content = JsonSerializer.Serialize(new { user.userid })
			//});

			return Ok(new { result = "confirmed", id = user.userid });
		}

		private Random random = new Random();
		private string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}

		[Authorize(Roles = Role.Super)]
		[HttpPost]
		public async Task<ActionResult> Remove(User person)
		{
			var user = await _context.Users.Where(a => a.userid == person.userid).FirstOrDefaultAsync();
			if (user == null)
			{
				return NotFound();
			}

			//var owner = _userService.GetById(User.Identity.Name);

			//if (owner.userid != user.userid && owner.role != Role.Super)
			//{
			//	return Forbid();
			//}

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();

			//_workLogService.Add(new WorkLog
			//{
			//	userid = User.Identity.Name,
			//	part = "remove",
			//	level = "Normal",
			//	content = JsonSerializer.Serialize(new
			//	{
			//		user.userid,
			//		user.username,
			//		user.depart,
			//		user.position,
			//		user.email,
			//		user.phone
			//	}, new JsonSerializerOptions
			//	{
			//		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			//	})
			//});

			return Ok(new { result = user.username + " removed" });
		}

		private bool UserExists(string userid)
		{
			return _context.Users.Any(e => e.userid == userid);
		}

		[Authorize(Roles = Role.Super)]
		[HttpPost]
		public async Task<ActionResult> setRole(User _user)
		{
			if (String.IsNullOrEmpty(_user.userid) || String.IsNullOrEmpty(_user.role))
			{
				return BadRequest();
			}

			return await setRole(_user.userid, _user.role);
		}

		private async Task<ActionResult> setRole(string _userid, string _role)
		{
			var user = await _context.Users.Where(a => a.userid == _userid).FirstOrDefaultAsync();

			if (user == null)
			{
				return NotFound();
			}

			//_workLogService.Add(new WorkLog
			//{
			//	userid = User.Identity.Name,
			//	part = "setrole",
			//	level = "Normal",
			//	content = JsonSerializer.Serialize(new { user.userid, user.role, newrole = _role })
			//});

			user.role = _role;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserExists(user.userid))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return Ok(new { result = user.username + " set Role " + _role });
		}

		[Authorize(Roles = Role.Super)]
		[HttpPost]
		public async Task<ActionResult> setGeoCode(User req)
		{
			if (String.IsNullOrEmpty(req.userid) || String.IsNullOrEmpty(req.geocode))
			{
				return BadRequest();
			}

			var user = await _context.Users
				.Where(a => a.userid == req.userid)
				.FirstOrDefaultAsync();
			if (user == null)
			{
				return NotFound();
			}

			user.geocode = req.geocode;
			await _context.SaveChangesAsync();

			return Ok(new { result = "geocode Changed" });
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult> newpassword(User req)
		{
			var user = await _context.Users
				.Where(a => a.userid == req.userid && a.username == req.username && a.email == req.email)
				.FirstOrDefaultAsync();
			if (user == null)
			{
				return NotFound();
			}

			var newpw = RandomString(8);
			user.userpw = GenerateMySQLHash(newpw);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				return BadRequest();
			}

			//_emailService.Send(
			//	from: "inent.dev@gmail.com",
			//	to: user.email,
			//	subject: "Odor Monitor 패스워드 재발급에 관한 건",
			//	html: $@"<h4>안녕하세요</h4><p>
			//			id : <strong>{user.userid}</strong><br>
			//			password : <strong>{newpw}</strong><br><br>
			//			패스워드가 재발급 되었습니다.<br><a href=""http://175.208.89.113:4100/"">여기</a>에서 가입된 ID를 확인해주세요.</p>"
			//	);

			string bufform = System.IO.File.ReadAllText(System.IO.Path.Combine(_env.ContentRootPath, @"form/newpwd.html"));

			_emailService.Send(
				from: "inent.dev@gmail.com",
				to: user.email,
				subject: "Odor Monitor 패스워드 재발급에 관한 건",
				html: String.Format(bufform,
						user.userid,
						newpw)
				);



			//_workLogService.Add(new WorkLog
			//{
			//	userid = User.Identity.Name,
			//	part = "newpass",
			//	level = "Normal",
			//	content = JsonSerializer.Serialize(new { user.userid })
			//});

			return Ok(new { result = "reissued password", id = user.userid });
		}

		public async Task<ActionResult> getGeocode()
		{
			var bufid = User.Identity.Name;

			var user = await _context.Users
				.Where(a => a.userid == bufid)
				.FirstOrDefaultAsync();
			if (user == null)
			{
				return NotFound();
			}

			return Ok(new { id = bufid, geocode = user.geocode} );
		}

		//public async Task<ActionResult> Logout()
		//{
		//	await HttpContext.SignOutAsync();

		//	return Ok("Logout");
		//}

		private string GenerateMySQLHash(string key)
		{
			byte[] keyArray = Encoding.UTF8.GetBytes(key);
			SHA1Managed enc = new SHA1Managed();
			byte[] encodedKey = enc.ComputeHash(enc.ComputeHash(keyArray));
			StringBuilder myBuilder = new StringBuilder(encodedKey.Length);

			foreach (byte b in encodedKey)
				myBuilder.Append(b.ToString("X2"));

			return "*" + myBuilder.ToString();
		}


		private async Task RenewCoverage(User account)
		{
			//var account = await _context.Users.Where(a => a.userid == User.Identity.Name).FirstOrDefaultAsync();

			var bufwhere = "";

			if (account.geocode.Substring(0, 1) == "1")
			{
				if (account.geocode.Substring(3) == "00")
				{
					bufwhere += $"WHERE geocode LIKE '{account.geocode.Substring(0, 3)}%' ";
				}
				else
				{
					bufwhere += $"WHERE geocode = '{account.geocode}' ";
				}
			}

			switch (account.role)
			{
				case "super":
				case "admin":
					break;
				case "user":
					bufwhere += $"AND depart = '{account.depart}'";
					break;
			}

			var bufsql = $"DELETE FROM coverages WHERE userid='{account.userid}'";

			await _context.Database.ExecuteSqlRawAsync(bufsql);

			bufsql = $"INSERT INTO coverages SELECT '{account.userid}','D',id  FROM devices {bufwhere}";

			await _context.Database.ExecuteSqlRawAsync(bufsql);

			bufsql = $"INSERT INTO coverages SELECT '{account.userid}','U',userid  FROM users {bufwhere}";

			await _context.Database.ExecuteSqlRawAsync(bufsql);

		}


	}
}
