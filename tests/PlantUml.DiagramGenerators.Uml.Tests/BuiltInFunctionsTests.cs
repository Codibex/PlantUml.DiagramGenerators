using FluentAssertions;
using PlantUml.DiagramGenerators.Core;

namespace PlantUml.DiagramGenerators.Uml.Tests;

public class BuiltInFunctionsTests
{
    [Fact]
    public void Function_Chr()
    {
        const int value = 5;
        BuiltInFunctions.Chr(value).Should().Be($"%chr({value})");
    }

    [Theory]
    [InlineData("#FF0808")]
    [InlineData("red")]
    public void Function_Darken(string color)
    {
        const int ratio = 20;
        BuiltInFunctions.Darken(color, ratio).Should().Be($"%darken(\"{color}\", {ratio})");
    }

    [Fact]
    public void Function_Date()
    {
        const string format = "yyyy.MM.dd";
        BuiltInFunctions.Date(format).Should().Be($"%date({format})");
    }

    [Fact]
    public void Function_Dec2Hex()
    {
        const int value = 5;
        BuiltInFunctions.Dec2Hex(value).Should().Be($"%dec2hex({value})");
    }

    [Fact]
    public void Function_DirPath()
    {
        BuiltInFunctions.DirPath().Should().Be("%dirpath()");
    }

    [Fact]
    public void Function_Feature()
    {
        const string feature = "feature";
        BuiltInFunctions.Feature(feature).Should().Be($"%feature(\"{feature}\")");
    }

    [Fact]
    public void Function_False()
    {
        BuiltInFunctions.False().Should().Be("%false()");
    }

    [Fact]
    public void Function_FileExists()
    {
        const string file = "C:\\temp\\foo.txt";
        BuiltInFunctions.FileExists(file).Should().Be($"%file_exists(\"{file}\")");
    }

    [Fact]
    public void Function_Filename()
    {
        BuiltInFunctions.Filename().Should().Be("%filename()");
    }

    [Fact]
    public void Function_FunctionExists()
    {
        const string function = "fun";
        BuiltInFunctions.FunctionExists(function).Should().Be($"%function_exists(\"{function}\")");
    }

    [Fact]
    public void Function_GetVariableValue()
    {
        const string variable = "$my_variable";
        BuiltInFunctions.GetVariableValue(variable).Should().Be($"%get_variable_value(\"{variable}\")");
    }

    [Fact]
    public void Function_GetEnv()
    {
        const string variable = "$my_env_variable";
        BuiltInFunctions.GetEnv(variable).Should().Be($"%getenv(\"{variable}\")");
    }

    [Fact]
    public void Function_Hex2Dec()
    {
        const string value = "A";
        BuiltInFunctions.Hex2Dec(value).Should().Be($"%hex2dec(\"{value}\")");
    }

    [Fact]
    public void Function_HslColor()
    {
        const byte red = 12;
        const byte green = 255;
        const byte blue = 102;
        BuiltInFunctions.HslColor(red, green, blue).Should().Be($"%hsl_color({red}, {green}, {blue})");
    }

    [Fact]
    public void Function_Intval()
    {
        const string value = "42";
        BuiltInFunctions.Intval(value).Should().Be($"%intval(\"{value}\")");
    }

    [Fact]
    public void Function_IsDark()
    {
        const string hexColor = "#010101";
        BuiltInFunctions.IsDark(hexColor).Should().Be($"%is_dark(\"{hexColor}\")");
    }

    [Fact]
    public void Function_IsLight()
    {
        const string hexColor = "#010101";
        BuiltInFunctions.IsLight(hexColor).Should().Be($"%is_light(\"{hexColor}\")");
    }

    [Theory]
    [InlineData("#FF0808")]
    [InlineData("red")]
    public void Function_Lighten(string color)
    {
        const int ratio = 20;
        BuiltInFunctions.Lighten(color, ratio).Should().Be($"%lighten(\"{color}\", {ratio})");
    }

    [Fact]
    public void Function_LoadJson()
    {
        const string file = "C:\\temp\\foo.txt";
        BuiltInFunctions.LoadJson(file).Should().Be($"%load_json(\"{file}\")");
    }

    [Fact]
    public void Function_Lower()
    {
        const string value = "Hello";
        BuiltInFunctions.Lower(value).Should().Be($"%lower(\"{value}\")");
    }

    [Fact]
    public void Function_Newline()
    {
        BuiltInFunctions.Newline().Should().Be("%newline()");
    }

    [Fact]
    public void Function_Not()
    {
        const string expression = "2+2==4";
        BuiltInFunctions.Not(expression).Should().Be($"%not(\"{expression}\")");
    }

    [Fact]
    public void Function_ReverseColor()
    {
        const string hexColor = "#010101";
        BuiltInFunctions.ReverseColor(hexColor).Should().Be($"%reverse_color(\"{hexColor}\")");
    }

    [Fact]
    public void Function_ReverseHsluvColor()
    {
        const string hexColor = "#010101";
        BuiltInFunctions.ReverseHsluvColor(hexColor).Should().Be($"%reverse_hsluv_color(\"{hexColor}\")");
    }

    [Fact]
    public void Function_SetVariableValue()
    {
        const string variable = "$my_variable";
        const string value = "my_value";
        BuiltInFunctions.SetVariableValue(variable, value).Should().Be($"%set_variable_value(\"{variable}\", \"{value}\")");
    }

    [Fact]
    public void Function_Size()
    {
        const string value = "foo";
        BuiltInFunctions.Size(value).Should().Be($"%size(\"{value}\")");
    }

    [Fact]
    public void Function_String()
    {
        const string expression = "1 + 2";
        BuiltInFunctions.String(expression).Should().Be($"%string({expression})");
    }

    [Fact]
    public void Function_Strlen()
    {
        const string value = "foo";
        BuiltInFunctions.Strlen(value).Should().Be($"%strlen(\"{value}\")");
        //Strlen(string value) => $"%strlen(\"{value}\")";
    }

    [Fact]
    public void Function_Strpos()
    {
        const string fullString = "abcdef";
        const string subString = "cd";
        BuiltInFunctions.Strpos(fullString, subString).Should().Be($"%strpos(\"{fullString}\", \"{subString}\")");
    }

    [Fact]
    public void Function_Substr()
    {
        const string fullString = "abcdef";
        const int start = 3;
        const int length = 2;
        BuiltInFunctions.Substr(fullString, start, length).Should().Be($"%substr(\"{fullString}\", {start}, {length})");
    }

    [Fact]
    public void Function_True()
    {
        BuiltInFunctions.True().Should().Be("%true()");
    }

    [Fact]
    public void Function_Upper()
    {
        const string value = "hello";
        BuiltInFunctions.Upper(value).Should().Be($"%upper(\"{value}\")");
    }

    [Fact]
    public void Function_VariableExists()
    {
        const string variable = "$my_variable";
        BuiltInFunctions.VariableExists(variable).Should().Be($"%variable_exists(\"{variable}\")");
    }

    [Fact]
    public void Function_Version()
    {
        BuiltInFunctions.Version().Should().Be("%version()");
    }
}