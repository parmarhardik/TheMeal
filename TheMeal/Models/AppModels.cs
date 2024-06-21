using System;
namespace TheMeal.Models
{
    // Category Model
    public class Category
    {
        public required string strCategory { get; set; }
    }

    // Recipe Model
    public class Recipe
    {
        public required string idMeal { get; set; }
        public required string strMeal { get; set; }
        public required string strMealThumb { get; set; }
    }

    // Recipe Detail Model
    public class RecipeDetail : Recipe
    {
        public required string strInstructions { get; set; }
    }

}

