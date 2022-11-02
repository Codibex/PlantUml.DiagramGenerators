namespace PlantUml.DiagramGenerators.Uml.Sequence;

internal class SequenceStatementBuilder
{
    private readonly SequenceOptions _sequenceOptions;

    internal SequenceStatementBuilder(SequenceOptions sequenceOptions)
    {
        _sequenceOptions = sequenceOptions;
    }

    internal string Build(SequenceBuilder sequenceBuilder, Action<ArrowOptions>? arrowConfig)
    {
        var sourceToTargetSequence = $"{_sequenceOptions.SourceParticipant}_{_sequenceOptions.TargetParticipant}";
        var targetToSourceSequence = $"{_sequenceOptions.TargetParticipant}_{_sequenceOptions.SourceParticipant}";
        if (_sequenceOptions.IgnoreForAutomaticArrowDirectionDetection == false)
        {
            if (sequenceBuilder.SequenceKeys.Contains(sourceToTargetSequence) == false &&
                sequenceBuilder.SequenceKeys.Contains(targetToSourceSequence) == false)
            {
                sequenceBuilder.AddSequenceKey(sourceToTargetSequence);
                arrowConfig += options =>
                {
                    options.Direction = ArrowDirection.SourceToTarget;
                };
            }
            else
            {
                bool sourceCountExists = sequenceBuilder.SequenceKeys.Count(s => s.Equals(sourceToTargetSequence)) > 0;

                if (sourceCountExists)
                {
                    sequenceBuilder.AddSequenceKey(sourceToTargetSequence);
                    arrowConfig += options =>
                    {
                        options.Direction = sequenceBuilder.SequenceKeys.Count(s => s.Equals(sourceToTargetSequence)) % 2 == 0
                            ? ArrowDirection.TargetToSource
                            : ArrowDirection.SourceToTarget;
                    };
                }
                else
                {
                    sequenceBuilder.AddSequenceKey(targetToSourceSequence);
                }
            }
        }

        string sourceParticipantStatement = GetParticipantStatement(_sequenceOptions.SourceParticipant);
        string targetParticipantStatement = GetParticipantStatement(_sequenceOptions.TargetParticipant);
        return AppendDescription($"{sourceParticipantStatement} {GetArrow(arrowConfig)} {targetParticipantStatement}", _sequenceOptions.Description);
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