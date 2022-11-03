namespace PlantUml.DiagramGenerators.Uml.Options;

public class StateDiagramOption : DiagramOption
{
    private StateDiagramOption(string parameter) : base(parameter)
    {
    }

    public static DiagramOption HideEmptyDescriptionTag
        => new StateDiagramOption("hide empty description");
}
