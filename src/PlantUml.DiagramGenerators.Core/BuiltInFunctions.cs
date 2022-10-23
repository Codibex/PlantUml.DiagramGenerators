namespace PlantUml.DiagramGenerators.Core;

/// <summary>
/// Builtin functions
/// </summary>
/// <remarks>
/// For details look at https://plantuml.com/de/preprocessing
/// </remarks>
public static class BuiltInFunctions
{
    public static string Chr(int unicode) => $"%chr({unicode})";
    public static string Darken(string color, int ratio) => $"%darken(\"{color}\", {ratio})";
    public static string Date(string format) => $"%date({format})";
    public static string Dec2Hex(int value) => $"%dec2hex({value})";
    public static string DirPath() => "%dirpath()";
    public static string Feature(string feature) => $"%feature(\"{feature}\")";
    public static string False() => "%false()";
    public static string FileExists(string file) => $"%file_exists(\"{file}\")";
    public static string Filename() => "%filename()";
    public static string FunctionExists(string function) => $"%function_exists(\"{function}\")";
    public static string GetVariableValue(string variable) => $"%get_variable_value(\"{variable}\")";
    public static string GetEnv(string variable) => $"%getenv(\"{variable}\")";
    public static string Hex2Dec(string value) => $"%hex2dec(\"{value}\")";
    public static string HslColor(byte red, byte green, byte blue) => $"%hsl_color({red}, {green}, {blue})";
    public static string Intval(string value) => $"%intval(\"{value}\")";
    public static string IsDark(string hexColor) => $"%is_dark(\"{hexColor}\")";
    public static string IsLight(string hexColor) => $"%is_light(\"{hexColor}\")";
    public static string Lighten(string color, int ratio) => $"%lighten(\"{color}\", {ratio})";
    public static string LoadJson(string file) => $"%load_json(\"{file}\")";
    public static string Lower(string value) => $"%lower(\"{value}\")";
    public static string Newline() => $"%newline()";
    public static string Not(string expression) => $"%not(\"{expression}\")";
    public static string ReverseColor(string hexColor) => $"%reverse_color(\"{hexColor}\")";
    public static string ReverseHsluvColor(string hexColor) => $"%reverse_hsluv_color(\"{hexColor}\")";
    public static string SetVariableValue(string variable, string value) => $"%set_variable_value(\"{variable}\", \"{value}\")";
    public static string Size(string value) => $"%size(\"{value}\")";
    public static string String(string expression) => $"%string({expression})";
    public static string Strlen(string value) => $"%strlen(\"{value}\")";
    public static string Strpos(string fullString, string subString) => $"%strpos(\"{fullString}\", \"{subString}\")";
    public static string Substr(string fullString, int start, int length) => $"%substr(\"{fullString}\", {start}, {length})";
    public static string True() => "%true()";
    public static string Upper(string value) => $"%upper(\"{value}\")";
    public static string VariableExists(string variable) => $"%variable_exists(\"{variable}\")";
    public static string Version() => $"%version()";
}