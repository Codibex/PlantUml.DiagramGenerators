using System.Text;

namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceBuilder : UmlBuilder
{
    private readonly List<string> _sequenceKeys = new();

    internal IEnumerable<string> SequenceKeys => _sequenceKeys.AsReadOnly();

    public SequenceBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public void AddSequenceKey(string sequenceKey)
        => _sequenceKeys.Add(sequenceKey);

    public SequenceBuilder AddSequence(string sourceParticipant, string targetParticipant,
        string? sequenceDescription = null, Action<ArrowOptions>? arrowConfig = null)
    {
        AddSequence(new SequenceOptions(sourceParticipant, targetParticipant, sequenceDescription), arrowConfig);
        return this;
    }

    public SequenceBuilder AddSequence(SequenceOptions sequenceOptions, Action<ArrowOptions>? arrowConfig = null)
    {
        AddEntry(new SequenceStatementBuilder(sequenceOptions).Build(this, arrowConfig));
        return this;
    }

    public SequenceBuilder AddParticipant(string participantName, string alias)
    {
        var participantOptions = ParticipantOptions.CreateParticipant(participantName, alias);
        AddEntry(new ParticipantStatementBuilder(participantOptions).Build());
        return this;
    }

    public SequenceBuilder AddParticipant(ParticipantOptions participantOptions)
    {
        AddEntry(new ParticipantStatementBuilder(participantOptions).Build());
        return this;
    }

    public SequenceBuilder AddActor(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateActor(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddBoundary(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateBoundary(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddControl(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateControl(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddEntity(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateEntity(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddDatabase(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateDatabase(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddCollections(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateCollections(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddQueue(string participantName, string alias)
    {
        var participant = ParticipantOptions.CreateQueue(participantName, alias);
        AddParticipant(participant);
        return this;
    }

    public SequenceBuilder AddAutoNumber(Action<AutoNumberOptions>? config = null)
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

    internal string Build()
    {
        var stringBuilder = new StringBuilder();
        foreach (string value in Statements.Values)
        {
            stringBuilder.AppendLine(value);
        }

        return stringBuilder.ToString().TrimEnd();
    }
}