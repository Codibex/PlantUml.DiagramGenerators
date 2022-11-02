using FluentAssertions;
using PlantUml.DiagramGenerators.Uml.Sequence;

namespace PlantUml.DiagramGenerators.Uml.Tests.Sequence;

public class SequenceDiagramBuilderNotesTests
{
    [Fact]
    public void Build_Notes_On_Messages()
    {
        string uml = new SequenceDiagramBuilder()
            .AddSequence("Alice", "Bob", "hello")
            .AddNote("this is a first note", NotePosition.Left)
            .AddSequence("Bob", "Alice", "ok")
            .AddNote("this is another note", NotePosition.Right)
            .AddSequence("Bob", "Bob", "I am thinking")
            .AddNote(@"a note
can also be defined
on several lines", NotePosition.Left)
            .Build();

        const string expected = @"@startuml
Alice -> Bob : hello
note left
this is a first note
end note
Bob -> Alice : ok
note right
this is another note
end note
Bob -> Bob : I am thinking
note left
a note
can also be defined
on several lines
end note
@enduml";

        uml.Should().Be(expected);
    }
}