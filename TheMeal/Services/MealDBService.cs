using System;
using Newtonsoft.Json;
using TheMeal.Models;

namespace TheMeal.Services
{
    public class MealDBService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://www.themealdb.com/api/json/v1/1/";

        public MealDBService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Category>> GetCategories()
        {
            var response = await _httpClient.GetStringAsync(BaseUrl + "categories.php");

            if (response != null)
                return JsonConvert.DeserializeObject<MealDBResponse<Category>>(response).categories;
            else
                return new List<Category>();
        }

        public async Task<List<Recipe>> GetRecipesByCategory(string category)
        {
            var response = await _httpClient.GetStringAsync(BaseUrl + $"filter.php?c={category}");

            if (response != null)
                return JsonConvert.DeserializeObject<MealDBResponse<Recipe>>(response).meals;
            else
                return new List<Recipe>();
        }

        public async Task<RecipeDetail> GetRecipeDetails(string id)
        {
            var response = await _httpClient.GetStringAsync(BaseUrl + $"lookup.php?i={id}");

            if (response != null)
                return JsonConvert.DeserializeObject<MealDBResponse<RecipeDetail>>(response).meals[0];
            else
                return new RecipeDetail() { idMeal = string.Empty, strInstructions = string.Empty, strMeal = string.Empty, strMealThumb = string.Empty};
        }

        // Helper class for deserialization (due to API structure)
        private class MealDBResponse<T>
        {
            public List<T> meals { get; set; }
            public List<T> categories { get; set; }
        }
    }

}

