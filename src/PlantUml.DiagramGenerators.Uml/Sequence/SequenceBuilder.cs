using System.Text;

namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceBuilder : UmlBuilder
{
    private readonly IList<string> _sequences = new List<string>();
    public SequenceBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public SequenceBuilder AddSequence(string sourceParticipant, string targetParticipant, string? sequenceDescription = null, ArrowOptions? arrowOptions = null)
    {
        var currentArrowOptions = arrowOptions ?? new ArrowOptions();

        var sourceToTargetSequence = $"{sourceParticipant}_{targetParticipant}";
        var targetToSourceSequence = $"{targetParticipant}_{sourceParticipant}";
        if (_sequences.Contains(sourceToTargetSequence) == false && _sequences.Contains(targetToSourceSequence) == false)
        {
            _sequences.Add(sourceToTargetSequence);
            currentArrowOptions.Direction = ArrowDirection.SourceToTarget;
        }
        else if (_sequences.Contains(sourceToTargetSequence))
        {
            currentArrowOptions.Direction = ArrowDirection.SourceToTarget;
        }
        else
        {
            currentArrowOptions.Direction = ArrowDirection.TargetToSource;
        }

        AddEntry(GetSequence(sourceParticipant, targetParticipant, sequenceDescription, currentArrowOptions));
        return this;
    }

    public SequenceBuilder AddParticipant(string participantName, string alias)
    {
        AddEntry(GetParticipant(ParticipantType.Participant, participantName, alias));
        return this;
    }

    public SequenceBuilder AddActor(string participantName, string alias)
    {
        AddEntry(GetParticipant(ParticipantType.Actor, participantName, alias));
        return this;
    }

    public SequenceBuilder AddBoundary(string participantName, string alias)
    {
        AddEntry(GetParticipant(ParticipantType.Boundary, participantName, alias));
        return this;
    }

    public SequenceBuilder AddControl(string participantName, string alias)
    {
        AddEntry(GetParticipant(ParticipantType.Control, participantName, alias));
        return this;
    }

    public SequenceBuilder AddEntity(string participantName, string alias)
    {
        AddEntry(GetParticipant(ParticipantType.Entity, participantName, alias));
        return this;
    }

    public SequenceBuilder AddDatabase(string participantName, string alias)
    {
        AddEntry(GetParticipant(ParticipantType.Database, participantName, alias));
        return this;
    }

    public SequenceBuilder AddCollections(string participantName, string alias)
    {
        AddEntry(GetParticipant(ParticipantType.Collections, participantName, alias));
        return this;
    }

    public SequenceBuilder AddQueue(string participantName, string alias)
    {
        AddEntry(GetParticipant(ParticipantType.Queue, participantName, alias));
        return this;
    }

    private static string GetSequence(string sourceParticipant, string targetParticipant, string? sequenceDescription, ArrowOptions arrowOptions)
    {
        return AppendDescription($"{sourceParticipant} {GetArrow(arrowOptions)} {targetParticipant}", sequenceDescription);
    }

    private static string GetParticipant(ParticipantType participantType, string participantName, string alias)
    {
        var participantText = participantType switch
        {
            ParticipantType.Participant => "Participant",
            ParticipantType.Actor => "Actor",
            ParticipantType.Boundary => "Boundary",
            ParticipantType.Control => "Control",
            ParticipantType.Entity => "Entity",
            ParticipantType.Database => "Database",
            ParticipantType.Collections => "Collections",
            ParticipantType.Queue => "Queue",
            _ => throw new ArgumentOutOfRangeException(nameof(participantType), participantType, null)
        };

        return $"{participantName} {participantText} as {alias}";
    }

    private static string GetArrow(ArrowOptions arrowOptions)
    {
        return new ArrowBuilder(arrowOptions).Build();
    }

    private static string AppendDescription(string transition, string? sequenceDescription)
        => string.IsNullOrWhiteSpace(sequenceDescription) ? transition : $"{transition} : {sequenceDescription}";

    internal string Build()
    {
        var stringBuilder = new StringBuilder();
        foreach (string value in Statements.Values)
        {
            stringBuilder.AppendLine(value);
        }

        return stringBuilder.ToString().TrimEnd();
    }
}