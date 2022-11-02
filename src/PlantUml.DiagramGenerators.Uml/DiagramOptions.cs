namespace PlantUml.DiagramGenerators.Uml;

/// <summary>
/// Base diagram options
/// </summary>
public abstract class DiagramOptions
{
    private List<string> Options { get; } = new();

    public IEnumerable<string> AdditionalOptions => Options;

    public DiagramOptions AddOptions(params string[] options)
    {
        Options.AddRange(options);
        return this;
    }
}