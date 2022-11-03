using FluentAssertions;
using PlantUml.DiagramGenerators.Uml.State;

namespace PlantUml.DiagramGenerators.Uml.Tests.State;

public class StateDiagramBuilderTests
{
    [Fact]
    public void Build_SimpleState()
    {
        var builder = new StateDiagramBuilder();
        string uml = builder.AddStartTransition("New", arrowConfig: config => { config.Length = 3; })
            .AddStateTransition("New", "InProgress")
            .AddStateTransition("InProgress", "Completed", arrowConfig: config => { config.Length = 1; })
            .AddFinalTransition("Completed", arrowConfig: config => { config.Length = 6; })
            .Build();

        const string expected = @"@startuml
hide empty description
[*] ---> New
New --> InProgress
InProgress -> Completed
Completed ------> [*]
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_With_SubState()
    {
        var builder = new StateDiagramBuilder();
        string uml = builder.AddStartTransition("New", arrowConfig: config => { config.Length = 3; })
            .AddStateTransition("New", "InProgress")
            .AddStateTransition("InProgress", "Completed", arrowConfig: config => { config.Length = 1; })
            .AddSubState("InProgress", b =>
            {
                b.AddStartTransition("State1")
                    .AddStateTransition("State1", "State2")
                    .AddFinalTransition("State2");
            })
            .AddFinalTransition("Completed", arrowConfig: config => { config.Length = 6; })
            .Build();

        const string expected = @"@startuml
hide empty description
[*] ---> New
New --> InProgress
InProgress -> Completed
state InProgress {
	[*] --> State1
	State1 --> State2
	State2 --> [*]
}
Completed ------> [*]
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_With_SubState_In_SubState()
    {
        var builder = new StateDiagramBuilder();
        string uml = builder.AddStartTransition("New", arrowConfig: config => { config.Length = 3; })
            .AddStateTransition("New", "InProgress")
            .AddStateTransition("InProgress", "Completed", arrowConfig: config => { config.Length = 1; })
            .AddSubState("InProgress", b =>
            {
                b.AddStartTransition("State1")
                    .AddStateTransition("State1", "State2")
                    .AddSubState("State2", b =>
                    {
                        b.AddStartTransition("State6")
                            .AddStateTransition("State6", "State7")
                            .AddFinalTransition("State7");
                    })
                    .AddFinalTransition("State2");
            })
            .AddFinalTransition("Completed", arrowConfig: config => { config.Length = 6; })
            .Build();

        const string expected = @"@startuml
hide empty description
[*] ---> New
New --> InProgress
InProgress -> Completed
state InProgress {
	[*] --> State1
	State1 --> State2
	state State2 {
		[*] --> State6
		State6 --> State7
		State7 --> [*]
	}
	State2 --> [*]
}
Completed ------> [*]
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_CompositeState()
    {
        string uml = new StateDiagramBuilder().AddStartTransition("NotShooting")
            .AddSubState("NotShooting", b =>
            {
                b.AddStartTransition("Idle")
                    .AddStateTransition("Idle", "Configuring", "EvConfig")
                    .AddStateTransition("Configuring", "Idle", "EvConfig");
            })
            .AddSubState("Configuring", b =>
            {
                b.AddStartTransition("NewValueSelection")
                    .AddStateTransition("NewValueSelection", "NewValuePreview", "EvNewValue")
                    .AddStateTransition("NewValuePreview", "NewValueSelection", "EvNewValueRejected")
                    .AddStateTransition("NewValuePreview", "NewValueSelection", "EvNewValueSaved")
                    .AddSubState("NewValuePreview", sb =>
                    {
                        sb.AddStateTransition("State1", "State2");
                    });
            })
            .Build(options =>
            {
                options.HideEmptyDescriptionTag.IsActive = false;
                options.AddOptions("scale 350 width");
            });

        const string expected = @"@startuml
scale 350 width
[*] --> NotShooting
state NotShooting {
	[*] --> Idle
	Idle --> Configuring : EvConfig
	Configuring --> Idle : EvConfig
}
state Configuring {
	[*] --> NewValueSelection
	NewValueSelection --> NewValuePreview : EvNewValue
	NewValuePreview --> NewValueSelection : EvNewValueRejected
	NewValuePreview --> NewValueSelection : EvNewValueSaved
	state NewValuePreview {
		State1 --> State2
	}
}
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_SubState_To_SubState()
    {
        string uml = new StateDiagramBuilder().AddSubState("A", b =>
            {
                b.AddSubState("X", b => { })
                    .AddSubState("Y", b => { });
            })
            .AddSubState("B", b =>
            {
                b.AddSubState("Z", b => { });
            })
            .AddStateTransition("X", "Z")
            .AddStateTransition("Z", "Y")
            .Build(options =>
            {
                options.HideEmptyDescriptionTag.IsActive = false;
            });

        const string expected = @"@startuml
state A {
	state X {

	}
	state Y {

	}
}
state B {
	state Z {

	}
}
X --> Z
Z --> Y
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_LongName()
    {
        string uml = new StateDiagramBuilder()
            .AddStartTransition("State1")
            .AddStateTransition("State1", "State2", "Succeeded")
            .AddFinalTransition("State1", "Aborted")
            .AddStateTransition("State2", "State3", "Succeeded")
            .AddFinalTransition("State2", "Aborted")
            .AddSubState("State3", b =>
            {
                b.AddState(new StateOptions("Accumulate Enough Data\\nLong State Name")
                {
                    Alias = "long1",
                    Description = "Just a test",
                })
                    .AddStartTransition("long1")
                    .AddStateTransition("long1", "long1", "New Data")
                    .AddStateTransition("long1", "ProcessData", "Enough Data");
            })
            .AddStateTransition("State3", "State3", "Failed")
            .AddFinalTransition("State3", "Success / Save Result")
            .AddFinalTransition("State3", "Aborted")
            .Build(options =>
            {
                options.AddOptions("scale 600 width");
            });

        const string expected = @"@startuml
hide empty description
scale 600 width
[*] --> State1
State1 --> State2 : Succeeded
State1 --> [*] : Aborted
State2 --> State3 : Succeeded
State2 --> [*] : Aborted
state State3 {
	state ""Accumulate Enough Data\nLong State Name"" as long1 : Just a test
	[*] --> long1
	long1 --> long1 : New Data
	long1 --> ProcessData : Enough Data
}
State3 --> State3 : Failed
State3 --> [*] : Success / Save Result
State3 --> [*] : Aborted
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Fork()
    {
        string uml = new StateDiagramBuilder()
            .AddState(new StateOptions("fork_state")
            {
                Alias = "f1",
                Type = StateType.Fork
            })
            .AddStartTransition("f1")
            .AddStateTransition("f1", "State2")
            .AddStateTransition("f1", "State3")
            .AddState(new StateOptions("join_state")
            {
                Alias = "f2",
                Type = StateType.Join
            })
            .AddStateTransition("State2", "f2", "Succeeded")
            .AddStateTransition("State3", "f2", "Aborted")
            .AddStateTransition("f2", "State4")
            .AddFinalTransition("State4")
            .Build();

        const string expected = @"@startuml
hide empty description
state ""fork_state"" as f1 <<fork>>
[*] --> f1
f1 --> State2
f1 --> State3
state ""join_state"" as f2 <<join>>
State2 --> f2 : Succeeded
State3 --> f2 : Aborted
f2 --> State4
State4 --> [*]
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Concurrent_Horizontal()
    {
        string uml = new StateDiagramBuilder()
            .AddStartTransition("Active")
            .AddSubState("Active", b =>
            {
                b.AddStartTransition("NumLockOff")
                    .AddStateTransition("NumLockOff", "NumLockOn", "EvNumLockPressed")
                    .AddStateTransition("NumLockOn", "NumLockOff", "EvNumLockPressed")
                    .AddConcurrentSeparator(ConcurrentSeparator.Horizontal)
                    .AddStateTransition("CapsLockOff", "CapsLockOn", "EvCapsLockPressed")
                    .AddStateTransition("CapsLockOn", "CapsLockOff", "EvCapsLockPressed")
                    .AddConcurrentSeparator(ConcurrentSeparator.Horizontal)
                    .AddStateTransition("ScrollLockOff", "ScrollLockOn", "EvCapsLockPressed")
                    .AddStateTransition("ScrollLockOn", "ScrollLockOff", "EvCapsLockPressed");
            })
            .Build();

        const string expected = @"@startuml
hide empty description
[*] --> Active
state Active {
	[*] --> NumLockOff
	NumLockOff --> NumLockOn : EvNumLockPressed
	NumLockOn --> NumLockOff : EvNumLockPressed
	--
	CapsLockOff --> CapsLockOn : EvCapsLockPressed
	CapsLockOn --> CapsLockOff : EvCapsLockPressed
	--
	ScrollLockOff --> ScrollLockOn : EvCapsLockPressed
	ScrollLockOn --> ScrollLockOff : EvCapsLockPressed
}
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Concurrent_Vertical()
    {
        string uml = new StateDiagramBuilder()
            .AddStartTransition("Active")
            .AddSubState("Active", b =>
            {
                b.AddStartTransition("NumLockOff")
                    .AddStateTransition("NumLockOff", "NumLockOn", "EvNumLockPressed")
                    .AddStateTransition("NumLockOn", "NumLockOff", "EvNumLockPressed")
                    .AddConcurrentSeparator(ConcurrentSeparator.Vertical)
                    .AddStateTransition("CapsLockOff", "CapsLockOn", "EvCapsLockPressed")
                    .AddStateTransition("CapsLockOn", "CapsLockOff", "EvCapsLockPressed")
                    .AddConcurrentSeparator(ConcurrentSeparator.Vertical)
                    .AddStateTransition("ScrollLockOff", "ScrollLockOn", "EvCapsLockPressed")
                    .AddStateTransition("ScrollLockOn", "ScrollLockOff", "EvCapsLockPressed");
            })
            .Build();

        const string expected = @"@startuml
hide empty description
[*] --> Active
state Active {
	[*] --> NumLockOff
	NumLockOff --> NumLockOn : EvNumLockPressed
	NumLockOn --> NumLockOff : EvNumLockPressed
	||
	CapsLockOff --> CapsLockOn : EvCapsLockPressed
	CapsLockOn --> CapsLockOff : EvCapsLockPressed
	||
	ScrollLockOff --> ScrollLockOn : EvCapsLockPressed
	ScrollLockOn --> ScrollLockOff : EvCapsLockPressed
}
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Stereotypes_full()
    {
        string uml = new StateDiagramBuilder()
            .AddState(new StateOptions("start1")
            {
                Type = StateType.Start
            })
            .AddState(new StateOptions("choice1")
            {
                Type = StateType.Choice
            })
            .AddState(new StateOptions("fork1")
            {
                Type = StateType.Fork
            })
            .AddState(new StateOptions("join2")
            {
                Type = StateType.Join
            })
            .AddState(new StateOptions("end3")
            {
                Type = StateType.End
            })
            .AddStartTransition("choice1", "from start \\nto choice")
            .AddStateTransition("start1", "choice1", "from start stereo\\nto choice")
            .AddStateTransition("choice1", "fork1", "from choice\\nto fork")
            .AddStateTransition("choice1", "join2", "from choice\\nto join")
            .AddStateTransition("choice1", "end3", "from choice\\nto end stereo")
            .AddStateTransition("fork1", "State1", "from fork\\nto state")
            .AddStateTransition("fork1", "State2", "from fork\\nto state")
            .AddStateTransition("State2", "join2", "from state\\nto join")
            .AddFinalTransition("State1", "from state\\nto end")
            .AddFinalTransition("join2", "from join\\nto end")
            .Build();

        const string expected = @"@startuml
hide empty description
state start1 <<start>>
state choice1 <<choice>>
state fork1 <<fork>>
state join2 <<join>>
state end3 <<end>>
[*] --> choice1 : from start \nto choice
start1 --> choice1 : from start stereo\nto choice
choice1 --> fork1 : from choice\nto fork
choice1 --> join2 : from choice\nto join
choice1 --> end3 : from choice\nto end stereo
fork1 --> State1 : from fork\nto state
fork1 --> State2 : from fork\nto state
State2 --> join2 : from state\nto join
State1 --> [*] : from state\nto end
join2 --> [*] : from join\nto end
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Point()
    {
        string uml = new StateDiagramBuilder()
            .AddSubState("Somp", b =>
            {
                b
                    .AddState(new StateOptions("entry1")
                    {
                        Type = StateType.EntryPoint
                    })
                    .AddState(new StateOptions("entry2")
                    {
                        Type = StateType.EntryPoint
                    })
                    .AddState(new StateOptions("sin"))
                    .AddStateTransition("entry1", "sin")
                    .AddStateTransition("entry2", "sin")
                    .AddStateTransition("sin", "sin2")
                    .AddStateTransition(new StateOptions("sin2"), new StateOptions("exitA")
                    {
                        Type = StateType.ExitPoint
                    });
            })
            .AddStartTransition("entry1")
            .AddStateTransition("exitA", "Foo")
            .AddStateTransition("Foo1", "entry2")
            .Build();

        const string expected = @"@startuml
hide empty description
state Somp {
	state entry1 <<entryPoint>>
	state entry2 <<entryPoint>>
	state sin
	entry1 --> sin
	entry2 --> sin
	sin --> sin2
	sin2 --> exitA <<exitPoint>>
}
[*] --> entry1
exitA --> Foo
Foo1 --> entry2
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Pin()
    {
        string uml = new StateDiagramBuilder()
            .AddSubState("Somp", b =>
            {
                b
                    .AddState(new StateOptions("entry1")
                    {
                        Type = StateType.InputPin
                    })
                    .AddState(new StateOptions("entry2")
                    {
                        Type = StateType.InputPin
                    })
                    .AddState(new StateOptions("sin"))
                    .AddStateTransition("entry1", "sin")
                    .AddStateTransition("entry2", "sin")
                    .AddStateTransition("sin", "sin2")
                    .AddStateTransition(new StateOptions("sin2"), new StateOptions("exitA")
                    {
                        Type = StateType.OutputPin
                    });
            })
            .AddStartTransition("entry1")
            .AddStateTransition("exitA", "Foo")
            .AddStateTransition("Foo1", "entry2")
            .Build();

        const string expected = @"@startuml
hide empty description
state Somp {
	state entry1 <<inputPin>>
	state entry2 <<inputPin>>
	state sin
	entry1 --> sin
	entry2 --> sin
	sin --> sin2
	sin2 --> exitA <<outputPin>>
}
[*] --> entry1
exitA --> Foo
Foo1 --> entry2
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Expansion()
    {
        string uml = new StateDiagramBuilder()
            .AddSubState("Somp", b =>
            {
                b
                    .AddState(new StateOptions("entry1")
                    {
                        Type = StateType.ExpansionInput
                    })
                    .AddState(new StateOptions("entry2")
                    {
                        Type = StateType.ExpansionInput
                    })
                    .AddState(new StateOptions("sin"))
                    .AddStateTransition("entry1", "sin")
                    .AddStateTransition("entry2", "sin")
                    .AddStateTransition("sin", "sin2")
                    .AddStateTransition(new StateOptions("sin2"), new StateOptions("exitA")
                    {
                        Type = StateType.ExpansionOutput
                    });
            })
            .AddStartTransition("entry1")
            .AddStateTransition("exitA", "Foo")
            .AddStateTransition("Foo1", "entry2")
            .Build();

        const string expected = @"@startuml
hide empty description
state Somp {
	state entry1 <<expansionInput>>
	state entry2 <<expansionInput>>
	state sin
	entry1 --> sin
	entry2 --> sin
	sin --> sin2
	sin2 --> exitA <<expansionOutput>>
}
[*] --> entry1
exitA --> Foo
Foo1 --> entry2
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_Alias()
    {
        string uml = new StateDiagramBuilder()
            .AddState(new StateOptions("alias1"))
            .AddState(new StateOptions("alias2"))
            .AddState(new StateOptions("long name")
            {
                Alias = "alias3"
            })
            .AddState(new StateOptions("alias4")
            {
                Alias = "long name"
            })
            .AddState(new StateOptions("alias1")
            {
                Description = "state alias1"
            })
            .AddState(new StateOptions("alias2")
            {
                Description = "state alias2"
            })
            .AddState(new StateOptions("alias3")
            {
                Description = "state \"long name\" as alias3"
            })
            .AddState(new StateOptions("alias4")
            {
                Description = "state alias4 as \"long name\""
            })
            .AddStateTransition("alias1", "alias2")
            .AddStateTransition("alias2", "alias3")
            .AddStateTransition("alias3", "alias4")
            .Build(options =>
            {
                options.HideEmptyDescriptionTag.IsActive = false;
            });

        const string expected = @"@startuml
state alias1
state alias2
state ""long name"" as alias3
state alias4 as ""long name""
state alias1 : state alias1
state alias2 : state alias2
state alias3 : state ""long name"" as alias3
state alias4 : state alias4 as ""long name""
alias1 --> alias2
alias2 --> alias3
alias3 --> alias4
@enduml";

        uml.Should().Be(expected);
    }
}
