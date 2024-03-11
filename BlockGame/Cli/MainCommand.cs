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

        using (Game game = new Game(width, height, "BlockGame")) {
            game.Run();
        }

        return 0;
    }
}