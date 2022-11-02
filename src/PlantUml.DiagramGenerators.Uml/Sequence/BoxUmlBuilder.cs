namespace PlantUml.DiagramGenerators.Uml.Sequence;

internal class BoxUmlBuilder : UmlBuilder
{
    public BoxUmlBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public BoxUmlBuilder AddBox(string title, Action<SequenceUmlBuilder> umlBuilderAction)
    {
        var sequenceUmlBuilder = new SequenceUmlBuilder(1);
        umlBuilderAction.Invoke(sequenceUmlBuilder);
        string statement = sequenceUmlBuilder.Build();

        AddEntry($"box \"{title}\"");
        AddEntry(statement);
        AddEntry("end box");

        return this;
    }
}