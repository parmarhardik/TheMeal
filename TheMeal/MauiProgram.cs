using Microsoft.Extensions.Logging;
using TheMeal.Services;
using TheMeal.ViewModels;
using TheMeal.Views;

namespace TheMeal;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Dependency Injection Registration
		builder.Services
			.AddHttpClient<MealDBService>() // Register the service with HttpClient
			.ConfigureHttpClient(client => client.BaseAddress = new Uri("https://www.themealdb.com/api/json/v1/1/"));

		builder.Services.AddSingleton<MealDBService>();

		builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<RecipeListViewModel>();
        builder.Services.AddTransient<RecipeDetailViewModel>();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(RecipeListPage), typeof(RecipeListPage));
        Routing.RegisterRoute(nameof(RecipeDetailPage), typeof(RecipeDetailPage));


#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

