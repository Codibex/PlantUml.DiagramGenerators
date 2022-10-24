namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class Sequence
{
    public string SourceParticipant { get; }
    public string TargetParticipant { get; }
    public string? Description { get; }
    public bool IgnoreForAutomaticArrowDirectionDetection { get; }
    public AutoNumber? AutoNumber { get; private set; }

    public Sequence(string sourceParticipant, string targetParticipant, string? description = null,
        bool ignoreForAutomaticArrowDirectionDetection = false)
    {
        SourceParticipant = sourceParticipant;
        TargetParticipant = targetParticipant;
        Description = description;
        IgnoreForAutomaticArrowDirectionDetection = ignoreForAutomaticArrowDirectionDetection;
    }

    public Sequence WithAutoNumber(int? startNumber = null, int? increment = null,
        string? style = null, AutoNumberBreak? autoNumberBreak = null)
    {
        AutoNumber = new AutoNumber(startNumber, increment, style, autoNumberBreak);
        return this;
    }
}