namespace PlantUml.DiagramGenerators.Uml.State;

public class StateTransitionUmlBuilder : UmlBuilder
{
    public StateTransitionUmlBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public StateTransitionUmlBuilder AddStartTransition(string stateName, string? transitionDescription = null, Action<ArrowOptions>? arrowConfig = null)
    {
        var stateOptions = new StateOptions(stateName);
        AddStartTransition(stateOptions, transitionDescription, arrowConfig);
        return this;
    }

    public StateTransitionUmlBuilder AddStartTransition(StateOptions stateOptions, string? transitionDescription, Action<ArrowOptions>? arrowConfig)
    {
        var builder = new StateTransitionStatementBuilder(new StateTransitionOptions(stateOptions,
            null, transitionDescription, arrowConfig));
        string statement = builder.Build(config =>
        {
            config.TransitionType = TransitionType.Start;
        });

        AddEntry(statement);
        AddNote(stateOptions);
        return this;
    }

    public StateTransitionUmlBuilder AddStateTransition(string sourceStateName, string targetStateName, string? transitionDescription = null, Action<ArrowOptions>? arrowConfig = null, TransitionNoteOptions? noteOptions = null)
    {
        var sourceStateOptions = new StateOptions(sourceStateName);
        var targetStateOptions = new StateOptions(targetStateName);

        AddStateTransition(sourceStateOptions, targetStateOptions, transitionDescription, arrowConfig, noteOptions);
        return this;
    }

    public StateTransitionUmlBuilder AddStateTransition(StateOptions sourceStateOptions, StateOptions targetStateOptions, string? transitionDescription = null, Action<ArrowOptions>? arrowConfig = null, TransitionNoteOptions? noteOptions = null)
    {
        var builder = new StateTransitionStatementBuilder(new StateTransitionOptions(sourceStateOptions,
            targetStateOptions, transitionDescription, arrowConfig));
        string statement = builder.Build(config =>
        {
            config.TransitionType = TransitionType.Standard;
        });
        AddEntry(statement);

        AddNote(sourceStateOptions);
        AddNote(targetStateOptions);
        AddNote(noteOptions);
        return this;
    }

    public StateTransitionUmlBuilder AddFinalTransition(string stateName, string? transitionDescription = null, Action<ArrowOptions>? arrowConfig = null)
    {
        var stateOptions = new StateOptions(stateName);

        var builder = new StateTransitionStatementBuilder(new StateTransitionOptions(stateOptions,
            null, transitionDescription, arrowConfig));
        string statement = builder.Build(config =>
        {
            config.TransitionType = TransitionType.Final;
        });

        AddEntry(statement);
        return this;
    }

    public StateTransitionUmlBuilder AddSubState(string stateName, Action<StateTransitionUmlBuilder> subStateBuildAction)
    {
        var stateOptions = new StateOptions(stateName);
        AddSubState(stateOptions, subStateBuildAction);
        return this;
    }

    public StateTransitionUmlBuilder AddSubState(StateOptions stateOptions, Action<StateTransitionUmlBuilder> subStateBuildAction)
    {
        var builder = new StateTransitionUmlBuilder(NestingDepth + 1);
        subStateBuildAction.Invoke(builder);

        string statement = new StateStatementBuilder(stateOptions)
            .Build(config =>
            {
                config.AddStateStatement = true;
            });

        AddEntry($"{statement} {{");
        AddEntry(builder.Build(), true);
        AddEntry("}");
        return this;
    }

    public StateTransitionUmlBuilder AddState(StateOptions stateOptions)
    {
        string statement = new StateStatementBuilder(stateOptions)
            .Build(config =>
            {
                config.AddStateStatement = true;
            });

        AddEntry(statement);
        AddNote(stateOptions);
        return this;
    }

    public StateTransitionUmlBuilder AddConcurrentSeparator(ConcurrentSeparator concurrentSeparator = ConcurrentSeparator.Horizontal)
    {
        string separator = concurrentSeparator switch
        {
            ConcurrentSeparator.Horizontal => "--",
            ConcurrentSeparator.Vertical => "||",
            _ => throw new ArgumentOutOfRangeException(nameof(concurrentSeparator), concurrentSeparator, null)
        };
        AddEntry(separator);
        return this;
    }

    public void AddNote(NoteOptions noteOptions)
    {
        string statement = new NoteStatementBuilder(noteOptions).Build();
        AddEntry(statement);
    }

    private void AddNote(StateOptions stateOptions)
    {
        if (stateOptions.NoteOptions is null)
        {
            return;
        }

        AddEntry(new StateNoteStatementBuilder(stateOptions).Build());
    }

    private void AddNote(TransitionNoteOptions? noteOptions)
    {
        if (noteOptions is null)
        {
            return;
        }

        AddEntry(new TransitionNoteStatementBuilder(noteOptions).Build());
    }
}