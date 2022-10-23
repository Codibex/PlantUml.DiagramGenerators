using FluentAssertions;
using PlantUml.DiagramGenerators.Uml.Sequence;

namespace PlantUml.DiagramGenerators.Uml.Tests.Sequence;
public class SequenceDiagramBuilderTests
{
    [Fact]
    public void Build_Simple()
    {
        string uml = new SequenceDiagramBuilder()
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

    [Fact]
    public void Build_Declaring_Participant()
    {
        string uml = new SequenceDiagramBuilder()
            .AddParticipant("participant", "Foo")
            .AddActor("actor", "Foo1")
            .AddBoundary("boundary", "Foo2")
            .AddControl("control", "Foo3")
            .AddEntity("entity", "Foo4")
            .AddDatabase("database", "Foo5")
            .AddCollections("collections", "Foo6")
            .AddQueue("queue", "Foo7")
            .AddSequence("Foo", "Foo1", "To actor")
            .AddSequence("Foo", "Foo2", "To boundary")
            .AddSequence("Foo", "Foo3", "To control")
            .AddSequence("Foo", "Foo4", "To entity")
            .AddSequence("Foo", "Foo5", "To database")
            .AddSequence("Foo", "Foo6", "To collections")
            .AddSequence("Foo", "Foo7", "To queue")
            .Build();

        const string expected = @"@startuml
hide empty description
participant Participant as Foo
actor Actor as Foo1
boundary Boundary as Foo2
control Control as Foo3
entity Entity as Foo4
database Database as Foo5
collections Collections as Foo6
queue Queue as Foo7
Foo -> Foo1 : To actor
Foo -> Foo2 : To boundary
Foo -> Foo3 : To control
Foo -> Foo4 : To entity
Foo -> Foo5 : To database
Foo -> Foo6 : To collections
Foo -> Foo7 : To queue
@enduml";

        uml.Should().Be(expected);
    }
}
