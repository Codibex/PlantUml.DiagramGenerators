namespace PlantUml.DiagramGenerators.Core.Theming;

/// <summary>
/// Themes
/// </summary>
/// <remarks>
/// For details look at https://plantuml.com/en/theme
/// </remarks>
public static class Themes
{
    public static string None => Theme("none");
    public static string Amiga => Theme("amiga");
    public static string AwsOrange => Theme("aws-orange");
    public static string BlackKnight => Theme("black-knight");
    public static string Bluegray => Theme("bluegray");
    public static string Blueprint => Theme("blueprint");
    public static string CarbonGray => Theme("carbon-gray");
    public static string Cerulean => Theme("cerulean");
    public static string CeruleanOutline => Theme("cerulean-outline");
    public static string CrtAmber => Theme("crt-amber");
    public static string CrtGreen => Theme("crt-green");
    public static string Cyborg => Theme("cyborg");
    public static string CyborgOutline => Theme("cyborg-outline");
    public static string Hacker => Theme("hacker");
    public static string Lightgray => Theme("lightgray");
    public static string Mars => Theme("mars");
    public static string Materia => Theme("materia");
    public static string MateriaOutline => Theme("materia-outline");
    public static string Metal => Theme("metal");
    public static string Mimeograph => Theme("mimeograph");
    public static string Minty => Theme("minty");
    public static string Plain => Theme("plain");
    public static string ReddressDarkblue => Theme("reddress-darkblue");
    public static string ReddressDarkgreen => Theme("reddress-darkgreen");
    public static string ReddressDarkorange => Theme("reddress-darkorange");
    public static string ReddressDarkred => Theme("reddress-darkred");
    public static string ReddressLightblue => Theme("reddress-lightblue");
    public static string ReddressLightgreen => Theme("reddress-lightgreen");
    public static string ReddressLightorange => Theme("reddress-lightorange");
    public static string ReddressLightred => Theme("reddress-lightred");
    public static string Sandstone => Theme("sandstone");
    public static string Silver => Theme("silver");
    public static string Sketchy => Theme("sketchy");
    public static string SketchyOutline => Theme("sketchy-outline");
    public static string Spacelab => Theme("spacelab");
    public static string SpacelabWhite => Theme("spacelab-white");
    public static string Superhero => Theme("superhero");
    public static string SuperheroOutline => Theme("superhero-outline");
    public static string Toy => Theme("toy");
    public static string United => Theme("united");
    public static string Vibrant => Theme("vibrant");

    private static string Theme(string theme) => $"!theme {theme}";
}