using MinecraftClone.Cli.Settings;
using Spectre.Console.Cli;

namespace MinecraftClone.Cli;

public class MainCommand : Command<MainSettings> {
    public override int Execute(CommandContext context, MainSettings settings) {
        int width  = settings.Width == 0 ? 854 : settings.Width;
        int height = settings.Height == 0 ? 480 : settings.Height;

        using (Game game = new Game(width, height, "Minecraft Clone")) {
            game.Run();
        }

        return 0;
    }
}