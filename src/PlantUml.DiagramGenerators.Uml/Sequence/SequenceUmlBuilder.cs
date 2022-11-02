namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceUmlBuilder : UmlBuilder
{
    private readonly List<string> _sequenceKeys = new();

    internal IEnumerable<string> SequenceKeys => _sequenceKeys.AsReadOnly();

    internal SequenceUmlBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    internal void AddSequenceKey(string sequenceKey)
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

    public SequenceUmlBuilder AddNote(string note, NotePosition position)
    {
        AddEntry($"note {position.ToString().ToLower()}");
        AddEntry(note);
        AddEntry("end note");
        return this;
    }

    public SequenceUmlBuilder AddPageHeader(string header)
    {
        AddEntry($"header {header}");
        return this;
    }

    public SequenceUmlBuilder AddPageFooter(string footer, bool includePageNumber)
    {
        var pageNumberStatement = includePageNumber ? " %page% of %lastpage%" : string.Empty;
        AddEntry($"footer {footer}{pageNumberStatement}");
        return this;
    }

    public SequenceUmlBuilder AddPageTitle(string title)
    {
        AddEntry($"title {title}");
        return this;
    }

    public SequenceUmlBuilder AddNewPage(string? title = null)
    {
        string statement = string.IsNullOrWhiteSpace(title) ? string.Empty : $" {title}";
        AddEntry($"newpage{statement}");
        return this;
    }

    public SequenceUmlBuilder AddMessageGroup(Action<MessageGroupStatementBuilder> messageBuilderAction)
    {
        var builder = new MessageGroupStatementBuilder(NestingDepth);
        messageBuilderAction.Invoke(builder);
        string statement = builder.Build();
        
        AddEntry(statement);
        return this;
    }

    public SequenceUmlBuilder AddGroup(string groupName, Action<SequenceUmlBuilder> umlBuilderAction)
    {
        var builder = new SequenceUmlBuilder(NestingDepth);
        umlBuilderAction.Invoke(builder);
        string statement = builder.Build();
        
        AddEntry($"group {groupName}");
        AddEntry(statement, ignoreTabs: true);
        AddEntry("end");
        return this;
    }

    public SequenceUmlBuilder AddLoop(int times, Action<SequenceUmlBuilder> umlBuilderAction)
    {
        var loopBuilder = new LoopUmlBuilder(NestingDepth + 1); 
        string loopStatement = loopBuilder.AddLoopBody(times, umlBuilderAction).Build();

        AddEntry(loopStatement, true);
        return this;
    }
}