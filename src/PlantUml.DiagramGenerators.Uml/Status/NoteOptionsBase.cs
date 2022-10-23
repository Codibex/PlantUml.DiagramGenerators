namespace PlantUml.DiagramGenerators.Uml.Status;

public abstract class NoteOptionsBase
{
    public string Note { get; }

    protected NoteOptionsBase(string note)
    {
        Note = note;
    }
}