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

    [Fact]
    public void Build_Some_Other_Notes()
    {
        var alice = ParticipantOptions.CreateParticipant("Alice")
            .WithNote(@"This is displayed
left of Alice.", NotePosition.Left, "#aqua")
            .WithNote("This is displayed right of Alice.", NotePosition.Right)
            .WithNote("This is displayed over Alice.", NotePosition.Over);

        var bob = ParticipantOptions.CreateParticipant("Bob");

        string uml = new SequenceDiagramBuilder()
            .AddParticipant(alice)
            .AddParticipant(bob)
            .AddNote(new NoteOptions(@"This is displayed
over Bob and Alice.", NotePosition.Over)
                .WithColor("#FFAAAA")
                .WithParticipant(alice)
                .WithParticipant(bob))
            .AddNote(new NoteOptions(@"This is yet another
example of
a long note.", NotePosition.Over)
                .WithParticipant(bob)
                .WithParticipant(alice))
            .Build();

        const string expected = @"@startuml
participant Alice
note left of Alice #aqua
This is displayed
left of Alice.
end note
note right of Alice
This is displayed right of Alice.
end note
note over Alice
This is displayed over Alice.
end note
participant Bob
note over Alice, Bob #FFAAAA
This is displayed
over Bob and Alice.
end note
note over Bob, Alice
This is yet another
example of
a long note.
end note
@enduml";

        uml.Should().Be(expected);
    }
}