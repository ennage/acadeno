using Microsoft.Extensions.Logging;
using Acadeno.Services;
namespace Acadeno;

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
		builder.Services.AddScoped<AuthService>();	// Existence of AuthService | put on top of .razor files : @inject AuthService AuthService

	#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
			builder.Logging.AddDebug();
	#endif
		// This tells the app: "Whenever I ask for the Database, use this specific file."
		builder.Services.AddDbContext<Acadeno.Tools.AppDbContext>();
		return builder.Build();
	}
}
