namespace Scrapping.Recipes.Models;

internal class RecipeModel
{
    internal string Title { get; set; }
    internal NutrientsModel NutrientsModel { get; set; }
    internal uint PrepTimeMinutes { get; set; }
    internal uint CookTimeMinutes { get; set; }
    internal uint DishesCount { get; set; }
    internal string[] Steps { get; set; }
    internal string[] Tags { get; set; }
    internal IngredientModel[] Ingredients { get; set; }
    internal FoodCategoriesModel FoodCategoriesModel { get; set; }
    internal string ImageUrl { get; set; }
    internal string RecipeUrl { get; set; }
    internal string ProviderName { get; set; }
    internal string Difficulty { get; set; }
    internal string Rating { get; set; }
}
