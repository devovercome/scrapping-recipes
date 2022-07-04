namespace Scrapping.Recipes.Models;

internal class FoodCategoriesModel
{
    internal string[] Categories { get; }
    internal string MainCategory { get; }

    public FoodCategoriesModel(IEnumerable<string> categories)
    {
        // actually, it should be a factory, since there is a logic for MainCategory.
        Categories = categories.ToArray();
        MainCategory = Categories[0];
    }
}
