using System.Reflection;
using BlockGame.Cli.Settings;
using BlockGame.Log;
using BlockGame.Util;
using Spectre.Console.Cli;

namespace BlockGame.Cli;

public class MainCommand : Command<MainSettings> {
    public override int Execute(CommandContext context, MainSettings settings) {
        int width  = settings.Width == 0 ? 854 : settings.Width;
        int height = settings.Height == 0 ? 480 : settings.Height;

        var assembly = Assembly.GetExecutingAssembly();

        var a = "";

        foreach (var name in assembly.GetManifestResourceNames()) {
            a += name + ", ";

            Logger.Debug("Resources", FileUtil.GetEmbeddedResource(name.Replace("BlockGame.", "")));
        }

        Logger.Debug("Resources", a.Replace("[", "[[").Replace("]", "]]"));

        using (Game game = new Game(width, height, "BlockGame")) {
            game.Run();
        }

        return 0;
    }
}