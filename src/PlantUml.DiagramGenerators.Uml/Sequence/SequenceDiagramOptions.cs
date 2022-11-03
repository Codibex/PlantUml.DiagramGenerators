using PlantUml.DiagramGenerators.Uml.Options;

namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceDiagramOptions : DiagramOptions
{
    public static SequenceDiagramOptions Default => new();

    public DiagramOption HideUnlinkedParticipant { get; } = SequenceDiagramOption.HideUnlinkedParticipant();
}