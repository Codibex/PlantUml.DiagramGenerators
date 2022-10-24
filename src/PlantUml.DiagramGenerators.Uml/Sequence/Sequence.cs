namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class Sequence
{
    public string SourceParticipant { get; }
    public string TargetParticipant { get; }
    public string? Description { get; }
    public AutoNumber? AutoNumber { get; private set; }

    public Sequence(string sourceParticipant, string targetParticipant, string? description = null)
    {
        SourceParticipant = sourceParticipant;
        TargetParticipant = targetParticipant;
        Description = description;
    }

    public Sequence WithAutoNumber(int? startNumber = null, int? increment = null, string? style = null)
    {
        AutoNumber = new AutoNumber(startNumber, increment, style);
        return this;
    }
}