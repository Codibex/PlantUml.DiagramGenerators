namespace PlantUml.DiagramGenerators.Uml;

public class StatusNotesBuilder
{
    private readonly StatusOptions _statusOptions;
    private readonly StatusNoteOptions _noteOptions;

    public StatusNotesBuilder(StatusOptions statusOptions)
    {
        _statusOptions = statusOptions;
        _noteOptions = statusOptions.NoteOptions!;
    }

    public string Build()
    {
        string[] lines = _noteOptions.Note.Split(Environment.NewLine);
        var note = $"note {GetPositionStatement()} of {_statusOptions!.GetStatusName()}";
        if (lines.Length == 1)
        {
            return $"{note} : {_noteOptions.Note}";
        }

        return $"{note}{Environment.NewLine}{_noteOptions.Note}{Environment.NewLine}end note";
    }

    private string GetPositionStatement()
        => _noteOptions.Position switch
        {
            NotePosition.Left => "left",
            NotePosition.Top => "top",
            NotePosition.Right => "right",
            NotePosition.Bottom => "bottom",
            _ => throw new ArgumentOutOfRangeException(nameof(_noteOptions.Position), _noteOptions.Position, null)
        };
}