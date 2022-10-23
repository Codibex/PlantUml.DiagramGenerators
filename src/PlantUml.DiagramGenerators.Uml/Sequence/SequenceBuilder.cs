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

        AddEntry(GetStatusTransition(sourceParticipant, targetParticipant, sequenceDescription, currentArrowOptions));
        return this;
    }

    private static string GetStatusTransition(string sourceParticipant, string targetParticipant, string? transitionDescription, ArrowOptions arrowOptions)
    {
        return AppendDescription($"{sourceParticipant} {GetArrow(arrowOptions)} {targetParticipant}", transitionDescription);
    }

    private static string GetArrow(ArrowOptions arrowOptions)
    {
        return new ArrowBuilder(arrowOptions).Build();
    }

    private static string AppendDescription(string transition, string? transitionDescription)
        => string.IsNullOrWhiteSpace(transitionDescription) ? transition : $"{transition} : {transitionDescription}";

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
