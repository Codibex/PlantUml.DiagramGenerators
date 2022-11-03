namespace PlantUml.DiagramGenerators.Uml.Options;

public class SequenceDiagramOption : DiagramOption
{
    private SequenceDiagramOption(string parameter) : base(parameter)
    {
    }

    public static DiagramOption HideUnlinkedParticipant()
        => new SequenceDiagramOption("hide unlinked");
}