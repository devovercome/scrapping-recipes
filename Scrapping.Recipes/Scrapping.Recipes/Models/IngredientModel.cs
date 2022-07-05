namespace Scrapping.Recipes.Models;

internal class IngredientModel
{
    internal string? Name { get; set; }

    internal string? Measure { get; set; }

    public override string ToString()
    {
        return Name + "-" + Measure;
    }
}
