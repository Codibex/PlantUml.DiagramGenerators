using FluentAssertions;

namespace PlantUml.DiagramGenerators.Uml.Tests;

public class StatusDiagramBuilderTests
{
    [Fact]
    public void Build_SimpleStatus()
    {
        var builder = new StatusDiagramBuilder();
        string uml = builder.AddStartTransition("New", arrowOptions: new ArrowOptions
            {
                Length = 3
            })
            .AddStatusTransition("New", "InProgress")
            .AddStatusTransition("InProgress", "Completed", arrowOptions: new ArrowOptions
            {
                Length = 1
            })
            .AddFinalTransition("Completed", arrowOptions: new ArrowOptions
            {
                Length = 6
            })
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
    public void Build_With_SubStatus()
    {
        var builder = new StatusDiagramBuilder();
        string uml = builder.AddStartTransition("New", arrowOptions: new ArrowOptions
            {
                Length = 3
            })
            .AddStatusTransition("New", "InProgress")
            .AddStatusTransition("InProgress", "Completed", arrowOptions: new ArrowOptions
            {
                Length = 1
            })
            .AddSubStatus("InProgress", b =>
            {
                b.AddStartTransition("State1")
                    .AddStatusTransition("State1", "State2")
                    .AddFinalTransition("State2");
            })
            .AddFinalTransition("Completed", arrowOptions: new ArrowOptions
            {
                Length = 6
            })
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
    public void Build_With_SubStatus_In_SubStatus()
    {
        var builder = new StatusDiagramBuilder();
        string uml = builder.AddStartTransition("New", arrowOptions: new ArrowOptions
            {
                Length = 3
            })
            .AddStatusTransition("New", "InProgress")
            .AddStatusTransition("InProgress", "Completed", arrowOptions: new ArrowOptions
            {
                Length = 1
            })
            .AddSubStatus("InProgress", b =>
            {
                b.AddStartTransition("State1")
                    .AddStatusTransition("State1", "State2")
                    .AddSubStatus("State2", b =>
                    {
                        b.AddStartTransition("Status6")
                            .AddStatusTransition("Status6", "Status7")
                            .AddFinalTransition("Status7");
                    })
                    .AddFinalTransition("State2");
            })
            .AddFinalTransition("Completed", arrowOptions: new ArrowOptions
            {
                Length = 6
            })
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
		[*] --> Status6
		Status6 --> Status7
		Status7 --> [*]
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
        string uml = new StatusDiagramBuilder().AddStartTransition("NotShooting")
            .AddSubStatus("NotShooting", b =>
            {
                b.AddStartTransition("Idle")
                    .AddStatusTransition("Idle", "Configuring", "EvConfig")
                    .AddStatusTransition("Configuring", "Idle", "EvConfig");
            })
            .AddSubStatus("Configuring", b =>
            {
                b.AddStartTransition("NewValueSelection")
                    .AddStatusTransition("NewValueSelection", "NewValuePreview", "EvNewValue")
                    .AddStatusTransition("NewValuePreview", "NewValueSelection", "EvNewValueRejected")
                    .AddStatusTransition("NewValuePreview", "NewValueSelection", "EvNewValueSaved")
                    .AddSubStatus("NewValuePreview", sb =>
                    {
                        sb.AddStatusTransition("State1", "State2");
                    });
            })
            .Build(options =>
            {
                options.HideEmptyDescriptionTag = false;
                options.AdditionalOptions = new[]
                {
                    "scale 350 width"
                };
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
        string uml = new StatusDiagramBuilder().AddSubStatus("A", b =>
            {
                b.AddSubStatus("X", b => { })
                    .AddSubStatus("Y", b => { });
            })
            .AddSubStatus("B", b =>
            {
                b.AddSubStatus("Z", b => { });
            })
            .AddStatusTransition("X", "Z")
            .AddStatusTransition("Z", "Y")
            .Build(options =>
            {
                options.HideEmptyDescriptionTag = false;
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
        string uml = new StatusDiagramBuilder()
            .AddStartTransition("State1")
            .AddStatusTransition("State1", "State2", "Succeeded")
            .AddFinalTransition("State1", "Aborted")
            .AddStatusTransition("State2", "State3", "Succeeded")
            .AddFinalTransition("State2", "Aborted")
            .AddSubStatus("State3", b =>
            {
                b.AddStatus(new StatusOptions("Accumulate Enough Data\\nLong State Name")
                    {
                        Alias = "long1",
                        Description = "Just a test",
                    })
                    .AddStartTransition("long1")
                    .AddStatusTransition("long1", "long1", "New Data")
                    .AddStatusTransition("long1", "ProcessData", "Enough Data");
            })
            .AddStatusTransition("State3", "State3", "Failed")
            .AddFinalTransition("State3", "Success / Save Result")
            .AddFinalTransition("State3", "Aborted")
            .Build(options =>
            {
                options.AdditionalOptions = new[]
                {
                    "scale 600 width"
                };
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
        string uml = new StatusDiagramBuilder()
            .AddStatus(new StatusOptions("fork_state")
            {
                Alias = "f1",
                Type = StatusType.Fork
            })
            .AddStartTransition("f1")
            .AddStatusTransition("f1", "State2")
            .AddStatusTransition("f1", "State3")
            .AddStatus(new StatusOptions("join_state")
            {
                Alias = "f2",
                Type = StatusType.Join
            })
            .AddStatusTransition("State2", "f2", "Succeeded")
            .AddStatusTransition("State3", "f2", "Aborted")
            .AddStatusTransition("f2", "State4")
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
        string uml = new StatusDiagramBuilder()
            .AddStartTransition("Active")
            .AddSubStatus("Active", b =>
            {
                b.AddStartTransition("NumLockOff")
                    .AddStatusTransition("NumLockOff", "NumLockOn", "EvNumLockPressed")
                    .AddStatusTransition("NumLockOn", "NumLockOff", "EvNumLockPressed")
                    .AddConcurrentSeparator(ConcurrentSeparator.Horizontal)
                    .AddStatusTransition("CapsLockOff", "CapsLockOn", "EvCapsLockPressed")
                    .AddStatusTransition("CapsLockOn", "CapsLockOff", "EvCapsLockPressed")
                    .AddConcurrentSeparator(ConcurrentSeparator.Horizontal)
                    .AddStatusTransition("ScrollLockOff", "ScrollLockOn", "EvCapsLockPressed")
                    .AddStatusTransition("ScrollLockOn", "ScrollLockOff", "EvCapsLockPressed");
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
        string uml = new StatusDiagramBuilder()
            .AddStartTransition("Active")
            .AddSubStatus("Active", b =>
            {
                b.AddStartTransition("NumLockOff")
                    .AddStatusTransition("NumLockOff", "NumLockOn", "EvNumLockPressed")
                    .AddStatusTransition("NumLockOn", "NumLockOff", "EvNumLockPressed")
                    .AddConcurrentSeparator(ConcurrentSeparator.Vertical)
                    .AddStatusTransition("CapsLockOff", "CapsLockOn", "EvCapsLockPressed")
                    .AddStatusTransition("CapsLockOn", "CapsLockOff", "EvCapsLockPressed")
                    .AddConcurrentSeparator(ConcurrentSeparator.Vertical)
                    .AddStatusTransition("ScrollLockOff", "ScrollLockOn", "EvCapsLockPressed")
                    .AddStatusTransition("ScrollLockOn", "ScrollLockOff", "EvCapsLockPressed");
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
        string uml = new StatusDiagramBuilder()
            .AddStatus(new StatusOptions("start1")
            {
                Type = StatusType.Start
            })
            .AddStatus(new StatusOptions("choice1")
            {
                Type = StatusType.Choice
            })
            .AddStatus(new StatusOptions("fork1")
            {
                Type = StatusType.Fork
            })
            .AddStatus(new StatusOptions("join2")
            {
                Type = StatusType.Join
            })
            .AddStatus(new StatusOptions("end3")
            {
                Type = StatusType.End
            })
            .AddStartTransition("choice1", "from start \\nto choice")
            .AddStatusTransition("start1", "choice1", "from start stereo\\nto choice")
            .AddStatusTransition("choice1", "fork1", "from choice\\nto fork")
            .AddStatusTransition("choice1", "join2", "from choice\\nto join")
            .AddStatusTransition("choice1", "end3", "from choice\\nto end stereo")
            .AddStatusTransition("fork1", "State1", "from fork\\nto state")
            .AddStatusTransition("fork1", "State2", "from fork\\nto state")
            .AddStatusTransition("State2", "join2", "from state\\nto join")
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
        string uml = new StatusDiagramBuilder()
            .AddSubStatus("Somp", b =>
            {
                b
                    .AddStatus(new StatusOptions("entry1")
                    {
                        Type = StatusType.EntryPoint
                    })
                    .AddStatus(new StatusOptions("entry2")
                    {
                        Type = StatusType.EntryPoint
                    })
                    .AddStatus(new StatusOptions("sin"))
                    .AddStatusTransition("entry1", "sin")
                    .AddStatusTransition("entry2", "sin")
                    .AddStatusTransition("sin", "sin2")
                    .AddStatusTransition(new StatusOptions("sin2"), new StatusOptions("exitA")
                    {
                        Type = StatusType.ExitPoint
                    });
            })
            .AddStartTransition("entry1")
            .AddStatusTransition("exitA", "Foo")
            .AddStatusTransition("Foo1", "entry2")
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
        string uml = new StatusDiagramBuilder()
            .AddSubStatus("Somp", b =>
            {
                b
                    .AddStatus(new StatusOptions("entry1")
                    {
                        Type = StatusType.InputPin
                    })
                    .AddStatus(new StatusOptions("entry2")
                    {
                        Type = StatusType.InputPin
                    })
                    .AddStatus(new StatusOptions("sin"))
                    .AddStatusTransition("entry1", "sin")
                    .AddStatusTransition("entry2", "sin")
                    .AddStatusTransition("sin", "sin2")
                    .AddStatusTransition(new StatusOptions("sin2"), new StatusOptions("exitA")
                    {
                        Type = StatusType.OutputPin
                    });
            })
            .AddStartTransition("entry1")
            .AddStatusTransition("exitA", "Foo")
            .AddStatusTransition("Foo1", "entry2")
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
        string uml = new StatusDiagramBuilder()
            .AddSubStatus("Somp", b =>
            {
                b
                    .AddStatus(new StatusOptions("entry1")
                    {
                        Type = StatusType.ExpansionInput
                    })
                    .AddStatus(new StatusOptions("entry2")
                    {
                        Type = StatusType.ExpansionInput
                    })
                    .AddStatus(new StatusOptions("sin"))
                    .AddStatusTransition("entry1", "sin")
                    .AddStatusTransition("entry2", "sin")
                    .AddStatusTransition("sin", "sin2")
                    .AddStatusTransition(new StatusOptions("sin2"), new StatusOptions("exitA")
                    {
                        Type = StatusType.ExpansionOutput
                    });
            })
            .AddStartTransition("entry1")
            .AddStatusTransition("exitA", "Foo")
            .AddStatusTransition("Foo1", "entry2")
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
        string uml = new StatusDiagramBuilder()
            .AddStatus(new StatusOptions("alias1"))
            .AddStatus(new StatusOptions("alias2"))
            .AddStatus(new StatusOptions("long name")
            {
                Alias = "alias3"
            })
            .AddStatus(new StatusOptions("alias4")
            {
                Alias = "long name"
            })
            .AddStatus(new StatusOptions("alias1")
            {
                Description = "state alias1"
            })
            .AddStatus(new StatusOptions("alias2")
            {
                Description = "state alias2"
            })
            .AddStatus(new StatusOptions("alias3")
            {
                Description = "state \"long name\" as alias3"
            })
            .AddStatus(new StatusOptions("alias4")
            {
                Description = "state alias4 as \"long name\""
            })
            .AddStatusTransition("alias1", "alias2")
            .AddStatusTransition("alias2", "alias3")
            .AddStatusTransition("alias3", "alias4")
            .Build(options =>
            {
                options.HideEmptyDescriptionTag = false;
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
