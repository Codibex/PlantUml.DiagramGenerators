namespace PlantUml.DiagramGenerators.Uml;

public abstract class UmlBuilder
{
    protected const string START_TAG = "@startuml";
    protected const string END_TAG = "@enduml";

    protected const string HIDE_EMPTY_DESCRIPTION_TAG = "hide empty description";

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