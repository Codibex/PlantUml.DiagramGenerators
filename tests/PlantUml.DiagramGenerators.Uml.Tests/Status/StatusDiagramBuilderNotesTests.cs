using FluentAssertions;
using PlantUml.DiagramGenerators.Uml.Status;

namespace PlantUml.DiagramGenerators.Uml.Tests.Status;

public class StatusDiagramBuilderNotesTests
{
    [Fact]
    public void Build_Note()
    {
        string uml = new StatusDiagramBuilder()
            .AddStartTransition("Active")
            .AddStatusTransition(new StatusOptions("Active")
            {
                NoteOptions = new StatusNoteOptions("this is a short\\nnote", NotePosition.Left)
            },
                new StatusOptions("Inactive")
                {
                    NoteOptions = new StatusNoteOptions(@"A note can also
be defined on
several lines", NotePosition.Right)
                })
            .Build();

        const string expected = @"@startuml
hide empty description
[*] --> Active
Active --> Inactive
note left of Active : this is a short\nnote
note right of Inactive
A note can also
be defined on
several lines
end note
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Floating_Note()
    {
        string uml = new StatusDiagramBuilder()
            .AddStatus(new StatusOptions("foo"))
            .AddNote(new NoteOptions("This is a floating note", "N1"))
            .Build();

        const string expected = @"@startuml
hide empty description
state foo
note ""This is a floating note"" as N1
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Link_Note()
    {
        string uml = new StatusDiagramBuilder()
            .AddStartTransition("State1")
            .AddStatusTransition("State1", "State2", noteOptions: new TransitionNoteOptions("  this is a state-transition note"))
            .Build();

        const string expected = @"@startuml
hide empty description
[*] --> State1
State1 --> State2
note on link
  this is a state-transition note
end note
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_More_Notes()
    {
        string uml = new StatusDiagramBuilder()
            .AddStartTransition(new StatusOptions("NotShooting")
            {
                NoteOptions = new StatusNoteOptions("This is a note on a composite", NotePosition.Right)
            })
            .AddSubStatus(new StatusOptions("Not Shooting State")
            {
                Alias = "NotShooting"
            }, b =>
            {
                b.AddStatus(new StatusOptions("Idle mode")
                {
                    Alias = "Idle"
                })
                    .AddStatus(new StatusOptions("Configuring mode")
                    {
                        Alias = "Configuring"
                    })
                    .AddStartTransition("Idle")
                    .AddStatusTransition("Idle", "Configuring", "EvConfig")
                    .AddStatusTransition("Configuring", "Idle", "EvConfig");
            })
            .Build(options =>
            {
                options.HideEmptyDescriptionTag = false;
                options.AddOptions("scale 350 width");
            });

        const string expected = @"@startuml
scale 350 width
[*] --> NotShooting
note right of NotShooting : This is a note on a composite
state ""Not Shooting State"" as NotShooting {
	state ""Idle mode"" as Idle
	state ""Configuring mode"" as Configuring
	[*] --> Idle
	Idle --> Configuring : EvConfig
	Configuring --> Idle : EvConfig
}
@enduml";

        uml.Should().Be(expected);
    }
}
