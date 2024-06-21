using System;
using System.Collections.ObjectModel;
using TheMeal.Models;
using TheMeal.Services;
using TheMeal.Views;

namespace TheMeal.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly MealDBService _mealDBService;
        public ObservableCollection<Category> Categories { get; } = new();

        public MainViewModel(MealDBService mealDBService)
        {
            _mealDBService = mealDBService;

            Task.Run(async () => await LoadCategoriesAsync());
        }

        public MainViewModel()
        {
        }
        private async Task LoadCategoriesAsync()
        {
            IsBusy = true;
            try
            {
                var categories = await _mealDBService.GetCategories();
                Categories.Clear();
                if (categories != null && categories.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        Categories.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load categories: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Command<Category> CategorySelectedCommand => new Command<Category>(async (category) =>
        {
            await Shell.Current.GoToAsync("//recipelist?Category="+ category.strCategory);
        });
    }

}

