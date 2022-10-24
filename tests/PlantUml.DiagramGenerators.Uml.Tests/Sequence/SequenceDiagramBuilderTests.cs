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
                LineStyle = ArrowLineStyle.Dotted
            })
            .AddSequence("Alice", "Bob", "Another authentication Request")
            .AddSequence("Alice", "Bob", "Another authentication Response", new ArrowOptions
            {
                LineStyle = ArrowLineStyle.Dotted
            })
            .Build();

        const string expected = @"@startuml
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
            .AddParticipant("Participant", "Foo")
            .AddActor("Actor", "Foo1")
            .AddBoundary("Boundary", "Foo2")
            .AddControl("Control", "Foo3")
            .AddEntity("Entity", "Foo4")
            .AddDatabase("Database", "Foo5")
            .AddCollections("Collections", "Foo6")
            .AddQueue("Queue", "Foo7")
            .AddSequence("Foo", "Foo1", "To actor")
            .AddSequence("Foo", "Foo2", "To boundary")
            .AddSequence("Foo", "Foo3", "To control")
            .AddSequence("Foo", "Foo4", "To entity")
            .AddSequence("Foo", "Foo5", "To database")
            .AddSequence("Foo", "Foo6", "To collections")
            .AddSequence("Foo", "Foo7", "To queue")
            .Build();

        const string expected = @"@startuml
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

    [Fact]
    public void Build_Participant_WithColor()
    {
        string uml = new SequenceDiagramBuilder()
            .AddParticipant(Participant.CreateActor("Bob").WithColor("#red"))
            .AddParticipant(Participant.CreateParticipant("Alice"))
            .AddParticipant(Participant.CreateParticipant("I have a really\\nlong name", "L").WithColor("#99FF99"))
            .AddSequence("Alice", "Bob", "Authentication Request")
            .AddSequence("Bob", "Alice", "Authentication Response")
            .AddSequence("Bob", "L", "Log transaction")
            .Build();

        const string expected = @"@startuml
actor Bob #red
participant Alice
participant ""I have a really\nlong name"" as L #99FF99
Alice -> Bob : Authentication Request
Bob -> Alice : Authentication Response
Bob -> L : Log transaction
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Order()
    {
        string uml = new SequenceDiagramBuilder()
            .AddParticipant(Participant.CreateParticipant("Last").WithOrder(30))
            .AddParticipant(Participant.CreateParticipant("Middle").WithOrder(20))
            .AddParticipant(Participant.CreateParticipant("First").WithOrder(10))
            .Build();

        const string expected = @"@startuml
participant Last order 30
participant Middle order 20
participant First order 10
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Participant_Multiline()
    {
        string uml = new SequenceDiagramBuilder()
            .AddParticipant(Participant.CreateParticipant("Participant").WithMultilineDeclaration(@"=Title
----
""SubTitle"""))
            .AddParticipant(Participant.CreateParticipant("Bob"))
            .AddSequence("Participant", "Bob")
            .Build();

        const string expected = @"@startuml
participant Participant [
	=Title
	----
	""SubTitle""
]
participant Bob
Participant -> Bob
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Participant_NonLetters()
    {
        string uml = new SequenceDiagramBuilder()
            .AddSequence("Alice", "Bob()", "Hello")
            .AddSequence("Bob()", "This is very\\nlong as Long")
            .AddSequence("Long", "Bob()", "ok", arrowOptions: new ArrowOptions
            {
                LineStyle = ArrowLineStyle.Dotted
            })
            .Build();

        const string expected = @"@startuml
Alice -> ""Bob()"" : Hello
""Bob()"" -> ""This is very\nlong"" as Long
Long --> ""Bob()"" : ok
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Message_To_Self()
    {
        string uml = new SequenceDiagramBuilder()
            .AddSequence("Alice", "Alice", "This is a signal to self.\\nIt also demonstrates\\nmultiline \\ntext")
            .Build();

        const string expected = @"@startuml
Alice -> Alice : This is a signal to self.\nIt also demonstrates\nmultiline \ntext
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Text_Alignment()
    {
        string uml = new SequenceDiagramBuilder()
            .AddSequence("Bob", "Alice", "Request")
            .AddSequence("Alice", "Bob", "Response")
            .Build(options =>
            {
                options.AddOptions(SkinParameter.SequenceMessageAlignment(SequenceMessageAlignment.Right));
            });

        const string expected = @"@startuml
skinparam sequenceMessageAlign right
Bob -> Alice : Request
Alice -> Bob : Response
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Response_Message_Below_Arrow()
    {
        string uml = new SequenceDiagramBuilder()
            .AddSequence("Bob", "Alice", "hello")
            .AddSequence("Alice", "Bob", "ok")
            .Build(options =>
            {
                options.AddOptions(SkinParameter.ResponseMessageBelowArrow(true));
            });

        const string expected = @"@startuml
skinparam responseMessageBelowArrow true
Bob -> Alice : hello
Alice -> Bob : ok
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Arrow_Color()
    {
        string uml = new SequenceDiagramBuilder()
            .AddSequence("Bob", "Alice", "hello", arrowOptions: new ArrowOptions
            {
                Color = "#red"
            })
            .AddSequence("Alice", "Bob", "ok", arrowOptions: new ArrowOptions
            {
                Color = "#0000FF",
                LineStyle = ArrowLineStyle.Dotted
            })
            .Build();

        const string expected = @"@startuml
Bob -[#red]> Alice : hello
Alice -[#0000FF]-> Bob : ok
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_AutoNumber()
    {
        string uml = new SequenceDiagramBuilder()
            .AddAutoNumber()
            .AddSequence(new Uml.Sequence.Sequence("Bob", "Alice", "Authentication Request"))
            .AddSequence("Bob", "Alice", "Authentication Response")
            .Build();

        const string expected = @"@startuml
autonumber
Bob -> Alice : Authentication Request
Bob <- Alice : Authentication Response
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_AutoNumber_With_StartNumber()
    {
        string uml = new SequenceDiagramBuilder()
            .AddAutoNumber()
            .AddSequence(new Uml.Sequence.Sequence("Bob", "Alice", "Authentication Request"))
            .AddSequence("Bob", "Alice", "Authentication Response")
            .AddAutoNumber(startNumber: 15.ToString())
            .AddSequence(new Uml.Sequence.Sequence("Bob", "Alice", "Another authentication Request"))
            .AddSequence("Bob", "Alice", "Another authentication Response")
            .AddAutoNumber(startNumber: 40.ToString(), increment: 10.ToString())
            .AddSequence(new Uml.Sequence.Sequence("Bob", "Alice", "Yet another authentication Request"))
            .AddSequence("Bob", "Alice", "Yet another authentication Response")
            .Build();

        const string expected = @"@startuml
autonumber
Bob -> Alice : Authentication Request
Bob <- Alice : Authentication Response
autonumber 15
Bob -> Alice : Another authentication Request
Bob <- Alice : Another authentication Response
autonumber 40 10
Bob -> Alice : Yet another authentication Request
Bob <- Alice : Yet another authentication Response
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_AutoNumber_With_Style()
    {
        string uml = new SequenceDiagramBuilder()
            .AddAutoNumber(style: "<b>[000]")
            .AddSequence(new Uml.Sequence.Sequence("Bob", "Alice", "Authentication Request"))
            .AddSequence("Bob", "Alice", "Authentication Response")
            .AddAutoNumber(startNumber: 15.ToString(), style: "<b>(<u>##</u>)")
            .AddSequence(new Uml.Sequence.Sequence("Bob", "Alice", "Another authentication Request"))
            .AddSequence("Bob", "Alice", "Another authentication Response")
            .AddAutoNumber(startNumber: 40.ToString(), increment: 10.ToString(), style: "<font color=red><b>Message 0  ")
            .AddSequence(new Uml.Sequence.Sequence("Bob", "Alice", "Yet another authentication Request"))
            .AddSequence("Bob", "Alice", "Yet another authentication Response")
            .Build();

        const string expected = @"@startuml
autonumber ""<b>[000]""
Bob -> Alice : Authentication Request
Bob <- Alice : Authentication Response
autonumber 15 ""<b>(<u>##</u>)""
Bob -> Alice : Another authentication Request
Bob <- Alice : Another authentication Response
autonumber 40 10 ""<font color=red><b>Message 0  ""
Bob -> Alice : Yet another authentication Request
Bob <- Alice : Yet another authentication Response
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_AutoNumber_Stop_Resume()
    {
        string uml = new SequenceDiagramBuilder()
            .AddAutoNumber(startNumber: 10.ToString(), increment: 10.ToString(), style: "<b>[000]")
            .AddSequence(new Uml.Sequence.Sequence("Bob", "Alice", "Authentication Request"))
            .AddSequence("Bob", "Alice", "Authentication Response")
            .AddAutoNumber(@break: AutoNumberBreak.Stop)
            .AddSequence(new Uml.Sequence.Sequence("Bob", "Alice", "dummy", ignoreForAutomaticArrowDirectionDetection: true))
            .AddAutoNumber(style: "<font color=red><b>Message 0  ", @break: AutoNumberBreak.Resume)
            .AddSequence(new Uml.Sequence.Sequence("Bob", "Alice", "Yet another authentication Request"))
            .AddSequence("Bob", "Alice", "Yet another authentication Response")
            .AddAutoNumber(@break: AutoNumberBreak.Stop)
            .AddSequence(new Uml.Sequence.Sequence("Bob", "Alice", "dummy", ignoreForAutomaticArrowDirectionDetection: true))
            .AddAutoNumber(increment: 1.ToString(), style: "<font color=blue><b>Message 0  ", @break: AutoNumberBreak.Resume)
            .AddSequence(new Uml.Sequence.Sequence("Bob", "Alice", "Yet another authentication Request"))
            .AddSequence("Bob", "Alice", "Yet another authentication Response")
            .Build();

        const string expected = @"@startuml
autonumber 10 10 ""<b>[000]""
Bob -> Alice : Authentication Request
Bob <- Alice : Authentication Response
autonumber stop
Bob -> Alice : dummy
autonumber resume ""<font color=red><b>Message 0  ""
Bob -> Alice : Yet another authentication Request
Bob <- Alice : Yet another authentication Response
autonumber stop
Bob -> Alice : dummy
autonumber resume 1 ""<font color=blue><b>Message 0  ""
Bob -> Alice : Yet another authentication Request
Bob <- Alice : Yet another authentication Response
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_AutoNumber_Mixed_Format()
    {
        string uml = new SequenceDiagramBuilder()
            .AddAutoNumber(startNumber: "1.1.1")
            .AddSequence(new Uml.Sequence.Sequence("Alice", "Bob", "Authentication request"))
            .AddSequence("Bob", "Alice", "Response", new ArrowOptions
            {
                LineStyle = ArrowLineStyle.Dotted
            })
            .AddAutoNumber(increment: "A")
            .AddSequence(new Uml.Sequence.Sequence("Alice", "Bob", "Another authentication request"))
            .AddSequence("Bob", "Alice", "Response", new ArrowOptions
            {
                LineStyle = ArrowLineStyle.Dotted
            })
            .AddAutoNumber(increment: "B")
            .AddSequence(new Uml.Sequence.Sequence("Alice", "Bob", "Another authentication request"))
            .AddSequence("Bob", "Alice", "Response", new ArrowOptions
            {
                LineStyle = ArrowLineStyle.Dotted
            })
            .AddAutoNumber(increment: "A")
            .AddSequence(new Uml.Sequence.Sequence("Alice", "Bob", "Another authentication request"))
            .AddAutoNumber(increment: "B")
            .AddSequence("Bob", "Alice", "Response", new ArrowOptions
            {
                LineStyle = ArrowLineStyle.Dotted
            })
            .Build();

        const string expected = @"@startuml
autonumber 1.1.1
Alice -> Bob : Authentication request
Bob --> Alice : Response
autonumber inc A
Alice -> Bob : Another authentication request
Bob --> Alice : Response
autonumber inc B
Alice -> Bob : Another authentication request
Bob --> Alice : Response
autonumber inc A
Alice -> Bob : Another authentication request
autonumber inc B
Bob --> Alice : Response
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_AutoNumber_Variable()
    {
        string uml = new SequenceDiagramBuilder()
            .AddAutoNumber(startNumber: 10.ToString())
            .AddSequence(new Uml.Sequence.Sequence("Alice", "Bob"))
            .AddNote(@"the <U+0025>autonumber<U+0025> works everywhere.
Here, its value is ** %autonumber% **", NotePosition.Right)
            .AddSequence("Bob", "Alice", "//This is the response %autonumber%//", new ArrowOptions
            {
                LineStyle = ArrowLineStyle.Dotted
            })
            .Build();

        const string expected = @"@startuml
autonumber 10
Alice -> Bob
note right
the <U+0025>autonumber<U+0025> works everywhere.
Here, its value is ** %autonumber% **
end note
Bob --> Alice : //This is the response %autonumber%//
@enduml";

        uml.Should().Be(expected);
    }
}
