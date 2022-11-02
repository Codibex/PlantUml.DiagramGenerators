using System.Text;

namespace PlantUml.DiagramGenerators.Uml.State;

public class StateDiagramBuilder : UmlDiagramBuilderBase<StateTransitionUmlBuilder, StateDiagramOptions>
{
    public StateDiagramBuilder() : base(new StateTransitionUmlBuilder(0), StateDiagramOptions.Default)
    {
        
    }

    public StateDiagramBuilder AddStartTransition(string stateName, string? description = null, Action<ArrowOptions>? arrowConfig = null)
    {
        UmlBuilder.AddStartTransition(stateName, description, arrowConfig);
        return this;
    }

    public StateDiagramBuilder AddStartTransition(StateOptions stateOptions, string? description = null, Action<ArrowOptions>? arrowConfig = null)
    {
        UmlBuilder.AddStartTransition(stateOptions, description, arrowConfig);
        return this;
    }

    public StateDiagramBuilder AddStateTransition(string sourceStateName, string targetStateName, string? description = null, Action<ArrowOptions>? arrowConfig = null, TransitionNoteOptions? noteOptions = null)
    {
        UmlBuilder.AddStateTransition(sourceStateName, targetStateName, description, arrowConfig, noteOptions);
        return this;
    }

    public StateDiagramBuilder AddStateTransition(StateOptions sourceStateOptions, StateOptions targetStateOptions, string? description = null, Action<ArrowOptions>? arrowConfig = null, TransitionNoteOptions? noteOptions = null)
    {
        UmlBuilder.AddStateTransition(sourceStateOptions, targetStateOptions, description, arrowConfig, noteOptions);
        return this;
    }

    public StateDiagramBuilder AddFinalTransition(string stateName, string? description = null, Action<ArrowOptions>? arrowConfig = null)
    {
        UmlBuilder.AddFinalTransition(stateName, description, arrowConfig);
        return this;
    }

    public StateDiagramBuilder AddSubState(string stateName, Action<StateTransitionUmlBuilder> subStateBuildAction)
    {
        UmlBuilder.AddSubState(stateName, subStateBuildAction);
        return this;
    }

    public StateDiagramBuilder AddSubState(StateOptions stateOptions, Action<StateTransitionUmlBuilder> subStateBuildAction)
    {
        UmlBuilder.AddSubState(stateOptions, subStateBuildAction);
        return this;
    }

    public StateDiagramBuilder AddState(StateOptions options)
    {
        UmlBuilder.AddState(options);
        return this;
    }

    public StateDiagramBuilder AddNote(NoteOptions noteOptions)
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