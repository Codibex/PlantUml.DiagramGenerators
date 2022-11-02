namespace PlantUml.DiagramGenerators.Uml;

public class DiagramOptions
{
    protected List<string> Options { get; } = new();

    public IReadOnlyList<string> AdditionalOptions => Options;

    public DiagramOptions AddOptions(params string[] options)
    {
        Options.AddRange(options);
        return this;
    }
}