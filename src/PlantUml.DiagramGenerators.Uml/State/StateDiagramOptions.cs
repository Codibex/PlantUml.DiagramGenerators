namespace PlantUml.DiagramGenerators.Uml.State;

/// <summary>
/// Diagram options
/// </summary>
public class StateDiagramOptions : DiagramOptions
{
    public static StateDiagramOptions Default => new(true);

    public bool HideEmptyDescriptionTag { get; set; }

    private StateDiagramOptions(bool hideEmptyDescriptionTag)
    {
        HideEmptyDescriptionTag = hideEmptyDescriptionTag;
    }
}