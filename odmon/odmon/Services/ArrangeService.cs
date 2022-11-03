using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using odmon.Helpers;
using odmon.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace odmon.Services
{
	public interface IArrangeService
	{
		void tidyRecents();
	}

	public class ArrangeService : IArrangeService
	{
		private readonly ILogger<ArrangeService> _logger;
		private readonly DeviceContext _context;
		public ArrangeService(ILogger<ArrangeService> logger, DeviceContext context)
		{
			_logger = logger;
			_context = context;
		}

		public void tidyRecents()
		{
			var bufsql = @"DELETE FROM recents WHERE id IN 
(
	SELECT id FROM (
	   SELECT *, RANK() OVER (PARTITION BY m.deviceid ORDER BY m.id DESC) AS a
	   FROM recents m
	) AS rankrow
	WHERE rankrow.a > 10
)";

			_context.Database.ExecuteSqlRaw(bufsql);

			_logger.LogInformation("tidy recents");

		}


	}
}
