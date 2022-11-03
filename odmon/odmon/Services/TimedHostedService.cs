using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using odmon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace odmon.Services
{
	public class TimedHostedService : IHostedService, IDisposable
	{
		private int executionCount = 0;
		private readonly ILogger<TimedHostedService> _logger;
		private readonly IServiceProvider _serviceProvider;

		private Timer _timer;

		public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider serviceProvider)
		{
			_logger = logger;
			_serviceProvider = serviceProvider;
		}

		public Task StartAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Timed Hosted Service running.");

			//_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

			_timer = new Timer(DoArrange, null, TimeSpan.Zero, TimeSpan.FromHours(1));

			return Task.CompletedTask;
		}

		private void DoWork(object state)
		{
			var count = Interlocked.Increment(ref executionCount);

			_logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);

			using (IServiceScope scope = _serviceProvider.CreateScope())
			{
				IAlertService alertService =
					scope.ServiceProvider.GetRequiredService<IAlertService>();

				alertService.checkAlert();
			}

		}
		private void DoArrange(object state)
		{
			using (IServiceScope scope = _serviceProvider.CreateScope())
			{
				IArrangeService arrangeService =
					scope.ServiceProvider.GetRequiredService<IArrangeService>();

				arrangeService.tidyRecents();
			}

		}

		public Task StopAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Timed Hosted Service is stopping.");

			_timer?.Change(Timeout.Infinite, 0);

			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}
