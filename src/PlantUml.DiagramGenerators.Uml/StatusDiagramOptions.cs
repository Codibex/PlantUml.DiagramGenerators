namespace PlantUml.DiagramGenerators.Uml;

/// <summary>
/// Diagram options
/// </summary>
public class StatusDiagramOptions
{
    public static StatusDiagramOptions Default => new(true, Array.Empty<string>());

    public bool HideEmptyDescriptionTag { get; set; }
    public string[] AdditionalOptions { get; set; }

    private StatusDiagramOptions(bool hideEmptyDescriptionTag, params string[] additionalOptions)
    {
        HideEmptyDescriptionTag = hideEmptyDescriptionTag;
        AdditionalOptions = additionalOptions;
    }
}