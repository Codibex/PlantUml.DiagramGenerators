namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class MessageGroupStatementBuilder : UmlBuilder
{
    internal MessageGroupStatementBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public MessageGroupStatementBuilder AddAlt(string message, Action<SequenceUmlBuilder> umlBuilderAction)
    {
        AddStatements("alt", message, umlBuilderAction);
        return this;
    }

    public MessageGroupStatementBuilder AddElse(string message, Action<SequenceUmlBuilder> umlBuilderAction)
    {
        AddStatements("else", message, umlBuilderAction);
        return this;
    }

    public MessageGroupStatementBuilder AddEnd()
    {
        AddEntry("end");
        return this;
    }

    private void AddStatements(string prefix, string message, Action<SequenceUmlBuilder> umlBuilderAction)
    {
        var builder = new SequenceUmlBuilder(NestingDepth + 1);
        umlBuilderAction.Invoke(builder);
        string statement = builder.Build();

        AddEntry($"{prefix} {message}");
        AddEntry(statement);
    }
}