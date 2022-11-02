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
            .AddSequence(new Uml.Sequence.SequenceOptions("Bob", "Alice", "Authentication Request"))
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
            .AddSequence(new Uml.Sequence.SequenceOptions("Bob", "Alice", "Authentication Request"))
            .AddSequence("Bob", "Alice", "Authentication Response")
            .AddAutoNumber(config =>
            {
                config.StartNumber = 15.ToString();
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Bob", "Alice", "Another authentication Request"))
            .AddSequence("Bob", "Alice", "Another authentication Response")
            .AddAutoNumber(config =>
            {
                config.StartNumber = 40.ToString();
                config.Increment = 10.ToString();
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Bob", "Alice", "Yet another authentication Request"))
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
            .AddAutoNumber(config =>
            {
                config.Style = "<b>[000]";
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Bob", "Alice", "Authentication Request"))
            .AddSequence("Bob", "Alice", "Authentication Response")
            .AddAutoNumber(config =>
            {
                config.StartNumber = 15.ToString();
                config.Style = "<b>(<u>##</u>)";
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Bob", "Alice", "Another authentication Request"))
            .AddSequence("Bob", "Alice", "Another authentication Response")
            .AddAutoNumber(config =>
            {
                config.StartNumber = 40.ToString();
                config.Increment = 10.ToString();
                config.Style = "<font color=red><b>Message 0  ";
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Bob", "Alice", "Yet another authentication Request"))
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
            .AddAutoNumber(config =>
            {
                config.StartNumber = 10.ToString();
                config.Increment = 10.ToString();
                config.Style = "<b>[000]";
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Bob", "Alice", "Authentication Request"))
            .AddSequence("Bob", "Alice", "Authentication Response")
            .AddAutoNumber(config =>
            {
                config.AutoNumberBreak = AutoNumberBreak.Stop;
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Bob", "Alice", "dummy", ignoreForAutomaticArrowDirectionDetection: true))
            .AddAutoNumber(config =>
            {
                config.Style = "<font color=red><b>Message 0  ";
                config.AutoNumberBreak = AutoNumberBreak.Resume;
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Bob", "Alice", "Yet another authentication Request"))
            .AddSequence("Bob", "Alice", "Yet another authentication Response")
            .AddAutoNumber(config =>
                {
                    config.AutoNumberBreak = AutoNumberBreak.Stop;
                })
            .AddSequence(new Uml.Sequence.SequenceOptions("Bob", "Alice", "dummy", ignoreForAutomaticArrowDirectionDetection: true))
            .AddAutoNumber(config =>
            {
                config.Increment = 1.ToString();
                config.Style = "<font color=blue><b>Message 0  ";
                config.AutoNumberBreak = AutoNumberBreak.Resume;
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Bob", "Alice", "Yet another authentication Request"))
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
            .AddAutoNumber(config =>
            {
                config.StartNumber = "1.1.1";
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Alice", "Bob", "Authentication request"))
            .AddSequence("Bob", "Alice", "Response", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .AddAutoNumber(config =>
            {
                config.Increment = "A";
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Alice", "Bob", "Another authentication request"))
            .AddSequence("Bob", "Alice", "Response", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .AddAutoNumber(config =>
            {
                config.Increment = "B";
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Alice", "Bob", "Another authentication request"))
            .AddSequence("Bob", "Alice", "Response", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .AddAutoNumber(config =>
            {
                config.Increment = "A";
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Alice", "Bob", "Another authentication request"))
            .AddAutoNumber(config =>
            {
                config.Increment = "B";
            })
            .AddSequence("Bob", "Alice", "Response", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
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
            .AddAutoNumber(config =>
            {
                config.StartNumber = 10.ToString();
            })
            .AddSequence(new Uml.Sequence.SequenceOptions("Alice", "Bob"))
            .AddNote(@"the <U+0025>autonumber<U+0025> works everywhere.
Here, its value is ** %autonumber% **", NotePosition.Right)
            .AddSequence("Bob", "Alice", "//This is the response %autonumber%//", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
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
