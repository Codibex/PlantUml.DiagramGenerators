namespace PlantUml.DiagramGenerators.Uml.State;

internal class StateTransitionOptions
{
    public StateOptions SourceStateOptions { get; }
    public StateOptions? TargetStateOptions { get; }
    public string? TransitionDescription { get; }
    public Action<ArrowOptions>? ArrowConfig { get; }
    public TransitionNoteOptions? NoteOptions { get; }
    public TransitionType TransitionType { get; set; } = TransitionType.Start;

    public StateTransitionOptions(StateOptions sourceStateOptions, StateOptions? targetStateOptions, string? transitionDescription = null, Action<ArrowOptions>? arrowConfig = null)
    {
        SourceStateOptions = sourceStateOptions;
        TargetStateOptions = targetStateOptions;
        TransitionDescription = transitionDescription;
        ArrowConfig = arrowConfig;
    }
}