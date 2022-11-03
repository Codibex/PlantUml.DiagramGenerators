namespace PlantUml.DiagramGenerators.Uml.Utilities;

internal static class Tab
{
    internal static string GetTabs(int tabCount) =>
        $"{string.Join("", Enumerable.Range(0, tabCount).Select(_ => "\t"))}";
}