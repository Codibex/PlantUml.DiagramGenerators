namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceOptions
{
    public string SourceParticipant { get; }
    public string TargetParticipant { get; }
    public string? Description { get; }
    public bool IgnoreAutomaticArrowDirectionDetection { get; }

    public SequenceOptions(string sourceParticipant, string targetParticipant, string? description = null,
        bool ignoreAutomaticArrowDirectionDetection = false)
    {
        SourceParticipant = sourceParticipant;
        TargetParticipant = targetParticipant;
        Description = description;
        IgnoreAutomaticArrowDirectionDetection = ignoreAutomaticArrowDirectionDetection;
    }
}