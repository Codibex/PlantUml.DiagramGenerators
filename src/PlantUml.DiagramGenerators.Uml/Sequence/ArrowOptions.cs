namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class ArrowOptions
{
    public ArrowStyle Style { get; init; } = ArrowStyle.Normal;
    internal ArrowDirection Direction { get; set; }
}

public enum ArrowDirection
{
    SourceToTarget,
    TargetToSource
}