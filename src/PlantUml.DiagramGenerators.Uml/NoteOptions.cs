namespace PlantUml.DiagramGenerators.Uml;

public class NoteOptions : NoteOptionsBase
{
    public string Alias { get; }

    public NoteOptions(string note, string alias) : base(note)
    {
        Alias = alias;
    }
}