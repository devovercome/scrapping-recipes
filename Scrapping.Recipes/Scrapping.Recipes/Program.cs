using Scrapping.Recipes;
using Scrapping.Recipes.Parsing.Parsers;
using static Scrapping.Recipes.ArgsHelper;
using static System.Console;

args = Lower(args);

if (TryDetectHelpArgs(args, out var response))
{
    Write(response);
    return;
}

var (providerName, url) = (args[0], args[1]);
var recipe = new EdaRuParser().Parse(url);
RecipeWriter.Write(recipe);