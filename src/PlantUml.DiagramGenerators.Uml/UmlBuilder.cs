namespace PlantUml.DiagramGenerators.Uml;

public class UmlConstants
{
    public const string START_TAG = "@startuml";
    public const string END_TAG = "@enduml";

    public const string HIDE_EMPTY_DESCRIPTION_TAG = "hide empty description";
}

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