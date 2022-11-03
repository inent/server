using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using odmon.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSocket;
using SuperSocket.ProtoBase;
using System.Text;
using odacc;
using SuperSocket.Command;
using odacc.Commands;
using SuperSocket.WebSocket.Server;
using odacc.Services;
using odacc.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace odmon
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//Log.Logger = new LoggerConfiguration()
			//	.MinimumLevel.Warning()
			//	//.MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
			//	.MinimumLevel.Override("Vonk", Serilog.Events.LogEventLevel.Information)
			//	.WriteTo.File("odmon_.txt",
			//		outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
			//		rollingInterval: RollingInterval.Day)
			//	.CreateLogger();

			//var configuration = new ConfigurationBuilder()
			//	.SetBasePath(Directory.GetCurrentDirectory())
			//	.AddJsonFile("appsettings.json")
			//	.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
			//	.Build();

			//Log.Logger = new LoggerConfiguration()
			//	.ReadFrom.Configuration(configuration)
			//	.CreateLogger();

			try
			{
				Log.Information("Starting web host");
				CreateHostBuilder(args).Build().Run();
				return;
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Host terminated unexpectedly");
				return;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseSerilog()
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				})
				.AsWebSocketHostBuilder()
				.UseSession<UserSession>()
				.ConfigureServices((context, services) =>
				{
					services.AddSingleton<odacc.UserService>();
					//services.AddSingleton<AccumService>();
					services.AddScoped<IAccumService, AccumService>();

					//services.AddDbContext<OdorContext>(
					//	options => options.UseMySql(context.Configuration["ConnectionStrings:DefaultConnection"]));

				})
				.UseCommand<StringPackageInfo, StringPackageConverter>(commandOptions =>
				{
					commandOptions.AddCommand<noneProc>();
					commandOptions.AddCommand<accumProc>();
					commandOptions.AddCommand<connectList>();
					commandOptions.AddCommand<orderInsys>();
					commandOptions.AddCommand<orderJtron>();

					//commandOptions.AddCommand<CON>();
					//commandOptions.AddCommand<MSG>();
				})

				//.ConfigureLogging((hostCtx, loggingBuilder) =>
				//{
				//	var logger = new LoggerConfiguration()
				//		.ReadFrom.Configuration(hostCtx.Configuration)
				//		.MinimumLevel.Information()
				//		.MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
				//		//.WriteTo.Console()
				//		.WriteTo.File("odacc_.txt",
				//			outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
				//			rollingInterval: RollingInterval.Day)
				//		.CreateLogger();

				//	loggingBuilder.AddSerilog(logger);

				//})

				;



	}
}
