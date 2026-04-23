using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Services;
using Acadeno.Backend.Tools;
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
			string dbPath = Path.Combine(FileSystem.AppDataDirectory, "acadenodb.db");	//	Path to SQLite file

			/*	Register the DbContext with the SQLite connection string
			This tells the app: "Whenever I ask for the Database, use this specific file." */
			builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

			//	SERVICES
			builder.Services.AddScoped<AuthService>();		//	Existence of AuthService | put on top of .razor files : @inject AuthService AuthService

		#if DEBUG
				builder.Services.AddBlazorWebViewDeveloperTools();
				builder.Logging.AddDebug();
		#endif
			return builder.Build();
		}
	}
}