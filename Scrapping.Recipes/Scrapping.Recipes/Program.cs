using Scrapping.Recipes;
using Scrapping.Recipes.Models;
using Scrapping.Recipes.Parsing.Parsers;
using static Scrapping.Recipes.ArgsHelper;
using static System.Console;

//args = Lower(args);

//if (TryDetectHelpArgs(args, out var response))
//{
//    Write(response);
//    return;
//}

//var (providerName, url) = (args[0], args[1]);

var providerName = "Eda.ru";

var urls = new string[]
{
    "https://eda.ru/recepty/osnovnye-blyuda/ovoshhnoe-ragu-s-kapustoj-34016",
    "https://eda.ru/recepty/zavtraki/mannaya-kasha-molochnaya-91461",
    "https://eda.ru/recepty/zavtraki/ovsjanka-shokoladnaja-33481",
    "https://eda.ru/recepty/supy/sup-s-tushenkoy-139741",
    "https://eda.ru/recepty/salaty/salat-vitaminnij-iz-siroj-svekli-32579",
    "https://eda.ru/recepty/osnovnye-blyuda/kurinye-zheludki-po-koreyski-136924",
};


var recipes = new List<RecipeModel>();
foreach (var url in urls)
{
    var recipe = new EdaRuParser().Parse(url);
    recipe.ProviderName = providerName;
    recipes.Add(recipe);
}

new PaprikaRecipeWriter(recipes).WriteYaml();
// var url = "https://eda.ru/recepty/osnovnye-blyuda/ovoshhnoe-ragu-s-kapustoj-34016";

