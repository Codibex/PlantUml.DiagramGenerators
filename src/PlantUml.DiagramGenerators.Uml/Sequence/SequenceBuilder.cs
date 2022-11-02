using System.Text;

namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceBuilder : UmlBuilder
{
    private readonly IList<string> _sequences = new List<string>();
    public SequenceBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public SequenceBuilder AddSequence(string sourceParticipant, string targetParticipant,
        string? sequenceDescription = null, Action<ArrowOptions>? arrowConfig = null)
    {
        AddSequence(new SequenceOptions(sourceParticipant, targetParticipant, sequenceDescription), arrowConfig);
        return this;
    }

    public SequenceBuilder AddSequence(SequenceOptions sequenceOptions, Action<ArrowOptions>? arrowConfig = null)
    {
        var sourceToTargetSequence = $"{sequenceOptions.SourceParticipant}_{sequenceOptions.TargetParticipant}";
        var targetToSourceSequence = $"{sequenceOptions.TargetParticipant}_{sequenceOptions.SourceParticipant}";
        if (sequenceOptions.IgnoreForAutomaticArrowDirectionDetection == false)
        {
            if (_sequences.Contains(sourceToTargetSequence) == false &&
                _sequences.Contains(targetToSourceSequence) == false)
            {
                _sequences.Add(sourceToTargetSequence);
                arrowConfig += options =>
                {
                    options.Direction = ArrowDirection.SourceToTarget;
                };
            }
            else
            {
                bool sourceCountExists = _sequences.Count(s => s.Equals(sourceToTargetSequence)) > 0;

                if (sourceCountExists)
                {
                    _sequences.Add(sourceToTargetSequence);
                    arrowConfig += options =>
                    {
                        options.Direction = _sequences.Count(s => s.Equals(sourceToTargetSequence)) % 2 == 0
                        ? ArrowDirection.TargetToSource
                        : ArrowDirection.SourceToTarget;
                    };
                }
                else
                {
                    _sequences.Add(targetToSourceSequence);
                }
            }
        }
        AddEntry(GetSequence(sequenceOptions.SourceParticipant, sequenceOptions.TargetParticipant, sequenceOptions.Description, arrowConfig));
        return this;
    }

    public SequenceBuilder AddParticipant(string participantName, string alias)
    {
        var participant = ParticipantBuilder.CreateParticipant(participantName, alias);
        AddEntry(participant.Build());
        return this;
    }

    public SequenceBuilder AddParticipant(ParticipantBuilder participantBuilder)
    {
        AddEntry(participantBuilder.Build());
        return this;
    }

    public SequenceBuilder AddActor(string participantName, string alias)
    {
        var participant = ParticipantBuilder.CreateActor(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddBoundary(string participantName, string alias)
    {
        var participant = ParticipantBuilder.CreateBoundary(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddControl(string participantName, string alias)
    {
        var participant = ParticipantBuilder.CreateControl(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddEntity(string participantName, string alias)
    {
        var participant = ParticipantBuilder.CreateEntity(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddDatabase(string participantName, string alias)
    {
        var participant = ParticipantBuilder.CreateDatabase(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddCollections(string participantName, string alias)
    {
        var participant = ParticipantBuilder.CreateCollections(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddQueue(string participantName, string alias)
    {
        var participant = ParticipantBuilder.CreateQueue(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddAutoNumber(Action<AutoNumberOptions>? config = null)
    {
        AddEntry(new AutoNumberBuilder().Build(config));
        return this;
    }

    public void AddNote(string note, NotePosition position)
    {
        AddEntry($"note {position.ToString().ToLower()}");
        AddEntry(note);
        AddEntry("end note");
    }

    internal string Build()
    {
        var stringBuilder = new StringBuilder();
        foreach (string value in Statements.Values)
        {
            stringBuilder.AppendLine(value);
        }

        return stringBuilder.ToString().TrimEnd();
    }

    private static string GetSequence(string sourceParticipant, string targetParticipant, string? sequenceDescription, Action<ArrowOptions>? arrowConfig = null)
    {
        var sourceParticipantStatement = GetParticipantStatement(sourceParticipant);
        var targetParticipantStatement = GetParticipantStatement(targetParticipant);
        return AppendDescription($"{sourceParticipantStatement} {GetArrow(arrowConfig)} {targetParticipantStatement}", sequenceDescription);
    }

    private static string GetParticipantStatement(string participant)
    {
        if (participant.Split(' ').Length > 1)
        {
            string[] parts = participant.Split("as");
            return $"\"{parts[0].TrimEnd()}\" as {parts[1].TrimStart()}";
        }

        return participant.All(char.IsLetterOrDigit) 
            ? participant 
            : $"\"{participant}\"";
    }

    private static string GetArrow(Action<ArrowOptions>? arrowConfig = null) 
        => new ArrowStatementBuilder().Build(arrowConfig);

    private static string AppendDescription(string transition, string? sequenceDescription)
        => string.IsNullOrWhiteSpace(sequenceDescription) ? transition : $"{transition} : {sequenceDescription}";
}