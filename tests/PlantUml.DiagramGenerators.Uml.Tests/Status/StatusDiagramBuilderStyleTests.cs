using FluentAssertions;
using PlantUml.DiagramGenerators.Uml.Status;

namespace PlantUml.DiagramGenerators.Uml.Tests.Status;
public class StatusDiagramBuilderStyleTests
{
    [Fact]
    public void Build_Arrow_Direction()
    {
        string uml = new StatusDiagramBuilder()
            .AddStartTransition("First", arrowConfig: config =>
            {
                config.Direction = ArrowDirection.Up;
            })
            .AddStatusTransition("First", "Second", arrowConfig: config =>
            {
                config.Direction = ArrowDirection.Right;
            })
            .AddStatusTransition("Second", "Third")
            .AddStatusTransition("Third", "Last", arrowConfig: config =>
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
        string uml = new StatusDiagramBuilder()
            .AddStatus(new StatusOptions("S1"))
            .AddStatus(new StatusOptions("S2"))
            .AddStatusTransition("S1", "S2", arrowConfig: config =>
            {
                config.Color = "#DD00AA";
            })
            .AddStatusTransition("S1", "S3", arrowConfig: config =>
            {
                config.Color = "#yellow";
            })
            .AddStatusTransition("S1", "S4", arrowConfig: config =>
            {
                config.Color = "#red";
                config.Style = "dashed";
            })
            .AddStatusTransition("S1", "S5", arrowConfig: config =>
            {
                config.Color = "#blue";
                config.Style = "dotted";
            })
            .AddStatusTransition("X1", "X2", arrowConfig: config =>
            {
                config.Style = "dashed";
            })
            .AddStatusTransition("Z1", "Z2", arrowConfig: config =>
            {
                config.Style = "dotted";
            })
            .AddStatusTransition("Y1", "Y2", arrowConfig: config =>
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
        string uml = new StatusDiagramBuilder()
            .AddSubStatus(new StatusOptions("CurrentSite")
            {
                Color = "#pink"
            }, b =>
            {
                b.AddSubStatus(new StatusOptions("HardwareSetup")
                {
                    Color = "#lightblue"
                }, sb =>
                {
                    sb.AddStatus(new StatusOptions("Site")
                    {
                        Color = "#brown"
                    })
                        .AddStatusTransition("Site", "Controller", arrowConfig: config =>
                        {
                            config.Style = "hidden";
                        })
                        .AddStatusTransition("Controller", "Devices", arrowConfig: config =>
                        {
                            config.Style = "hidden";
                        });
                })
                    .AddSubStatus(new StatusOptions("PresentationSetup"), sb =>
                    {
                        sb.AddStatusTransition("Groups", "PlansAndGraphics", arrowConfig: config =>
                        {
                            config.Style = "hidden";
                        });
                    })
                    .AddStatus(new StatusOptions("Trends")
                    {
                        Color = "#FFFF77"
                    })
                    .AddStatus(new StatusOptions("Schedule")
                    {
                        Color = "#magenta"
                    })
                    .AddStatus(new StatusOptions("AlarmSupression"));
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
