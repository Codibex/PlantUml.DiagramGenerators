namespace PlantUml.DiagramGenerators.Uml.State;

public class StateNoteOptions : NoteOptionsBase
{
    public NotePosition Position { get; }

    public StateNoteOptions(string note, NotePosition position) : base(note)
    {
        Position = position;
    }
}