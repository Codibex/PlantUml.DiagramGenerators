using System.Text;

namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceDiagramBuilder
{
    private readonly SequenceBuilder _builder = new(0);

    public SequenceDiagramBuilder AddSequence(string sourceParticipant, string targetParticipant, string? sequenceDescription = null, ArrowOptions? arrowOptions = null)
    {
        _builder.AddSequence(sourceParticipant, targetParticipant, sequenceDescription, arrowOptions);
        return this;
    }

    public string Build(Action<SequenceDiagramOptions>? options = null)
    {
        var currentOptions = SequenceDiagramOptions.Default;
        options?.Invoke(currentOptions);

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(UmlConstants.START_TAG);

        if (currentOptions.HideEmptyDescriptionTag)
        {
            stringBuilder.AppendLine(UmlConstants.HIDE_EMPTY_DESCRIPTION_TAG);
        }

        foreach (string additionalOption in currentOptions.AdditionalOptions)
        {
            stringBuilder.AppendLine(additionalOption);
        }

        stringBuilder.AppendLine(_builder.Build());
        stringBuilder.AppendLine(UmlConstants.END_TAG);

        return stringBuilder.ToString().TrimEnd();
    }
}

public class SequenceDiagramOptions
{
    public static SequenceDiagramOptions Default => new(true, Array.Empty<string>());

    public bool HideEmptyDescriptionTag { get; set; }
    public string[] AdditionalOptions { get; set; }

    private SequenceDiagramOptions(bool hideEmptyDescriptionTag, params string[] additionalOptions)
    {
        HideEmptyDescriptionTag = hideEmptyDescriptionTag;
        AdditionalOptions = additionalOptions;
    }
}