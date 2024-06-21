using System;
using TheMeal.Models;
using TheMeal.Services;

namespace TheMeal.ViewModels
{
    public class RecipeDetailViewModel : BaseViewModel
    {
        private readonly MealDBService _mealDBService;

        public Command GoBackCommand { get; }

        private RecipeDetail _recipe;

        public RecipeDetail Recipe
        {
            get => _recipe;
            private set => SetProperty(ref _recipe, value);
        }

        public RecipeDetailViewModel()
        {
        }

        public RecipeDetailViewModel(MealDBService mealDBService)
        {
            _mealDBService = mealDBService;

            GoBackCommand = new Command(async () => await Shell.Current.GoToAsync("//recipelist"));
        }

        public async Task InitializeAsync(string recipeId)
        {
            IsBusy = true;
            try
            {
                Recipe = await _mealDBService.GetRecipeDetails(recipeId);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load recipe details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

}

