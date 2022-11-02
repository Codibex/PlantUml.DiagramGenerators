namespace PlantUml.DiagramGenerators.Uml.Status;

public class StatusTransitionUmlBuilder : UmlBuilder
{
    public StatusTransitionUmlBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public StatusTransitionUmlBuilder AddStartTransition(string statusName, string? transitionDescription = null, Action<ArrowOptions>? arrowConfig = null)
    {
        var statusOptions = new StatusOptions(statusName);
        AddStartTransition(statusOptions, transitionDescription, arrowConfig);
        return this;
    }

    public StatusTransitionUmlBuilder AddStartTransition(StatusOptions statusOptions, string? transitionDescription, Action<ArrowOptions>? arrowConfig)
    {
        var builder = new StatusTransitionStatementBuilder(new StatusTransitionOptions(statusOptions,
            null, transitionDescription, arrowConfig));
        string statement = builder.Build(config =>
        {
            config.TransitionType = TransitionType.Start;
        });

        AddEntry(statement);
        AddNote(statusOptions);
        return this;
    }

    public StatusTransitionUmlBuilder AddStatusTransition(string sourceStatusName, string targetStatusName, string? transitionDescription = null, Action<ArrowOptions>? arrowConfig = null, TransitionNoteOptions? noteOptions = null)
    {
        var sourceStatusOptions = new StatusOptions(sourceStatusName);
        var targetStatusOptions = new StatusOptions(targetStatusName);

        AddStatusTransition(sourceStatusOptions, targetStatusOptions, transitionDescription, arrowConfig, noteOptions);
        return this;
    }

    public StatusTransitionUmlBuilder AddStatusTransition(StatusOptions sourceStatusOptions, StatusOptions targetStatusOptions, string? transitionDescription = null, Action<ArrowOptions>? arrowConfig = null, TransitionNoteOptions? noteOptions = null)
    {
        var builder = new StatusTransitionStatementBuilder(new StatusTransitionOptions(sourceStatusOptions,
            targetStatusOptions, transitionDescription, arrowConfig));
        string statement = builder.Build(config =>
        {
            config.TransitionType = TransitionType.Standard;
        });
        AddEntry(statement);

        AddNote(sourceStatusOptions);
        AddNote(targetStatusOptions);
        AddNote(noteOptions);
        return this;
    }

    public StatusTransitionUmlBuilder AddFinalTransition(string statusName, string? transitionDescription = null, Action<ArrowOptions>? arrowConfig = null)
    {
        var statusOptions = new StatusOptions(statusName);

        var builder = new StatusTransitionStatementBuilder(new StatusTransitionOptions(statusOptions,
            null, transitionDescription, arrowConfig));
        string statement = builder.Build(config =>
        {
            config.TransitionType = TransitionType.Final;
        });

        AddEntry(statement);
        return this;
    }

    public StatusTransitionUmlBuilder AddSubStatus(string statusName, Action<StatusTransitionUmlBuilder> subStatusBuildAction)
    {
        var statusOptions = new StatusOptions(statusName);
        AddSubStatus(statusOptions, subStatusBuildAction);
        return this;
    }

    public StatusTransitionUmlBuilder AddSubStatus(StatusOptions statusOptions, Action<StatusTransitionUmlBuilder> subStatusBuildAction)
    {
        var builder = new StatusTransitionUmlBuilder(NestingDepth + 1);
        subStatusBuildAction.Invoke(builder);

        string statusString = new StatusStatementBuilder(statusOptions)
            .Build(config =>
            {
                config.AddStateStatement = true;
            });

        AddEntry($"{statusString} {{");
        AddEntry(builder.Build(), true);
        AddEntry("}");
        return this;
    }

    public StatusTransitionUmlBuilder AddStatus(StatusOptions statusOptions)
    {
        string status = new StatusStatementBuilder(statusOptions)
            .Build(config =>
            {
                config.AddStateStatement = true;
            });

        AddEntry(status);
        AddNote(statusOptions);
        return this;
    }

    public StatusTransitionUmlBuilder AddConcurrentSeparator(ConcurrentSeparator concurrentSeparator = ConcurrentSeparator.Horizontal)
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
        string note = new NoteStatementBuilder(noteOptions).Build();
        AddEntry(note);
    }

    private void AddNote(StatusOptions statusOptions)
    {
        if (statusOptions.NoteOptions is null)
        {
            return;
        }

        AddEntry(new StatusNoteStatementBuilder(statusOptions).Build());
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