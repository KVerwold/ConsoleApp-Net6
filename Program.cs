using ConsoleApp.Apps;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ConsoleApp
{
	[Command(Name = "ConsoleApp", Description = "ConsoleApp template"),
		Subcommand(typeof(RunApp))]
	[HelpOption("-?|-h|--help")]
	internal class Program
	{
		private static readonly CommandLineApplication _app = new CommandLineApplication<Program>();

		private static int Main(string[] args)
		{
			using IHost host = CreateHostBuilder(args).Build();
			_app.Conventions
				.UseDefaultConventions()
				.UseConstructorInjection(host.Services);

			try
			{
				return _app.Execute(args);
			}
			catch (CommandParsingException)
			{
				_app.ShowHelp();
				return -1;
			}
		}
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseConsoleLifetime()
				.ConfigureHostConfiguration(configHost => configHost.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true))
				.ConfigureServices((hostingContext, services) => ConfigureServices(hostingContext.Configuration, services))
				.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))
			;

		private static void ConfigureServices(IConfiguration config, IServiceCollection services)
		{
			//var sqlConnectionString = config.GetConnectionString("SqlConnection");
			//services.Configure<MyConfigClass>(config.GetSection(nameof(MyConfigClass)));
			//services.AddSingleton<IMyService, MyService>();
		}

		private int OnExecute()
		{
			_app.ShowHelp();
			return 0;
		}
	}
}