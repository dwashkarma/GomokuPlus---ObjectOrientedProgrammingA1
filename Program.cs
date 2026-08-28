Console.WriteLine("GomokuPlus starting...");


Game game = new Game();

if (args.Length > 0)
{
    game.RunAutomated(args[0]);
}
else
{
    game.Start();
}