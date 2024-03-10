using BlockGame.Cli;
using BlockGame.Log;
using Spectre.Console.Cli;

namespace BlockGame;

class Program {
    static readonly string tag = "BlockGame";

    public static int Main(string[] args) {
        Logger.Info(tag, "Game is starting...");

        var app = new CommandApp<MainCommand>();

        app.Configure(config => { });

        return app.Run(args);
    }
}