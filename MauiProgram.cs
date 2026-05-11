using Acadeno.Backend.Services;
using Acadeno.Backend.Simulation;
using Acadeno.Backend.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Acadeno
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				});

			builder.Services.AddMauiBlazorWebView();

			//	DATABASE CONFIGURATION
			string dbPath = Path.Combine(AppContext.BaseDirectory, "Data", "acadeno.db");	//	Path to SQLite file

			/*	Register the DbContext with the SQLite connection string
			This tells the app: "Whenever I ask for the Database, use this specific file." */
			builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

			//	SERVICES
			builder.Services.AddScoped<AuthService>();
			builder.Services.AddScoped<CalendarService>();
			builder.Services.AddScoped<CourseService>();
			builder.Services.AddScoped<IdService>();
			builder.Services.AddScoped<ScheduleService>();
			builder.Services.AddScoped<TaskService>();
			builder.Services.AddScoped<TermService>();
			builder.Services.AddScoped<UserService>();

			//	SIMULATION
			builder.Services.AddScoped<GenAveSimulator>();
			builder.Services.AddScoped<GradingEngine>();
			builder.Services.AddScoped<PriorityEngine>();

		#if DEBUG
				builder.Services.AddBlazorWebViewDeveloperTools();
				builder.Logging.AddDebug();
		#endif
			return builder.Build();
		}
	}
}