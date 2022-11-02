namespace PlantUml.DiagramGenerators.Uml.Utilities;
internal static class StringExtensions
{
    internal static string AddTabsPerLine(this string text, int tabCount)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return text;
        }

        return string.Join(Environment.NewLine, text.Split(Environment.NewLine).Select(l => $"{Tab.GetTabs(tabCount)}{l}"));
        
    }
}