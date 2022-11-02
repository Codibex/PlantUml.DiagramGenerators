using System.Text;

namespace PlantUml.DiagramGenerators.Uml.Status;

public class StatusDiagramBuilder : UmlDiagramBuilderBase<StatusTransitionUmlBuilder, StatusDiagramOptions>
{
    public StatusDiagramBuilder() : base(new StatusTransitionUmlBuilder(0), StatusDiagramOptions.Default)
    {
        
    }

    public StatusDiagramBuilder AddStartTransition(string statusName, string? description = null, Action<ArrowOptions>? arrowConfig = null)
    {
        UmlBuilder.AddStartTransition(statusName, description, arrowConfig);
        return this;
    }

    public StatusDiagramBuilder AddStartTransition(StatusOptions statusOptions, string? description = null, Action<ArrowOptions>? arrowConfig = null)
    {
        UmlBuilder.AddStartTransition(statusOptions, description, arrowConfig);
        return this;
    }

    public StatusDiagramBuilder AddStatusTransition(string sourceStatusName, string targetStatusName, string? description = null, Action<ArrowOptions>? arrowConfig = null, TransitionNoteOptions? noteOptions = null)
    {
        UmlBuilder.AddStatusTransition(sourceStatusName, targetStatusName, description, arrowConfig, noteOptions);
        return this;
    }

    public StatusDiagramBuilder AddStatusTransition(StatusOptions sourceStatusOptions, StatusOptions targetStatusOptions, string? description = null, Action<ArrowOptions>? arrowConfig = null, TransitionNoteOptions? noteOptions = null)
    {
        UmlBuilder.AddStatusTransition(sourceStatusOptions, targetStatusOptions, description, arrowConfig, noteOptions);
        return this;
    }

    public StatusDiagramBuilder AddFinalTransition(string statusName, string? description = null, Action<ArrowOptions>? arrowConfig = null)
    {
        UmlBuilder.AddFinalTransition(statusName, description, arrowConfig);
        return this;
    }

    public StatusDiagramBuilder AddSubStatus(string statusName, Action<StatusTransitionUmlBuilder> subStatusBuildAction)
    {
        UmlBuilder.AddSubStatus(statusName, subStatusBuildAction);
        return this;
    }

    public StatusDiagramBuilder AddSubStatus(StatusOptions statusOptions, Action<StatusTransitionUmlBuilder> subStatusBuildAction)
    {
        UmlBuilder.AddSubStatus(statusOptions, subStatusBuildAction);
        return this;
    }

    public StatusDiagramBuilder AddStatus(StatusOptions options)
    {
        UmlBuilder.AddStatus(options);
        return this;
    }

    public StatusDiagramBuilder AddNote(NoteOptions noteOptions)
    {
        UmlBuilder.AddNote(noteOptions);
        return this;
    }
    
    protected override void AddDiagramSpecificOptionsStatements(StringBuilder stringBuilder)
    {
        if (Options.HideEmptyDescriptionTag)
        {
            stringBuilder.AppendLine(UmlConstants.HIDE_EMPTY_DESCRIPTION_TAG);
        }
    }
}