namespace PlantUml.DiagramGenerators.Uml.Status;

internal class StatusTransitionOptions
{
    public StatusOptions SourceStatusOptions { get; }
    public StatusOptions? TargetStatusOptions { get; }
    public string? TransitionDescription { get; }
    public Action<ArrowOptions>? ArrowConfig { get; }
    public TransitionNoteOptions? NoteOptions { get; }
    public TransitionType TransitionType { get; set; } = TransitionType.Start;

    public StatusTransitionOptions(StatusOptions sourceStatusOptions, StatusOptions? targetStatusOptions, string? transitionDescription = null, Action<ArrowOptions>? arrowConfig = null)
    {
        SourceStatusOptions = sourceStatusOptions;
        TargetStatusOptions = targetStatusOptions;
        TransitionDescription = transitionDescription;
        ArrowConfig = arrowConfig;
    }
}