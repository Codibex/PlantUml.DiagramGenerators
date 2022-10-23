using FluentAssertions;
using PlantUml.DiagramGenerators.Uml.Sequence;

namespace PlantUml.DiagramGenerators.Uml.Tests.Sequence;
public class SequenceDiagramBuilderTests
{
    [Fact]
    public void Build_Simple()
    {
        var uml = new SequenceDiagramBuilder()
            .AddSequence("Alice", "Bob", "Authentication Request")
            .AddSequence("Bob", "Alice", "Authentication Response", new ArrowOptions
            {
                Style = ArrowStyle.Dotted
            })
            .AddSequence("Alice", "Bob", "Another authentication Request")
            .AddSequence("Alice", "Bob", "Another authentication Response", new ArrowOptions
            {
                Style = ArrowStyle.Dotted
            })
            .Build();

        const string expected = @"@startuml
hide empty description
Alice -> Bob : Authentication Request
Bob --> Alice : Authentication Response
Alice -> Bob : Another authentication Request
Alice <-- Bob : Another authentication Response
@enduml";

        uml.Should().Be(expected);
    }
}
