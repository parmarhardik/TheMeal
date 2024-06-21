using TheMeal.Services;
using TheMeal.ViewModels;

namespace TheMeal;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();

        HttpClient httpClient = new HttpClient();
        var mealDBService = new MealDBService(httpClient); 
        BindingContext = new MainViewModel(mealDBService);
    }
}


