using System.Text;

namespace PlantUml.DiagramGenerators.Uml.Status;

public class StatusDiagramBuilder
{
    private readonly StatusTransitionBuilder _builder = new(0);

    public StatusDiagramBuilder AddStartTransition(string statusName, string? description = null, ArrowOptions? arrowOptions = null)
    {
        _builder.AddStartTransition(statusName, description, arrowOptions);
        return this;
    }

    public StatusDiagramBuilder AddStartTransition(StatusOptions statusOptions, string? description = null, ArrowOptions? arrowOptions = null)
    {
        _builder.AddStartTransition(statusOptions, description, arrowOptions);
        return this;
    }

    public StatusDiagramBuilder AddStatusTransition(string sourceStatusName, string targetStatusName, string? description = null, ArrowOptions? arrowOptions = null, TransitionNoteOptions? noteOptions = null)
    {
        _builder.AddStatusTransition(sourceStatusName, targetStatusName, description, arrowOptions, noteOptions);
        return this;
    }

    public StatusDiagramBuilder AddStatusTransition(StatusOptions sourceStatusOptions, StatusOptions targetStatusOptions, string? description = null, ArrowOptions? arrowOptions = null, TransitionNoteOptions? noteOptions = null)
    {
        _builder.AddStatusTransition(sourceStatusOptions, targetStatusOptions, description, arrowOptions, noteOptions);
        return this;
    }

    public StatusDiagramBuilder AddFinalTransition(string statusName, string? description = null, ArrowOptions? arrowOptions = null)
    {
        _builder.AddFinalTransition(statusName, description, arrowOptions);
        return this;
    }

    public StatusDiagramBuilder AddSubStatus(string statusName, Action<StatusTransitionBuilder> subStatusBuildAction)
    {
        _builder.AddSubStatus(statusName, subStatusBuildAction);
        return this;
    }

    public StatusDiagramBuilder AddSubStatus(StatusOptions statusOptions, Action<StatusTransitionBuilder> subStatusBuildAction)
    {
        _builder.AddSubStatus(statusOptions, subStatusBuildAction);
        return this;
    }

    public StatusDiagramBuilder AddStatus(StatusOptions options)
    {
        _builder.AddStatus(options);
        return this;
    }

    public StatusDiagramBuilder AddNote(NoteOptions noteOptions)
    {
        _builder.AddNote(noteOptions);
        return this;
    }

    public string Build(Action<StatusDiagramOptions>? options = null)
    {
        var currentOptions = StatusDiagramOptions.Default;
        options?.Invoke(currentOptions);

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(START_TAG);

        if (currentOptions.HideEmptyDescriptionTag)
        {
            stringBuilder.AppendLine(HIDE_EMPTY_DESCRIPTION_TAG);
        }

        foreach (string additionalOption in currentOptions.AdditionalOptions)
        {
            stringBuilder.AppendLine(additionalOption);
        }

        stringBuilder.AppendLine(_builder.Build());
        stringBuilder.AppendLine(END_TAG);

        return stringBuilder.ToString().TrimEnd();
    }
}