using FluentAssertions;
using PlantUml.DiagramGenerators.Uml.State;

namespace PlantUml.DiagramGenerators.Uml.Tests.State;
public class StateDiagramBuilderStyleTests
{
    [Fact]
    public void Build_Arrow_Direction()
    {
        string uml = new StateDiagramBuilder()
            .AddStartTransition("First", arrowConfig: config =>
            {
                config.Direction = ArrowDirection.Up;
            })
            .AddStateTransition("First", "Second", arrowConfig: config =>
            {
                config.Direction = ArrowDirection.Right;
            })
            .AddStateTransition("Second", "Third")
            .AddStateTransition("Third", "Last", arrowConfig: config =>
            {
                config.Direction = ArrowDirection.Left;
            })
            .Build();

        const string expected = @"@startuml
hide empty description
[*] -up-> First
First -right-> Second
Second --> Third
Third -left-> Last
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_LineStyle()
    {
        string uml = new StateDiagramBuilder()
            .AddState(new StateOptions("S1"))
            .AddState(new StateOptions("S2"))
            .AddStateTransition("S1", "S2", arrowConfig: config =>
            {
                config.Color = "#DD00AA";
            })
            .AddStateTransition("S1", "S3", arrowConfig: config =>
            {
                config.Color = "#yellow";
            })
            .AddStateTransition("S1", "S4", arrowConfig: config =>
            {
                config.Color = "#red";
                config.Style = "dashed";
            })
            .AddStateTransition("S1", "S5", arrowConfig: config =>
            {
                config.Color = "#blue";
                config.Style = "dotted";
            })
            .AddStateTransition("X1", "X2", arrowConfig: config =>
            {
                config.Style = "dashed";
            })
            .AddStateTransition("Z1", "Z2", arrowConfig: config =>
            {
                config.Style = "dotted";
            })
            .AddStateTransition("Y1", "Y2", arrowConfig: config =>
            {
                config.Color = "#blue";
                config.Style = "bold";
            })
            .Build();

        const string expected = @"@startuml
hide empty description
state S1
state S2
S1 -[#DD00AA]-> S2
S1 -[#yellow]-> S3
S1 -[#red,dashed]-> S4
S1 -[#blue,dotted]-> S5
X1 -[dashed]-> X2
Z1 -[dotted]-> Z2
Y1 -[#blue,bold]-> Y2
@enduml";

        uml.Should().Be(expected);
    }

    [Fact]
    public void Build_InlineColor()
    {
        string uml = new StateDiagramBuilder()
            .AddSubState(new StateOptions("CurrentSite")
            {
                Color = "#pink"
            }, b =>
            {
                b.AddSubState(new StateOptions("HardwareSetup")
                {
                    Color = "#lightblue"
                }, sb =>
                {
                    sb.AddState(new StateOptions("Site")
                    {
                        Color = "#brown"
                    })
                        .AddStateTransition("Site", "Controller", arrowConfig: config =>
                        {
                            config.Style = "hidden";
                        })
                        .AddStateTransition("Controller", "Devices", arrowConfig: config =>
                        {
                            config.Style = "hidden";
                        });
                })
                    .AddSubState(new StateOptions("PresentationSetup"), sb =>
                    {
                        sb.AddStateTransition("Groups", "PlansAndGraphics", arrowConfig: config =>
                        {
                            config.Style = "hidden";
                        });
                    })
                    .AddState(new StateOptions("Trends")
                    {
                        Color = "#FFFF77"
                    })
                    .AddState(new StateOptions("Schedule")
                    {
                        Color = "#magenta"
                    })
                    .AddState(new StateOptions("AlarmSupression"));
            })
            .Build();

        const string expected = @"@startuml
hide empty description
state CurrentSite #pink {
	state HardwareSetup #lightblue {
		state Site #brown
		Site -[hidden]-> Controller
		Controller -[hidden]-> Devices
	}
	state PresentationSetup {
		Groups -[hidden]-> PlansAndGraphics
	}
	state Trends #FFFF77
	state Schedule #magenta
	state AlarmSupression
}
@enduml";

        uml.Should().Be(expected);
    }
}
