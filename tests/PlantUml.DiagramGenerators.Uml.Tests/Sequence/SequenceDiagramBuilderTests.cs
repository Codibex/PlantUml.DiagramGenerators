using FluentAssertions;
using PlantUml.DiagramGenerators.Uml.Options;
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
                options.AddOptions(SequenceSkinParameter.SequenceMessageAlignment(SequenceMessageAlignment.Right));
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
                options.AddOptions(SequenceSkinParameter.ResponseMessageBelowArrow(true));
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

    [Fact]
    public void Build_Grouping_Message()
    {
        string uml = new SequenceDiagramBuilder()
            .AddSequence(new SequenceOptions("Alice", "Bob", "Authentication Request"))
            .AddMessageGroup(gb =>
            {
                gb.AddAlt("successful case", builder =>
                    {

                        builder.AddSequence(new SequenceOptions("Bob", "Alice", "Authentication Accepted"));
                    })
                    .AddElse("some kind of failure", builder =>
                    {
                        builder.AddSequence(new SequenceOptions("Bob", "Alice", "Authentication Failure"));
                        builder.AddGroup("My own label", subBuilder =>
                        {
                            subBuilder
                                .AddSequence(new SequenceOptions("Alice", "Log", "Log attack start"))
                                .AddLoop(1000, subSubBuilder =>
                                {
                                    subSubBuilder.AddSequence(new SequenceOptions("Alice", "Bob", "DNS Attack"));
                                })
                                .AddSequence(new SequenceOptions("Alice", "Log", "Log attack end", ignoreAutomaticArrowDirectionDetection: true));
                        });
                    })
                    .AddElse("Another type of failure", builder =>
                    {
                        builder.AddSequence(new SequenceOptions("Bob", "Alice", "Please repeat"));
                    })
                    .AddEnd();
            })
            .Build();

        const string expected = @"@startuml
Alice -> Bob : Authentication Request
alt successful case
	Bob -> Alice : Authentication Accepted
else some kind of failure
	Bob -> Alice : Authentication Failure
	group My own label
		Alice -> Log : Log attack start
		loop 1000 times
			Alice -> Bob : DNS Attack
		end
		Alice -> Log : Log attack end
	end
else Another type of failure
	Bob -> Alice : Please repeat
end
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Secondary_Group_Label()
    {
        string uml = new SequenceDiagramBuilder()
            .AddSequence(new SequenceOptions("Alice", "Bob", "Authentication Request"))
            .AddSequence(new SequenceOptions("Bob", "Alice", "Authentication Failure"))
            .AddGroup("My own label [My own label 2]", builder =>
            {
                builder.AddSequence(new SequenceOptions("Alice", "Log", "Log attack start"))
                    .AddLoop(1000, subBuilder =>
                    {
                        subBuilder.AddSequence(new SequenceOptions("Alice", "Bob", "DNS Attack"));
                    })
                    .AddSequence(new SequenceOptions("Alice", "Log", "Log attack end", ignoreAutomaticArrowDirectionDetection: true));
            })
            .Build();

        const string expected = @"@startuml
Alice -> Bob : Authentication Request
Bob -> Alice : Authentication Failure
group My own label [My own label 2]
	Alice -> Log : Log attack start
	loop 1000 times
		Alice -> Bob : DNS Attack
	end
	Alice -> Log : Log attack end
end
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Divider()
    {
        var alice = ParticipantOptions.CreateParticipant("Alice");
        var bob = ParticipantOptions.CreateParticipant("Bob");

        string uml = new SequenceDiagramBuilder()
            .AddDivider("Initialization")
            .AddSequence(alice, bob, "Authentication Request")
            .AddSequence(bob, alice, "Authentication Response", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .AddDivider("Repetition")
            .AddSequence(alice, bob, "Another authentication Request")
            .AddSequence(alice, bob, "another authentication Response", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .Build();

        const string expected = @"@startuml
== Initialization ==
Alice -> Bob : Authentication Request
Bob --> Alice : Authentication Response
== Repetition ==
Alice -> Bob : Another authentication Request
Alice <-- Bob : another authentication Response
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Reference()
    {
        var alice = ParticipantOptions.CreateParticipant("Alice");
        var bob = ParticipantOptions.CreateActor("Bob");

        string uml = new SequenceDiagramBuilder()
            .AddParticipant(alice)
            .AddParticipant(bob)
            .AddReference("init", alice, bob)
            .AddSequence(alice, bob, "hello")
            .AddReference(@"This can be on
several lines", bob)
            .Build();

        const string expected = @"@startuml
participant Alice
actor Bob
ref over Alice, Bob
	init
end ref
Alice -> Bob : hello
ref over Bob
	This can be on
	several lines
end ref
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Delay()
    {
        var alice = ParticipantOptions.CreateParticipant("Alice");
        var bob = ParticipantOptions.CreateActor("Bob");

        string uml = new SequenceDiagramBuilder()
            .AddSequence(alice, bob, "Authentication Request")
            .AddDelay()
            .AddSequence(bob, alice, "Authentication Response", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .AddDelay("5 minutes later")
            .AddSequence(bob, alice, "Good Bye !", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .Build();

        const string expected = @"@startuml
Alice -> Bob : Authentication Request
...
Bob --> Alice : Authentication Response
...5 minutes later...
Bob --> Alice : Good Bye !
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Spacing()
    {
        var alice = ParticipantOptions.CreateParticipant("Alice");
        var bob = ParticipantOptions.CreateActor("Bob");

        string uml = new SequenceDiagramBuilder()
            .AddSequence(alice, bob, "message 1")
            .AddSequence(bob, alice, "ok", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .AddSpace()
            .AddSequence(alice, bob, "message 2")
            .AddSequence(bob, alice, "ok", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .AddSpace(45)
            .AddSequence(alice, bob, "message 3")
            .AddSequence(bob, alice, "ok", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .Build();

        const string expected = @"@startuml
Alice -> Bob : message 1
Bob --> Alice : ok
|||
Alice -> Bob : message 2
Bob --> Alice : ok
||45||
Alice -> Bob : message 3
Bob --> Alice : ok
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Box()
    {
        var alice1 = ParticipantOptions.CreateParticipant("Alice1");
        var alice2 = ParticipantOptions.CreateParticipant("Alice2");

        var bob1 = ParticipantOptions.CreateParticipant("Bob1");
        var bob2 = ParticipantOptions.CreateParticipant("Bob2");

        var @out = ParticipantOptions.CreateParticipant("Out");

        string uml = new SequenceDiagramBuilder()
            .AddBox("Foo1", builder =>
            {
                builder.AddParticipant(alice1)
                    .AddParticipant(alice2);
            })
            .AddBox("Foo2", builder =>
            {
                builder.AddParticipant(bob1)
                    .AddParticipant(bob2);
            })
            .AddSequence(alice1, bob1, "hello")
            .AddSequence(alice1, @out, "out")
            .Build(config =>
            {
                config.AddOptions(SequenceSkinParameter.ParticipantPadding(20),
                    SequenceSkinParameter.BoxPadding(10));
            });

        const string expected = @"@startuml
skinparam ParticipantPadding 20
skinparam BoxPadding 10
box ""Foo1""
	participant Alice1
	participant Alice2
end box
box ""Foo2""
	participant Bob1
	participant Bob2
end box
Alice1 -> Bob1 : hello
Alice1 -> Out : out
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_SkinParameter_HandWritten()
    {
        string uml = new SequenceDiagramBuilder()
            .AddParticipant(ParticipantOptions.CreateActor("User"))
            .AddParticipant(ParticipantOptions.CreateParticipant("First Class", "A"))
            .AddParticipant(ParticipantOptions.CreateParticipant("Second Class", "B"))
            .AddParticipant(ParticipantOptions.CreateParticipant("Last Class", "C"))
            .AddSequence("User", "A", "DoWork")
            .Activate("A")
            .AddSequence("A", "B", "Create Request")
            .Activate("B")
            .AddSequence("B", "C", "DoWork")
            .Activate("C")
            .AddSequence("C", "B", "WorkDone", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .Destroy("C")
            .AddSequence("B", "A", "Request Created", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .Deactivate("B")
            .AddSequence("A", "User", "Done", config =>
            {
                config.LineStyle = ArrowLineStyle.Dotted;
            })
            .Deactivate("A")
            .Build(config =>
            {
                config.AddOptions(SkinParameter.BackgroundColor("#EEEBDC"),
                    SkinParameter.HandWritten(),
                    SkinParameter.ArrowColor("DeepSkyBlue"),
                    SequenceSkinParameter.ActorBorderColor("DeepSkyBlue"),
                    SequenceSkinParameter.LifeLineBorderColor("blue"),
                    SequenceSkinParameter.LifeLineBackgroundColor("#A9DCDF"),
                    SequenceSkinParameter.ParticipantBorderColor("DeepSkyBlue"),
                    SequenceSkinParameter.ParticipantBackgroundColor("DodgerBlue"),
                    SequenceSkinParameter.ParticipantFontName("Impact"),
                    SequenceSkinParameter.ParticipantFontSize(17),
                    SequenceSkinParameter.ParticipantFontColor("#A9DCDF"),
                    SequenceSkinParameter.ActorBackgroundColor("aqua"),
                    SequenceSkinParameter.ActorFontColor("DeepSkyBlue"),
                    SequenceSkinParameter.ActorFontSize(17),
                    SequenceSkinParameter.ActorFontName("Aapex"));
            });

        const string expected = @"@startuml
skinparam backgroundColor #EEEBDC
skinparam handwritten true
skinparam ArrowColor DeepSkyBlue
skinparam ActorBorderColor DeepSkyBlue
skinparam sequenceLifeLineBorderColor blue
skinparam sequenceLifeLineBackgroundColor #A9DCDF
skinparam sequenceParticipantBorderColor DeepSkyBlue
skinparam sequenceParticipantBackgroundColor DodgerBlue
skinparam sequenceParticipantFontName Impact
skinparam sequenceParticipantFontSize 17
skinparam sequenceParticipantFontColor #A9DCDF
skinparam sequenceActorBackgroundColor aqua
skinparam sequenceActorFontColor DeepSkyBlue
skinparam sequenceActorFontSize 17
skinparam sequenceActorFontName Aapex
actor User
participant ""First Class"" as A
participant ""Second Class"" as B
participant ""Last Class"" as C
User -> A : DoWork
activate A
A -> B : Create Request
activate B
B -> C : DoWork
activate C
C --> B : WorkDone
destroy C
B --> A : Request Created
deactivate B
A --> User : Done
deactivate A
@enduml";

        uml.Should().Be(expected);
    }
}
