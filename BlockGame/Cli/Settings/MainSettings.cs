using Spectre.Console.Cli;

namespace MinecraftClone.Cli.Settings;

public class MainSettings : CommandSettings {
    [CommandOption("-h|--height <HEIGHT>")]
    public int Height { get; set; }

    [CommandOption("-w|--width <WIDTH>")]
    public int Width { get; set; }
}