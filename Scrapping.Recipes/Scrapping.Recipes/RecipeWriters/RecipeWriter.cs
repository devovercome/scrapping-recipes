using Newtonsoft.Json;
using Scrapping.Recipes.Models;

namespace Scrapping.Recipes;

internal class RecipeWriter
{
    private const string RawFileName = "_raw";
    private const string IngredientsFileName = "Ingredients";
    private const string StepsFileName = "Steps";
    private const string Txt = ".txt";
    private const string Json = ".json";

    private readonly RecipeModel recipe;

    public RecipeWriter(RecipeModel recipe)
    {
        this.recipe = recipe;
    }

    public void WriteJson()
    {
        var json = JsonConvert.SerializeObject(recipe, Formatting.Indented);
        var jsonPath = Path.Combine(RecipeDir.FullName, RawFileName + Json);
        File.WriteAllText(jsonPath, json);
    }

    public void WriteIngredients()
    {
        var lines = recipe.Ingredients
            .Select(x => x.Name + '\t' + x.Measure)
            .ToArray();
        WriteLines(IngredientsFileName + Txt, lines);
    }

    public void WriteSteps()
    {
        WriteLines(StepsFileName + Txt, recipe.Steps);
    }

    public static void Write(RecipeModel recipe)
    {
        var writer = new RecipeWriter(recipe);
        writer.WriteSteps();
        writer.WriteIngredients();
        writer.WriteJson();
    }

    private void WriteLines(string fileName, string[] lines)
    {
        File.WriteAllLines(
            Path.Combine(RecipeDir.FullName, fileName),
            lines);
    }

    private DirectoryInfo RecipeDir 
        => new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
            .CreateSubdirectory(recipe.Title);
}
