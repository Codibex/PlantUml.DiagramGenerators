namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceOptions
{
    public string SourceParticipant { get; }
    public string TargetParticipant { get; }
    public string? Description { get; }
    public bool IgnoreForAutomaticArrowDirectionDetection { get; }

    public SequenceOptions(string sourceParticipant, string targetParticipant, string? description = null,
        bool ignoreForAutomaticArrowDirectionDetection = false)
    {
        SourceParticipant = sourceParticipant;
        TargetParticipant = targetParticipant;
        Description = description;
        IgnoreForAutomaticArrowDirectionDetection = ignoreForAutomaticArrowDirectionDetection;
    }
}