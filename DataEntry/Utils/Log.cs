#nullable disable
public static class Log {
    enum WarningLevel { Debug, Warning, Error }

    public static void Debug(string msg) => output(WarningLevel.Debug, msg);
    public static void Error(string msg) => output(WarningLevel.Error, msg);
    public static void Warning(string msg) => output(WarningLevel.Warning, msg);

    // Private

    static void output(WarningLevel warn, string msg) {
        var prefix = warn switch {
        WarningLevel.Debug => "",
        WarningLevel.Warning => "WARNING: ",
        WarningLevel.Error => "ERROR: ",
        _ => $"{warn}: "
        };
        System.Console.WriteLine($"{prefix}{msg}");
    }
}
