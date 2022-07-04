using HtmlAgilityPack;

namespace Scrapping.Recipes.Helpers;

internal static class HtmlExtensions
{
    public static HtmlNodeCollection GetByTag(this HtmlNode node, string tag, string? innerText = null)
    {
        // NOTE: The token .//
        // selects the current node
        // and queries it by //
        if (innerText == null)
        {
            return node.SelectNodes(".//" + tag);
        }
        return node.SelectNodes($".//{tag}[. = '{innerText}']");
    }

    public static HtmlNodeCollection GetByProperty(this HtmlNode node, string propertyName, string? propertyValue = null)
    {
        if (propertyValue == null)
        {
            return node.SelectNodes($".//*[@{propertyName}]");
        }
        else
        {
            return node.SelectNodes($".//*[@{propertyName}='{propertyValue}']");
        }
    }

    public static HtmlNodeCollection GetByTag(this HtmlDocument html, string tag, string? innerText = null)
        => html.DocumentNode.GetByTag(tag, innerText);

    public static HtmlNodeCollection GetByProperty(this HtmlDocument html, string propertyName, string? propertyValue = null)
        => html.DocumentNode.GetByProperty(propertyName, propertyValue);
}