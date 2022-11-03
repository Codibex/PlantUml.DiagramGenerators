using FluentAssertions;
using PlantUml.DiagramGenerators.Uml.State;

namespace PlantUml.DiagramGenerators.Uml.Tests.State;

public class StateDiagramBuilderNotesTests
{
    [Fact]
    public void Build_Note()
    {
        string uml = new StateDiagramBuilder()
            .AddStartTransition("Active")
            .AddStateTransition(new StateOptions("Active")
            {
                NoteOptions = new StateNoteOptions("this is a short\\nnote", NotePosition.Left)
            },
                new StateOptions("Inactive")
                {
                    NoteOptions = new StateNoteOptions(@"A note can also
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
        string uml = new StateDiagramBuilder()
            .AddState(new StateOptions("foo"))
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
        string uml = new StateDiagramBuilder()
            .AddStartTransition("State1")
            .AddStateTransition("State1", "State2", noteOptions: new TransitionNoteOptions("  this is a state-transition note"))
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
        string uml = new StateDiagramBuilder()
            .AddStartTransition(new StateOptions("NotShooting")
            {
                NoteOptions = new StateNoteOptions("This is a note on a composite", NotePosition.Right)
            })
            .AddSubState(new StateOptions("Not Shooting State")
            {
                Alias = "NotShooting"
            }, b =>
            {
                b.AddState(new StateOptions("Idle mode")
                {
                    Alias = "Idle"
                })
                    .AddState(new StateOptions("Configuring mode")
                    {
                        Alias = "Configuring"
                    })
                    .AddStartTransition("Idle")
                    .AddStateTransition("Idle", "Configuring", "EvConfig")
                    .AddStateTransition("Configuring", "Idle", "EvConfig");
            })
            .Build(options =>
            {
                options.HideEmptyDescriptionTag.IsActive = false;
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
