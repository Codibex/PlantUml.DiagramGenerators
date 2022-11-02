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

    [Fact]
    public void Build_Notes_Shape()
    {
        var caller = ParticipantOptions.CreateParticipant("caller");
        var server = ParticipantOptions.CreateParticipant("server");

        string uml = new SequenceDiagramBuilder()
            .AddSequence(caller, server, "conReq")
            .AddNote(new NoteOptions("idle", NotePosition.Over).WithParticipant(caller)
                .WithShape(NoteShape.Hexagonal))
            .AddSequence(caller, server, "conConf")
            .AddNote(new NoteOptions(@" """"r"""" as rectangle
 """"h"""" as hexagon", NotePosition.Over)
                .WithParticipant(server)
                .WithShape(NoteShape.Rectangle))
            .AddNote(new NoteOptions(@" this is
 on several
 lines", NotePosition.Over)
                .WithParticipant(server)
                .WithShape(NoteShape.Rectangle))
            .AddNote(new NoteOptions(@" this is
 on several
 lines", NotePosition.Over)
                .WithParticipant(caller)
                .WithShape(NoteShape.Hexagonal))
            .Build();

        const string expected = @"@startuml
caller -> server : conReq
hnote over caller
idle
endhnote
caller <- server : conConf
rnote over server
 """"r"""" as rectangle
 """"h"""" as hexagon
endrnote
rnote over server
 this is
 on several
 lines
endrnote
hnote over caller
 this is
 on several
 lines
endhnote
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Notes_Across()
    {
        var alice = ParticipantOptions.CreateParticipant("Alice");
        var bob = ParticipantOptions.CreateParticipant("Bob");
        var charlie = ParticipantOptions.CreateParticipant("Charlie");

        string uml = new SequenceDiagramBuilder()
            .AddSequence(alice, bob, "m1")
            .AddSequence(bob, charlie, "m2")
            .AddNote(new NoteOptions(@"Old method for note over all part. with:
""""note over //FirstPart, LastPart//"""".", NotePosition.Over)
                .WithParticipant(alice)
                .WithParticipant(charlie))
            .AddNote(new NoteOptions(@"New method with:
""""note across""""", NotePosition.Across))
            .AddSequence(bob, alice)
            .AddNote(new NoteOptions(@"Note across all part.", NotePosition.Across)
                .WithShape(NoteShape.Hexagonal))
            .Build();

        const string expected = @"@startuml
Alice -> Bob : m1
Bob -> Charlie : m2
note over Alice, Charlie
Old method for note over all part. with:
""""note over //FirstPart, LastPart//"""".
end note
note across
New method with:
""""note across""""
end note
Bob -> Alice
hnote across
Note across all part.
endhnote
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Notes_Aligned()
    {
        var alice = ParticipantOptions.CreateParticipant("Alice");
        var bob = ParticipantOptions.CreateParticipant("Bob");

        string uml = new SequenceDiagramBuilder()
            .AddNote(new NoteOptions(@"initial state of Alice", NotePosition.Over)
                .WithParticipant(alice))
            .AddNote(new NoteOptions(@"initial state of Bob", NotePosition.Over)
                .WithParticipant(bob)
                .WithAlignment())
            .AddSequence(bob, alice, "hello")
            .Build();

        const string expected = @"@startuml
note over Alice
initial state of Alice
end note
/ note over Bob
initial state of Bob
end note
Bob -> Alice : hello
@enduml";

        uml.Should().Be(expected);
    }
}