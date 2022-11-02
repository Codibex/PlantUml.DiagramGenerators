using System.Text;

namespace PlantUml.DiagramGenerators.Uml;

public abstract class UmlBuilder
{
    protected int NestingDepth { get; }
    private SortedList<int, string> Statements { get; } = new();

    protected UmlBuilder(int nestingDepth)
    {
        NestingDepth = nestingDepth;
    }

    internal string Build()
    {
        var stringBuilder = new StringBuilder();
        foreach (string value in Statements.Values)
        {
            stringBuilder.AppendLine(value);
        }

        return stringBuilder.ToString().TrimEnd();
    }

    protected void AddEntry(string entry, bool ignoreTabs = false)
    {
        string tabs = ignoreTabs ? string.Empty : GetTabs();
        Statements.Add(Statements.Count, $"{tabs}{entry}");
    }

    private string GetTabs() =>
        $"{string.Join("", Enumerable.Range(0, NestingDepth).Select(_ => "\t"))}";
}