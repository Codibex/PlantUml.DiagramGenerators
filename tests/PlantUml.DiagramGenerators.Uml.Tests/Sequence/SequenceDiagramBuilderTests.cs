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
            .AddSequence("Bob", "Alice", "Authentication Response", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .AddSequence("Alice", "Bob", "Another authentication Request")
            .AddSequence("Alice", "Bob", "Another authentication Response", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
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
            .AddParticipant(ParticipantOptions.CreateActor("Bob").WithColor("#red"))
            .AddParticipant(ParticipantOptions.CreateParticipant("Alice"))
            .AddParticipant(ParticipantOptions.CreateParticipant("I have a really\\nlong name", "L").WithColor("#99FF99"))
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
            .AddParticipant(ParticipantOptions.CreateParticipant("Last").WithOrder(30))
            .AddParticipant(ParticipantOptions.CreateParticipant("Middle").WithOrder(20))
            .AddParticipant(ParticipantOptions.CreateParticipant("First").WithOrder(10))
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
            .AddParticipant(ParticipantOptions.CreateParticipant("Participant").WithMultilineDeclaration(@"=Title
----
""SubTitle"""))
            .AddParticipant(ParticipantOptions.CreateParticipant("Bob"))
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
            .AddSequence("Long", "Bob()", "ok", config =>
            {

                config.LineStyle = ArrowLineStyle.Dotted;
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
            .AddSequence("Bob", "Alice", "hello", config =>
            {
                config.Color = "#red";
            })
            .AddSequence("Alice", "Bob", "ok", config =>
            {
                config.Color = "#0000FF";
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .Build();

        const string expected = @"@startuml
Bob -[#red]> Alice : hello
Alice -[#0000FF]-> Bob : ok
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Page_Title_Header_Footer()
    {
        string uml = new SequenceDiagramBuilder()
            .AddPageHeader("Page Header")
            .AddPageFooter("Page", true)
            .AddPageTitle("Example Title")
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 1"))
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 2", ignoreAutomaticArrowDirectionDetection: true))
            .Build();

        const string expected = @"@startuml
header Page Header
footer Page %page% of %lastpage%
title Example Title
Alice -> Bob : message 1
Alice -> Bob : message 2
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Splitting_Diagrams()
    {
        string uml = new SequenceDiagramBuilder()
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 1"))
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 2", ignoreAutomaticArrowDirectionDetection: true))
            .AddNewPage()
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 3", ignoreAutomaticArrowDirectionDetection: true))
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 4", ignoreAutomaticArrowDirectionDetection: true))
            .AddNewPage("A title for the\\nlast page")
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 5", ignoreAutomaticArrowDirectionDetection: true))
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 6", ignoreAutomaticArrowDirectionDetection: true))
            .Build();

        const string expected = @"@startuml
Alice -> Bob : message 1
Alice -> Bob : message 2
newpage
Alice -> Bob : message 3
Alice -> Bob : message 4
newpage A title for the\nlast page
Alice -> Bob : message 5
Alice -> Bob : message 6
@enduml";

        uml.Should().Be(expected);
    }
}
