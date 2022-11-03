using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using odmon.Helpers;
using odmon.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace odmon.Services
{
	public interface IUserService
	{
		User Authenticate(string username, string password);

	}

	public class UserService : IUserService
	{
		private readonly AppSettings _appSettings;
		private readonly DeviceContext _context;

		public UserService(DeviceContext context, IOptions<AppSettings> appSettings)
		{
			_context = context;
			_appSettings = appSettings.Value;
		}

		public User Authenticate(string userid, string password)
		{
			//var users = from m in _context.Users select m;
			////var user = users.Where(s => s.userid.Contains(userid)).FirstOrDefault();

			//var user = users.SingleOrDefault(x => x.userid == userid && x.userpw == password);

			var user = _context.Users.FromSqlRaw($"select * from users where userid='{userid}' and userpw=password('{password}')")
				.FirstOrDefault();

			// return null if user not found
			if (user == null)
				return null;

			// authentication successful so generate jwt token
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.userid),
					new Claim(ClaimTypes.Role, user.role)
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

			};

			//switch (user.role)
			//{
			//	case Role.Super:
			//		tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, Role.Admin));
			//		tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, Role.User));
			//		break;
			//	case Role.Admin:
			//		tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, Role.User));
			//		break;
			//	case Role.User:
			//		break;

			//}

			var token = tokenHandler.CreateToken(tokenDescriptor);
			user.token = tokenHandler.WriteToken(token);

			return user;
		}

	}
}
