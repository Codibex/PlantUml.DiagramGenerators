using FluentAssertions;
using PlantUml.DiagramGenerators.Uml.Status;

namespace PlantUml.DiagramGenerators.Uml.Tests.Status;
public class StatusDiagramBuilderStyleTests
{
    [Fact]
    public void Build_Arrow_Direction()
    {
        string uml = new StatusDiagramBuilder()
            .AddStartTransition("First", arrowOptions: new ArrowOptions
            {
                Direction = ArrowDirection.Up
            })
            .AddStatusTransition("First", "Second", arrowOptions: new ArrowOptions
            {
                Direction = ArrowDirection.Right
            })
            .AddStatusTransition("Second", "Third")
            .AddStatusTransition("Third", "Last", arrowOptions: new ArrowOptions
            {
                Direction = ArrowDirection.Left
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
            .AddStatusTransition("S1", "S2", arrowOptions: new ArrowOptions
            {
                Color = "#DD00AA"
            })
            .AddStatusTransition("S1", "S3", arrowOptions: new ArrowOptions
            {
                Color = "#yellow"
            })
            .AddStatusTransition("S1", "S4", arrowOptions: new ArrowOptions
            {
                Color = "#red",
                Style = "dashed"
            })
            .AddStatusTransition("S1", "S5", arrowOptions: new ArrowOptions
            {
                Color = "#blue",
                Style = "dotted"
            })
            .AddStatusTransition("X1", "X2", arrowOptions: new ArrowOptions
            {
                Style = "dashed"
            })
            .AddStatusTransition("Z1", "Z2", arrowOptions: new ArrowOptions
            {
                Style = "dotted"
            })
            .AddStatusTransition("Y1", "Y2", arrowOptions: new ArrowOptions
            {
                Color = "#blue",
                Style = "bold"
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
                        .AddStatusTransition("Site", "Controller", arrowOptions: new ArrowOptions
                        {
                            Style = "hidden"
                        })
                        .AddStatusTransition("Controller", "Devices", arrowOptions: new ArrowOptions
                        {
                            Style = "hidden"
                        });
                })
                    .AddSubStatus(new StatusOptions("PresentationSetup"), sb =>
                    {
                        sb.AddStatusTransition("Groups", "PlansAndGraphics", arrowOptions: new ArrowOptions
                        {
                            Style = "hidden"
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
