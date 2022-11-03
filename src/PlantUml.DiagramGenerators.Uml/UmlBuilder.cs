using System.Text;
using PlantUml.DiagramGenerators.Uml.Utilities;

namespace PlantUml.DiagramGenerators.Uml;

/// <summary>
/// Base class which provides base functionality to provide an uml part
/// </summary>
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
        string tabs = ignoreTabs ? string.Empty : Tab.GetTabs(NestingDepth);
        Statements.Add(Statements.Count, $"{tabs}{entry}");
    }
}