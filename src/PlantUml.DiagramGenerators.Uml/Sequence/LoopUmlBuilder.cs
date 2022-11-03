namespace PlantUml.DiagramGenerators.Uml.Sequence;

/// <summary>
/// Builds the loop uml part
/// </summary>
internal class LoopUmlBuilder : UmlBuilder
{
    internal LoopUmlBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    internal LoopUmlBuilder AddLoopBody(int times, Action<SequenceUmlBuilder> umlBuilderAction)
    {
        var sequenceUmlBuilder = new SequenceUmlBuilder(1);
        umlBuilderAction.Invoke(sequenceUmlBuilder);
        string statement = sequenceUmlBuilder.Build();

        AddEntry($"loop {times} times");
        AddEntry(statement);
        AddEntry("end");

        return this;
    }
}