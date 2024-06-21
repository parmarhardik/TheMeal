using System;
using System.Collections.ObjectModel;
using TheMeal.Services;
using TheMeal.Models;
using TheMeal.Views;

namespace TheMeal.ViewModels
{
    public class RecipeListViewModel : BaseViewModel
    {
        private readonly MealDBService _mealDBService;
        public ObservableCollection<Recipe> Recipes { get; } = new();

        public Command GoBackCommand { get; } 

        public string Category { get; set; }

        public RecipeListViewModel()
        {
        }

        public RecipeListViewModel(MealDBService mealDBService)
        {
            _mealDBService = mealDBService;

            GoBackCommand = new Command(async () => await Shell.Current.GoToAsync("//MainPage"));
        }

        public async Task InitializeAsync(string category)
        {
            Category = category;
            await LoadRecipesAsync();
        }

        private async Task LoadRecipesAsync()
        {
            IsBusy = true;
            try
            {
                var recipes = await _mealDBService.GetRecipesByCategory(Category);
                Recipes.Clear();
                foreach (var recipe in recipes)
                {
                    Recipes.Add(recipe);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load recipes: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Command<Recipe> RecipeSelectedCommand => new Command<Recipe>(async (recipe) =>
        {
            await Shell.Current.GoToAsync("//recipedetail?RecipeId=" + recipe.idMeal);
        });

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Category", out var category) && category is string categoryName)
            {
                _ = InitializeAsync(categoryName);
            }
        }
    }

}

