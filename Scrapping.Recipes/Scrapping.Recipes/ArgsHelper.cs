namespace Scrapping.Recipes;

internal static class ArgsHelper
{
    internal static bool TryDetectHelpArgs(string[] args, out string responseLine)
    {
        responseLine = string.Empty;
        var command = args[0];
        var limit = 6; // random limit.

        if (command.Contains("help") && command.Length < limit)
        {
            responseLine = "Expected format: <provider> <url>"
                + Environment.NewLine
                + "Example: eda.ru https://eda.ru/recepty/osnovnye-blyuda/ovoshhnoe-ragu-s-kapustoj-34016";

            return true;
        }

        return false;
    }

    internal static string[] Lower(string[] args)
        => args.Select(x => x.ToLower()).ToArray();
}
