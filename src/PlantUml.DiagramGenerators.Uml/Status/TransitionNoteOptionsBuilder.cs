namespace PlantUml.DiagramGenerators.Uml.Status;

public class TransitionNoteOptionsBuilder
{
    private readonly TransitionNoteOptions _noteOptions;

    public TransitionNoteOptionsBuilder(TransitionNoteOptions noteOptions)
    {
        _noteOptions = noteOptions;
    }

    public string Build()
        => $"note on link{Environment.NewLine}{_noteOptions.Note}{Environment.NewLine}end note";
}