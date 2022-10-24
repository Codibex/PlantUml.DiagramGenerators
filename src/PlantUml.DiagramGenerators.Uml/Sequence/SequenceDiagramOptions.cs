namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceDiagramOptions
{
    public static SequenceDiagramOptions Default => new(Array.Empty<string>());

    public bool AutoNumber { get; set; }
    public string[] AdditionalOptions { get; set; }

    private SequenceDiagramOptions(params string[] additionalOptions)
    {
        AdditionalOptions = additionalOptions;
    }
}