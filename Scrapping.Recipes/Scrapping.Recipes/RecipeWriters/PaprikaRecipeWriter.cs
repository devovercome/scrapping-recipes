// genius!
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Newtonsoft.Json;
using System.Linq;
using Scrapping.Recipes.Models;

namespace Scrapping.Recipes;

internal class PaprikaRecipeWriter
{
    private const string Yaml = ".yaml";

    private readonly PaprikaYamlModel[] recipes;

    public PaprikaRecipeWriter(IEnumerable<RecipeModel> recipes)
    {
        this.recipes = recipes
            .Select(recipe => 
                new PaprikaYamlModel()
                {
                    Notes = ToYamlString(recipe.FoodCategoriesModel.Categories),
                    Categories = ToYamlArrayString(recipe.Tags),
                    Description = "",
                    Difficulty = recipe.Difficulty,
                    Directions = ToYamlString(recipe.Steps),
                    Ingredients = ToYamlString(recipe.Ingredients.Select(x => x.ToString()).ToArray()),
                    Name = recipe.Title,
                    Nutritional_Info = recipe.NutrientsModel.Calories.ToString(),
                    Photo = recipe.ImageUrl,
                    Rating = recipe.Rating,
                    Servings = recipe.DishesCount.ToString(),
                    Source = recipe.ProviderName,
                    Source_URL = recipe.RecipeUrl,
                    Cook_Time = recipe.CookTimeMinutes.ToString(),
                    Prep_Time = recipe.PrepTimeMinutes.ToString(),
                    Total_Time = $"{recipe.PrepTimeMinutes + recipe.CookTimeMinutes} minutes",
                })
            .ToArray();
    }

    public void WriteYaml()
    {
        var yaml = YamlConverter.YamlConvert.SerializeObject(recipes);
        var path = Path.Combine(RecipeDir.FullName, "output" + Yaml);
        File.WriteAllText(path, yaml);
    }

    private static string ToYamlString(IEnumerable<string> strings)
    {
        strings = strings ?? new string[] { string.Empty };
        if (!strings.Any())
        {
            return string.Empty;
        }

        // TODO: rework for modern approach.
        var sb = new System.Text.StringBuilder();
        foreach (var item in strings)
        {
            sb.Append(item + " ");
        }
        return sb.ToString();
    }

    private static string ToYamlArrayString(IEnumerable<string> strings)
    {
        strings = strings ?? new string[] { string.Empty };
        // TODO: rework for modern approach.
        var sb = new System.Text.StringBuilder();
        sb.Append("[");
        foreach (var item in strings)
        {
            sb.Append(item + ",");
        }
        sb.Remove(sb.Length - 1, 1);
        sb.Append("]");

        return sb.ToString();
    }


    private DirectoryInfo RecipeDir 
        => new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
}

/// <summary>
/// https://www.paprikaapp.com/help/windows/
/// </summary>
internal class PaprikaYamlModel
{

    /// <summary> 
    /// The name of the recipe.
    /// </summary>
    /// <remarks>
    /// My Tasty Recipe.
    /// </remarks>
    public string Name { get; set; }

    /// <summary> 
    /// The number of servings this recipe makes.
    /// </summary>
    /// <remarks>
    /// 4-6 servings.
    /// </remarks>
    public string Servings { get; set; }

    /// <summary> 
    /// The difficulty of the recipe, from Easy to Hard. Custom values may also be entered.
    /// </summary>
    /// <remarks>
    /// Ez.
    /// </remarks>
    public string Difficulty { get; set; }

    /// <summary> 
    /// The amount of preparation time this recipe requires.
    /// </summary>
    /// <remarks>
    /// 100 minutes.
    /// </remarks>
    public string Prep_Time { get; set; }

    /// <summary> 
    /// The amount of cooking time this recipe requires.
    /// </summary>
    /// <remarks>
    /// 30 minutes.
    /// </remarks>
    public string Cook_Time { get; set; }

    /// <summary> 
    /// The total amount of time this recipe requires.
    /// </summary>
    /// <remarks>
    /// 130 minutes.
    /// </remarks>
    public string Total_Time { get; set; }

    /// <summary> 
    /// A star rating from 0 to 5 for this recipe.
    /// </summary>
    /// <remarks>
    /// 5.
    /// </remarks>
    public string Rating { get; set; }

    /// <summary> 
    /// The categories this recipe belongs to.
    /// </summary>
    /// <remarks>
    /// [Dinner, Holiday]
    /// </remarks>
    public string Categories { get; set; }

    /// <summary> 
    /// Where this recipe came from. Grandma, Serious Eats, etc.
    /// </summary>
    /// <remarks>
    /// Food Network.
    /// </remarks>
    public string Source { get; set; }

    /// <summary> 
    /// This is an optional field that records the original URL of the recipe if it came from a website.
    /// </summary>
    /// <remarks>
    /// www.FoodNetwork.com.
    /// </remarks>
    public string Source_URL { get; set; }

    /// <summary> 
    /// A short introduction of the recipe, displayed above the directions.
    /// </summary>    
    public string Description { get; set; }

    /// <summary> 
    /// The directions for this recipe.
    /// </summary>
    /// <remarks>
    /// Mix things together.
    /// Eat.
    /// Tasty.
    /// Yum yum yum.
    /// </remarks>
    public string Directions { get; set; }

    /// <summary> 
    /// Any cooking notes you would like to include with the recipe. Displayed below the directions.
    /// </summary>
    /// <remarks>
    /// This is delicious!!!
    /// </remarks>
    public string Notes { get; set; }

    /// <summary> 
    /// Any nutritional information you would like to record for this recipe. Displayed below the ingredients.
    /// </summary>
    /// <remarks>
    /// 500 calories.
    /// </remarks>
    public string Nutritional_Info { get; set; }

    /// <summary> 
    /// The ingredients required for this recipe. Note Create ingredient headings by ending them with a colon (e.g. Sauce:).
    /// </summary>
    /// <remarks>
    /// 1/2 lb meat
    /// 1/2 lb vegetables
    /// salt
    /// pepper
    /// 2 tbsp olive oil
    /// 4 cups flour
    /// </remarks>
    public string Ingredients { get; set; }

    /// <summary> 
    /// An optional thumbnail and one or more higher-resolution images. You can select files from the filesystem or from the clipboard.
    /// </summary>
    /// <remarks>
    /// Base64 image.
    /// </remarks>
    public string Photo { get; set; }   
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.