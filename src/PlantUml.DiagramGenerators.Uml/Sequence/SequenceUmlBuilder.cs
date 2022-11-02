namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceUmlBuilder : UmlBuilder
{
    private readonly List<string> _sequenceKeys = new();

    internal IEnumerable<string> SequenceKeys => _sequenceKeys.AsReadOnly();

    public SequenceUmlBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public void AddSequenceKey(string sequenceKey)
        => _sequenceKeys.Add(sequenceKey);

    public SequenceUmlBuilder AddSequence(string sourceParticipant, string targetParticipant,
        string? sequenceDescription = null, Action<ArrowOptions>? arrowConfig = null)
    {
        AddSequence(new SequenceOptions(sourceParticipant, targetParticipant, sequenceDescription), arrowConfig);
        return this;
    }

    public SequenceUmlBuilder AddSequence(SequenceOptions sequenceOptions, Action<ArrowOptions>? arrowConfig = null)
    {
        AddEntry(new SequenceStatementBuilder(sequenceOptions).Build(this, arrowConfig));
        return this;
    }

    public SequenceUmlBuilder AddParticipant(string participantName, string alias)
    {
        var participantOptions = ParticipantOptions.CreateParticipant(participantName, alias);
        AddEntry(new ParticipantStatementBuilder(participantOptions).Build());
        return this;
    }

    public SequenceUmlBuilder AddParticipant(ParticipantOptions participantOptions)
    {
        AddEntry(new ParticipantStatementBuilder(participantOptions).Build());
        return this;
    }

    public SequenceUmlBuilder AddActor(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateActor(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceUmlBuilder AddBoundary(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateBoundary(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceUmlBuilder AddControl(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateControl(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceUmlBuilder AddEntity(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateEntity(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceUmlBuilder AddDatabase(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateDatabase(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceUmlBuilder AddCollections(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateCollections(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceUmlBuilder AddQueue(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateQueue(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceUmlBuilder AddAutoNumber(Action<AutoNumberOptions>? config = null)
    {
        AddEntry(new AutoNumberStatementBuilder().Build(config));
        return this;
    }

    public void AddNote(string note, NotePosition position)
    {
        AddEntry($"note {position.ToString().ToLower()}");
        AddEntry(note);
        AddEntry("end note");
    }
}