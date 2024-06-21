using TheMeal.Services;
using TheMeal.ViewModels;

namespace TheMeal.Views;

[QueryProperty("RecipeId", "RecipeId")]
public partial class RecipeDetailPage : ContentPage, IQueryAttributable
{
    private readonly RecipeDetailViewModel _viewModel;

    public string RecipeId { get; set; }

    public RecipeDetailPage()
	{
		InitializeComponent();
        HttpClient httpClient = new HttpClient();
        var mealDBService = new MealDBService(httpClient);
        _viewModel = new RecipeDetailViewModel(mealDBService);
        BindingContext = _viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("RecipeId", out var recipeId))
        {
            RecipeId = recipeId.ToString();
            _ = _viewModel.InitializeAsync(RecipeId);
        }
    }
}
