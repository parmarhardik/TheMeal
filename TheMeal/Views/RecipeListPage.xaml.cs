using TheMeal.Services;
using TheMeal.ViewModels;

namespace TheMeal.Views;

[QueryProperty("Category", "Category")]
public partial class RecipeListPage : ContentPage, IQueryAttributable
{

    private readonly RecipeListViewModel _viewModel;

    public string Category { get; set; }

    public RecipeListPage()
	{
		InitializeComponent();

        HttpClient httpClient = new HttpClient();
        var mealDBService = new MealDBService(httpClient);
        _viewModel = new RecipeListViewModel(mealDBService);
        BindingContext = _viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Category", out var category))
        {
            Category = category.ToString();
            _ = _viewModel.InitializeAsync(Category);
        }
    }

}
