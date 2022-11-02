namespace PlantUml.DiagramGenerators.Uml.Status;

/// <summary>
/// Diagram options
/// </summary>
public class StatusDiagramOptions : DiagramOptions
{
    public static StatusDiagramOptions Default => new(true);

    public bool HideEmptyDescriptionTag { get; set; }

    private StatusDiagramOptions(bool hideEmptyDescriptionTag)
    {
        HideEmptyDescriptionTag = hideEmptyDescriptionTag;
    }
}