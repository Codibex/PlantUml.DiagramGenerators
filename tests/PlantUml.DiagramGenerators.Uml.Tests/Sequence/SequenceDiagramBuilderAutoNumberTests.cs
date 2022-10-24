using FluentAssertions;
using PlantUml.DiagramGenerators.Uml.Sequence;

namespace PlantUml.DiagramGenerators.Uml.Tests.Sequence;

public class SequenceDiagramBuilderAutoNumberTests
{
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
