namespace PlantUml.DiagramGenerators.Uml.Sequence;

internal class ArrowBuilder
{
    private readonly ArrowOptions _options;

    public ArrowBuilder(ArrowOptions options)
    {
        _options = options;
    }

    public string Build()
    {
        return _options.Style switch
        {
            ArrowStyle.Normal => _options.Direction == ArrowDirection.SourceToTarget ? "->" : "<-",
            ArrowStyle.Dotted => _options.Direction == ArrowDirection.TargetToSource ? "-->" : "<--",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
