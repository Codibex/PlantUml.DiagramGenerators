using System.Text;

namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceBuilder : UmlBuilder
{
    private readonly IList<string> _sequences = new List<string>();
    public SequenceBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public SequenceBuilder AddSequence(string sourceParticipant, string targetParticipant,
        string? sequenceDescription = null, ArrowOptions? arrowOptions = null)
    {
        AddSequence(new Sequence(sourceParticipant, targetParticipant, sequenceDescription), arrowOptions);
        return this;
    }

    public SequenceBuilder AddSequence(Sequence sequence, ArrowOptions? arrowOptions = null)
    {
        var currentArrowOptions = arrowOptions ?? new ArrowOptions();

        var sourceToTargetSequence = $"{sequence.SourceParticipant}_{sequence.TargetParticipant}";
        var targetToSourceSequence = $"{sequence.TargetParticipant}_{sequence.SourceParticipant}";
        if (sequence.IgnoreForAutomaticArrowDirectionDetection == false)
        {
            if (_sequences.Contains(sourceToTargetSequence) == false &&
                _sequences.Contains(targetToSourceSequence) == false)
            {
                _sequences.Add(sourceToTargetSequence);
                currentArrowOptions.Direction = ArrowDirection.SourceToTarget;
            }
            else
            {
                bool sourceCountExists = _sequences.Count(s => s.Equals(sourceToTargetSequence)) > 0;

                if (sourceCountExists)
                {
                    _sequences.Add(sourceToTargetSequence);
                    currentArrowOptions.Direction = _sequences.Count(s => s.Equals(sourceToTargetSequence)) % 2 == 0
                        ? ArrowDirection.TargetToSource
                        : ArrowDirection.SourceToTarget;
                }
                else
                {
                    _sequences.Add(targetToSourceSequence);
                }
            }
        }

        if (sequence.AutoNumber is not null)
        {
            AddEntry(sequence.AutoNumber.GetStatement());
        }
        AddEntry(GetSequence(sequence.SourceParticipant, sequence.TargetParticipant, sequence.Description, currentArrowOptions));
        return this;
    }

    public SequenceBuilder AddParticipant(string participantName, string alias)
    {
        var participant = Participant.CreateParticipant(participantName, alias);
        AddEntry(participant.GetStatement());
        return this;
    }

    public SequenceBuilder AddParticipant(Participant participant)
    {
        AddEntry(participant.GetStatement());
        return this;
    }

    public SequenceBuilder AddActor(string participantName, string alias)
    {
        var participant = Participant.CreateActor(participantName, alias);
        AddEntry(participant.GetStatement());
        return this;
    }

    public SequenceBuilder AddBoundary(string participantName, string alias)
    {
        var participant = Participant.CreateBoundary(participantName, alias);
        AddEntry(participant.GetStatement());
        return this;
    }

    public SequenceBuilder AddControl(string participantName, string alias)
    {
        var participant = Participant.CreateControl(participantName, alias);
        AddEntry(participant.GetStatement());
        return this;
    }

    public SequenceBuilder AddEntity(string participantName, string alias)
    {
        var participant = Participant.CreateEntity(participantName, alias);
        AddEntry(participant.GetStatement());
        return this;
    }

    public SequenceBuilder AddDatabase(string participantName, string alias)
    {
        var participant = Participant.CreateDatabase(participantName, alias);
        AddEntry(participant.GetStatement());
        return this;
    }

    public SequenceBuilder AddCollections(string participantName, string alias)
    {
        var participant = Participant.CreateCollections(participantName, alias);
        AddEntry(participant.GetStatement());
        return this;
    }

    public SequenceBuilder AddQueue(string participantName, string alias)
    {
        var participant = Participant.CreateQueue(participantName, alias);
        AddEntry(participant.GetStatement());
        return this;
    }

    private static string GetSequence(string sourceParticipant, string targetParticipant, string? sequenceDescription, ArrowOptions arrowOptions)
    {
        var sourceParticipantStatement = GetParticipantStatement(sourceParticipant);
        var targetParticipantStatement = GetParticipantStatement(targetParticipant);
        return AppendDescription($"{sourceParticipantStatement} {GetArrow(arrowOptions)} {targetParticipantStatement}", sequenceDescription);
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