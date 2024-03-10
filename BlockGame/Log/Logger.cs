using System.Diagnostics;
using Spectre.Console;

namespace BlockGame.Log;

public class Logger {
    public static void Info(string tag, string message) {
        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);

        AnsiConsole.MarkupLine($"[blue][[{time:h:mm:ss}]][/] [green][[{Process.GetCurrentProcess().ProcessName}/INFO]][/] [aqua]({tag})[/] {message}");
    }

    public static void Error(string tag, string message) {
        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);

        AnsiConsole.MarkupLine($"[blue][[{time:h:mm:ss}]][/] [red][[{Process.GetCurrentProcess().ProcessName}/ERROR]][/] [aqua]({tag})[/] {message}");
    }

    public static void Warn(string tag, string message) {
        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);

        AnsiConsole.MarkupLine($"[blue][[{time:h:mm:ss}]][/] [yellow][[{Process.GetCurrentProcess().ProcessName}/WARNING]][/] [aqua]({tag})[/] {message}");
    }

    public static void Debug(string tag, string message) {
        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);

        AnsiConsole.MarkupLine($"[blue][[{time:h:mm:ss}]][/] [purple][[{Process.GetCurrentProcess().ProcessName}/DEBUG]][/] [aqua]({tag})[/] {message}");
    }

    public static void Fatal(string tag, string message) {
        TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);

        AnsiConsole.MarkupLine($"[blue][[{time:h:mm:ss}]][/] [red][[{Process.GetCurrentProcess().ProcessName}/FATAL]][/] [aqua]({tag})[/] {message}");
    }
}