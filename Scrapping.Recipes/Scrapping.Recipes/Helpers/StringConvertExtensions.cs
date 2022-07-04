namespace Scrapping.Recipes.Helpers;

internal static class StringConvertExtensions
{
    internal static uint ToUInt(this string target)
        => target == null ? 0 : Convert.ToUInt32(target);
}
