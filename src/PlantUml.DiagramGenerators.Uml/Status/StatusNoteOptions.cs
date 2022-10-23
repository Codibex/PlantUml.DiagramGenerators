namespace PlantUml.DiagramGenerators.Uml.Status;

public class StatusNoteOptions : NoteOptionsBase
{
    public NotePosition Position { get; }

    public StatusNoteOptions(string note, NotePosition position) : base(note)
    {
        Position = position;
    }
}