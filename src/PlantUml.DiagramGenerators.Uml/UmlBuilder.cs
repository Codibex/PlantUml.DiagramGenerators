namespace PlantUml.DiagramGenerators.Uml;

public abstract class UmlBuilder
{
    protected int NestingDepth { get; }
    protected SortedList<int, string> Statements { get; } = new();

    protected UmlBuilder(int nestingDepth)
    {
        NestingDepth = nestingDepth;
    }

    protected void AddEntry(string entry) => Statements.Add(Statements.Count, $"{GetTabs()}{entry}");

    private string GetTabs() =>
        $"{string.Join("", Enumerable.Range(0, NestingDepth).Select(_ => "\t"))}";
}