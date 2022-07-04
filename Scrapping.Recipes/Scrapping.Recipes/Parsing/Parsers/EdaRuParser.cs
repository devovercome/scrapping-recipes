using HtmlAgilityPack;
using Scraping.Web;
using Scrapping.Recipes.Models;

namespace Scrapping.Recipes.Parsing.Parsers;

internal class EdaRuParser
{
    public RecipeModel Parse(string url)
    {
        var html = GetHtml(url);

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        return new RecipeModel
        {
            Title = GetRecipeHeader(doc),
            NutrientsModel = new()
            {
                Calories = GetCaloriesAmount(doc),
                Proteins = GetProteinsAmount(doc),
                Fat = GetFatAmount(doc),
                Carbon = GetCarbonAmount(doc)
            },
            CookTimeMinutes = GetCookTime(doc),
            DishesCount = GetDishesCount(doc),
            DescriptionLines = GetDescriptionLines(doc),
            Ingredients = GetIngredients(doc),
            FoodCategoriesModel = GetCategories(doc),
            ProviderName = "eda.ru",
            ImageUrl = GetImageUrl(doc),
            RecipeUrl = url,
        };
    }

    private static string GetHtml(string url)
        => url.StartsWith("http") switch
        {
            true => new HttpRequestFluent(true).FromUrl(url).Load().HtmlPage,
            false => File.ReadAllText(url)
        };

    private static uint GetDishesCount(HtmlDocument doc)
        => doc.GetByTag("span", "порции")
            .First()
            .ParentNode
            .GetByTag("div")
            .Last()
            .InnerText
            .ToUInt();

    private static FoodCategoriesModel GetCategories(HtmlDocument doc) 
        => new(doc
            .GetByProperty("itemtype", "http://schema.org/BreadcrumbList")
            .First()
            .GetByProperty("itemtype", "http://schema.org/ListItem")
            .Skip(1)
            .Select(x => x.InnerText)
            .ToArray());

    private static IngredientModel[] GetIngredients(HtmlDocument doc)
    {
        var ingredientLineNodes = GetItemProps(doc, "recipeIngredient")
            .Select(x => x.ParentNode.ParentNode.ParentNode);

        var result = new SortedDictionary<string, string>();

        foreach (var node in ingredientLineNodes)
        {
            var name = node.GetByProperty("itemprop", "recipeIngredient").First().InnerText;
            var amount = node.GetTags("span").Last().InnerText;
            if (amount.Contains("½"))
            {
                amount = amount.Replace("½", "0.5");
            }

            result.Add(name, amount);
        }

        return result.Select(x => new IngredientModel()
        {
            Name = x.Key,
            Measure = x.Value
        }).ToArray();
    }

    private static string GetRecipeHeader(HtmlDocument doc)
        => doc.GetByTag("h1").First().InnerText;

    private static uint GetCookTime(HtmlDocument doc)
        => GetItemProp(doc, "cookTime")
            .Split(' ')
            .First()
            .ToUInt();

    private static string[] GetDescriptionLines(HtmlDocument doc)
        => ToInnerText(GetItemProps(doc, "text"));

    private static string GetImageUrl(HtmlDocument doc)
        => doc.GetByProperty("alt", "Изображение материала").First().Attributes.First(x => x.Name == "src").Value;

    private static uint GetCaloriesAmount(HtmlDocument doc)
        => GetItemProp(doc, "calories").ToUInt();

    private static uint GetProteinsAmount(HtmlDocument doc)
        => GetItemProp(doc, "proteinContent").ToUInt();

    private static uint GetFatAmount(HtmlDocument doc)
        => GetItemProp(doc, "fatContent").ToUInt();

    private static uint GetCarbonAmount(HtmlDocument doc)
        => GetItemProp(doc, "carbohydrateContent").ToUInt();

    private static string GetItemProp(HtmlDocument doc, string propertyName)
        => GetItemProps(doc, propertyName).First().InnerText;

    private static HtmlNodeCollection GetItemProps(HtmlDocument doc, string propertyName)
        => doc.GetByProperty("itemprop", propertyName);

    private static string[] ToInnerText(HtmlNodeCollection nodes)
        => nodes.Select(x => x.InnerText).ToArray();


}
