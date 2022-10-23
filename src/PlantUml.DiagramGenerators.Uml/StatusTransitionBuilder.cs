using System.Text;

namespace PlantUml.DiagramGenerators.Uml;

public class StatusTransitionBuilder : UmlBuilder
{
    private readonly int _nestingDepth;
    private readonly SortedList<int, string> _list = new();

    public StatusTransitionBuilder(int nestingDepth)
    {
        _nestingDepth = nestingDepth;
    }

    public StatusTransitionBuilder AddStartTransition(string statusName, string? transitionDescription = null, ArrowOptions? arrowOptions = null)
    {
        var statusOptions = new StatusOptions(statusName)
        {
            AddStateStatement = false
        };
        AddStartTransition(statusOptions, transitionDescription, arrowOptions);
        return this;
    }

    public StatusTransitionBuilder AddStartTransition(StatusOptions statusOptions, string? transitionDescription, ArrowOptions? arrowOptions)
    {
        statusOptions.AddStateStatement = false;
        AddEntry(GetStartTransition(statusOptions, transitionDescription, arrowOptions));
        AddNote(statusOptions);
        return this;
    }

    public StatusTransitionBuilder AddStatusTransition(string sourceStatusName, string targetStatusName, string? transitionDescription = null, ArrowOptions? arrowOptions = null, TransitionNoteOptions? noteOptions = null)
    {
        var sourceStatusOptions = new StatusOptions(sourceStatusName)
        {
            AddStateStatement = false
        };
        var targetStatusOptions = new StatusOptions(targetStatusName)
        {
            AddStateStatement = false
        };
        AddEntry(GetStatusTransition(sourceStatusOptions, targetStatusOptions, transitionDescription, arrowOptions));
        AddNote(noteOptions);
        return this;
    }

    public StatusTransitionBuilder AddStatusTransition(StatusOptions sourceStatusOptions, StatusOptions targetStatusOptions, string? transitionDescription = null, ArrowOptions? arrowOptions = null, TransitionNoteOptions? noteOptions = null)
    {
        sourceStatusOptions.AddStateStatement = false;
        targetStatusOptions.AddStateStatement = false;
        AddEntry(GetStatusTransition(sourceStatusOptions, targetStatusOptions, transitionDescription, arrowOptions));

        AddNote(sourceStatusOptions);
        AddNote(targetStatusOptions);
        AddNote(noteOptions);
        return this;
    }

    public StatusTransitionBuilder AddFinalTransition(string statusName, string? transitionDescription = null, ArrowOptions? arrowOptions = null)
    {
        var statusOptions = new StatusOptions(statusName)
        {
            AddStateStatement = false
        };
        AddEntry(GetFinalTransition(statusOptions, transitionDescription, arrowOptions));
        return this;
    }

    public StatusTransitionBuilder AddSubStatus(string statusName, Action<StatusTransitionBuilder> subStatusBuildAction)
    {
        var statusOptions = new StatusOptions(statusName)
        {
            AddStateStatement = true
        };
        AddSubStatus(statusOptions, subStatusBuildAction);
        return this;
    }

    public StatusTransitionBuilder AddSubStatus(StatusOptions statusOptions, Action<StatusTransitionBuilder> subStatusBuildAction)
    {
        var builder = new StatusTransitionBuilder(_nestingDepth + 1);
        subStatusBuildAction.Invoke(builder);

        string statusString = new StatusBuilder(statusOptions)
            .Build();

        AddEntry($"{statusString} {{");
        _list.Add(_list.Count, builder.Build());
        AddEntry("}");
        return this;
    }

    public StatusTransitionBuilder AddStatus(StatusOptions statusOptions)
    {
        string status = new StatusBuilder(statusOptions)
            .Build();

        AddEntry(status);
        AddNote(statusOptions);
        return this;
    }

    public StatusTransitionBuilder AddConcurrentSeparator(ConcurrentSeparator concurrentSeparator = ConcurrentSeparator.Horizontal)
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
        string note = new NotesBuilder(noteOptions).Build();
        AddEntry(note);
    }

    private void AddNote(StatusOptions statusOptions)
    {
        if (statusOptions.NoteOptions is null)
        {
            return;
        }

        AddEntry(new StatusNotesBuilder(statusOptions).Build());
    }

    private void AddNote(TransitionNoteOptions? noteOptions)
    {
        if (noteOptions is null)
        {
            return;
        }

        AddEntry(new TransitionNoteOptionsBuilder(noteOptions).Build());
    }

    internal string Build()
    {
        var stringBuilder = new StringBuilder();
        foreach (string value in _list.Values)
        {
            stringBuilder.AppendLine(value);
        }

        return stringBuilder.ToString().TrimEnd();
    }

    private void AddEntry(string entry) => _list.Add(_list.Count, $"{GetTabs()}{entry}");

    private string GetTabs() =>
        $"{string.Join("", Enumerable.Range(0, _nestingDepth).Select(_ => "\t"))}";

    private static string GetStartTransition(StatusOptions statusOptions, string? description, ArrowOptions? arrowOptions = null)
    {
        string targetStatusName = new StatusBuilder(statusOptions).Build();
        return AppendDescription($"[*] {GetArrow(arrowOptions)} {targetStatusName}", description);
    }

    private static string GetStatusTransition(StatusOptions sourceStatusOptions, StatusOptions targetStatusOptions, string? transitionDescription, ArrowOptions? arrowOptions = null)
    {
        string sourceStatusName = new StatusBuilder(sourceStatusOptions).Build();
        string targetStatusName = new StatusBuilder(targetStatusOptions).Build();
        return AppendDescription($"{sourceStatusName} {GetArrow(arrowOptions)} {targetStatusName}", transitionDescription);
    }

    private static string GetFinalTransition(StatusOptions statusOptions, string? transitionDescription, ArrowOptions? arrowOptions = null)
    {
        string sourceStatusName = new StatusBuilder(statusOptions).Build();
        return AppendDescription($"{sourceStatusName} {GetArrow(arrowOptions)} [*]", transitionDescription);
    }

    private static string GetArrow(ArrowOptions? arrowOptions = null)
    {
        return new ArrowBuilder(arrowOptions ?? new ArrowOptions()).Build();
    }

    private static string AppendDescription(string transition, string? transitionDescription) 
        => string.IsNullOrWhiteSpace(transitionDescription) ? transition : $"{transition} : {transitionDescription}";
}