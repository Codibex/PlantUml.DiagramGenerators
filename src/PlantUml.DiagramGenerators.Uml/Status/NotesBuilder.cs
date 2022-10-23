namespace PlantUml.DiagramGenerators.Uml.Status;

public class NotesBuilder
{
    private readonly NoteOptions _noteOptions;

    public NotesBuilder(NoteOptions noteOptions)
    {
        _noteOptions = noteOptions;
    }

    public string Build()
    {
        bool hasAlias = string.IsNullOrWhiteSpace(_noteOptions.Alias) == false;

        string note = hasAlias
            ? $"\"{_noteOptions.Note}\""
            : _noteOptions.Note;

        string alias = hasAlias
            ? $" as {_noteOptions.Alias}"
            : string.Empty;

        var noteStatement = $"note {note}{alias}";
        return noteStatement;
    }
}