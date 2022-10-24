namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceDiagramOptions
{
    private readonly List<string> _additionalOptions = new();

    public IReadOnlyList<string> AdditionalOptions => _additionalOptions;

    public static SequenceDiagramOptions Default => new();

    private SequenceDiagramOptions()
    {
    }

    public SequenceDiagramOptions AddOptions(params string[] options)
    {
        _additionalOptions.AddRange(options);
        return this;
    }
}