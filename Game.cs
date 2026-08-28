
enum GameMode
{
    HumanVsHuman,
    HumanVsComputer
}


public class Game
{

    private Board board;
    private Player player1;
    private Player player2;
    private Player currentPlayer;

    private GameMode gameMode;

    private Random randomStone = new Random();

    public Game()
    {
        this.board = new Board();
        this.player1 = new Player(PlayerTypes.Player1);
        this.player2 = new Player(PlayerTypes.Player2);
        this.currentPlayer = this.player1;

    }

    private void SwitchPlayer()
    {
        if (currentPlayer.PlayerType == PlayerTypes.Player1)
        {
            currentPlayer = player2;
        }
        else
        {
            currentPlayer = player1;
        }
    }

    private void SelectGameMode()
    {
        while (true)
        {
            Console.WriteLine("Select Game Mode:");
            Console.WriteLine("1=Human vs Human");
            Console.WriteLine("2=Human vs Computer");
            Console.Write("Enter your choice of game mode (1 or 2):");


            string? choice = Console.ReadLine();
            if (choice == "1")
            {
                gameMode = GameMode.HumanVsHuman;
                break;
            }
            else if (choice == "2")
            {
                gameMode = GameMode.HumanVsComputer;
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select 1 = Human vs Human or 2 = Human vs Computer.");


            }
        }
    }


    private bool ComputerMove()
    {
        while (true)
        {
            if (board.IsBoardFull())
            {
                Console.WriteLine("Board cells are full and computer cannot place any stones.");
                SwitchPlayer();
                return false;
            }
            int row = randomStone.Next(1, 11);
            int col = randomStone.Next(1, 11);
            if (board.IsEmptyCell(row, col))
            {
                Console.WriteLine("----------Computer Placing the stone.----------");
                Stone stone = new Stone(StoneTypes.Ordinary, PlayerTypes.Player2);
                bool success = board.PlaceStone(row, col, stone);
                if (success)
                {

                    Console.WriteLine($"Stone Placed by Computer at {row}: {col}");

                    if (board.CheckVertical(PlayerTypes.Player2) || board.CheckHorizontal(PlayerTypes.Player2) || board.CheckDiagonal(PlayerTypes.Player2))
                    {
                        board.DisplayBoard();
                        Console.WriteLine($"{PlayerTypes.Player2} wins!");
                        return true;

                    }
                    else
                    {
                        SwitchPlayer();
                        return false;

                    }
                }

            }


        }

    }



    public void Start()
    {
        SelectGameMode();
        Console.WriteLine($"Game mode selected: {gameMode}");

        while (true)
        {
            board.DisplayBoard();


            if (gameMode == GameMode.HumanVsComputer && currentPlayer.PlayerType == PlayerTypes.Player2)
            {
                bool computerWins = ComputerMove();
                if (computerWins)
                {
                    break;
                }

                continue;

            }
            Console.WriteLine($"Current player: {currentPlayer.PlayerType}");
            Console.WriteLine("Enter row and column to place stone (e.g: O3:4) {O = ordinary Stone , H = Heavy Stone , E = Erase Stone or Help = open help information}:");
            string? input = Console.ReadLine();


            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid command.");
                continue;
            }

            if (input == ("S") || input == "s")
            {
                SaveGame();
                continue;
            }
            if (input == ("L") || input == "l")
            {
                LoadGame();
                continue;
            }


            if (input == "help")
            {
                ShowHelp();
                continue;
            }

            if (input == "Q")

            {
                Console.WriteLine("Game has been ended.");
                break;
            }

            char command = input[0];
            string coordinates = input.Substring(1);
            string[] parts = coordinates.Split(":");


            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid command format.");
                continue;
            }


            if (!int.TryParse(parts[0], out int row) ||
             !int.TryParse(parts[1], out int col))
            {
                Console.WriteLine("Invalid coordinates. Row and column must be numbers.");
                continue;
            }


            if (command == 'O' || command == 'o')
            {
                Stone stone = new Stone(StoneTypes.Ordinary, currentPlayer.PlayerType);
                bool success = board.PlaceStone(row, col, stone);
                if (success)
                {
                    if (board.CheckHorizontal(currentPlayer.PlayerType) || board.CheckVertical(currentPlayer.PlayerType) || board.CheckDiagonal(currentPlayer.PlayerType))
                    {
                        board.DisplayBoard();
                        Console.WriteLine($"{currentPlayer.PlayerType} wins!");
                        break;
                    }
                    SwitchPlayer();
                }
            }
            else if (command == 'H' || command == 'h')
            {
                if (currentPlayer.HeavyStonesCount <= 0)
                {
                    Console.WriteLine($"No heavy stones left for {currentPlayer.PlayerType}");
                    continue;
                }
                else
                {

                    Stone stone = new Stone(StoneTypes.Heavy, currentPlayer.PlayerType);
                    bool success = board.PlaceStone(row, col, stone);
                    if (success)
                    {
                        currentPlayer.UseHeavyStone();
                        if (board.CheckHorizontal(currentPlayer.PlayerType) || board.CheckVertical(currentPlayer.PlayerType) || board.CheckDiagonal(currentPlayer.PlayerType))
                        {
                            board.DisplayBoard();
                            Console.WriteLine($"{currentPlayer.PlayerType} wins!");
                            break;
                        }

                        SwitchPlayer();
                    }
                }
            }
            else if (command == 'E' || command == 'e')
            {
                if (currentPlayer.EraserCount <= 0)
                {
                    Console.WriteLine($"No erasers left for {currentPlayer.PlayerType}");
                    continue;
                }
                bool success = board.EraseStone(row, col, currentPlayer.PlayerType);
                if (success)
                {

                    currentPlayer.UseEraser();
                    SwitchPlayer();
                }
            }
            else
            {
                Console.WriteLine("Invalid command. Use 'O' = ordinary stone, 'H' = heavy stone, or 'E' = erase stone or Help = open help information.");
            }
        }
    }


    public void SaveGame()
    {
        Directory.CreateDirectory("SavedGames");
        using (StreamWriter writer = new StreamWriter("SavedGames/savegame.txt"))
        {
            writer.WriteLine($"Game Mode: {gameMode}");
            writer.WriteLine($"Current Player: {currentPlayer.PlayerType}");

            writer.WriteLine($"Player1 Heavy Stones: {player1.HeavyStonesCount}");
            writer.WriteLine($"Player1 Eraser: {player1.EraserCount}");

            writer.WriteLine($"Player2 Heavy Stones: {player2.HeavyStonesCount}");
            writer.WriteLine($"Player2 Eraser: {player2.EraserCount}");

            writer.WriteLine("----Board data----");

            for (int row = 1; row <= 10; row++)
            {
                for (int col = 1; col <= 10; col++)
                {
                    Stone? stone = board.GetStone(row, col);
                    if (stone != null)
                    {
                        writer.WriteLine($"{row},{col},{stone.Player},{stone.Type}");
                    }
                }
            }


        }
        Console.WriteLine("Game saved successfully.");

    }


    public void LoadGame()
    {
        string filePath = "SavedGames/savegame.txt";
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Cannot find the file.");
            return;
        }
        Console.WriteLine("Saved file found successfully.");
        string[] lines = File.ReadAllLines(filePath);
        Console.WriteLine("Files loaded succesfully.");

        string[] gameModeSection = lines[0].Split(":");
        string savedGameMode = gameModeSection[1].Trim();


        if (savedGameMode == "HumanVsHuman")
        {
            gameMode = GameMode.HumanVsHuman;
        }
        else if (savedGameMode == "HumanVsComputer")
        {
            gameMode = GameMode.HumanVsComputer;
        }


        string[] savedCurrentPlayerSection = lines[1].Split(":");
        string savedCurrentPlayer = savedCurrentPlayerSection[1].Trim();
        if (savedCurrentPlayer == "Player1")
        {
            currentPlayer = player1;
        }
        else if (savedCurrentPlayer == "Player2")
        {
            currentPlayer = player2;
        }


        string[] savedP1HeavyStonesSection = lines[2].Split(":");
        string savedP1HeavyStonesCount = savedP1HeavyStonesSection[1].Trim();
        if (int.TryParse(savedP1HeavyStonesCount, out int heavyP1Count))
        {
            player1.SetHeavyStoneCount(heavyP1Count);
        }

        string[] savedP1EraserSection = lines[3].Split(":");
        string savedP1Eraser = savedP1EraserSection[1].Trim();
        if (int.TryParse(savedP1Eraser, out int eraserP1Count))
        {
            player1.SetEraserCount(eraserP1Count);
        }


        string[] savedP2HeavyStonesSection = lines[4].Split(":");

        string savedP2HeavyStonesCount = savedP2HeavyStonesSection[1].Trim();
        if (int.TryParse(savedP2HeavyStonesCount, out int heavyP2Count))
        {
            player2.SetHeavyStoneCount(heavyP2Count);
        }

        string[] savedP2EraserSection = lines[5].Split(":");
        string savedP2Eraser = savedP2EraserSection[1].Trim();
        if (int.TryParse(savedP2Eraser, out int eraserP2Count))
        {
            player2.SetEraserCount(eraserP2Count);
        }


        // Getting board data................
        board = new Board();

        for (int i = 7; i < lines.Length; i++)
        {
            string[] stoneData = lines[i].Split(",");

            if (!int.TryParse(stoneData[0], out int row) || !int.TryParse(stoneData[1], out int col))
            {
                Console.WriteLine("Invalid row and column data in saved file.");
                continue;
            }
            ;

            string player = stoneData[2];
            string type = stoneData[3];
            PlayerTypes savedPlayer;
            StoneTypes savedStoneTypes;
            if (player == "Player1")
            {
                savedPlayer = PlayerTypes.Player1;
            }
            else
            {
                savedPlayer = PlayerTypes.Player2;
            }

            if (type == "Ordinary")
            {
                savedStoneTypes = StoneTypes.Ordinary;
            }
            else if (type == "Heavy")
            {
                savedStoneTypes = StoneTypes.Heavy;
            }
            else
            {
                Console.WriteLine("Invalid Stone type.");
                continue;
            }


            Stone stone = new Stone(savedStoneTypes, savedPlayer);
            board.PlaceStone(row, col, stone);
        }



    }



    public bool GetExecuted(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid command.");
            return false;
        }

        char command = input[0];

        string coordinates = input.Substring(1);
        string[] parts = coordinates.Split(':');

        if (parts.Length != 2)
        {
            Console.WriteLine($"Invalid command: {input}");
            return false;
        }

        if (!int.TryParse(parts[0], out int row) ||
            !int.TryParse(parts[1], out int col))
        {
            Console.WriteLine($"Invalid coordinates: {input}");
            return false;
        }

        if (command == 'O' || command == 'o')
        {
            Stone stone =
                new Stone(StoneTypes.Ordinary, currentPlayer.PlayerType);

            bool success = board.PlaceStone(row, col, stone);

            if (!success)
            {
                return false;
            }

            if (board.CheckDiagonal(currentPlayer.PlayerType) || board.CheckHorizontal(currentPlayer.PlayerType) || board.CheckVertical(currentPlayer.PlayerType))
            {
                return true;
            }

            SwitchPlayer();
            return false;
        }

        // Heavy Stone
        else if (command == 'H' || command == 'h')
        {
            if (currentPlayer.HeavyStonesCount <= 0)
            {
                Console.WriteLine($"No heavy stones left for {currentPlayer.PlayerType}");
                return false;
            }

            Stone stone = new Stone(StoneTypes.Heavy, currentPlayer.PlayerType);
            bool success = board.PlaceStone(row, col, stone);

            if (!success)
            {
                return false;
            }

            currentPlayer.UseHeavyStone();

            if (board.CheckDiagonal(currentPlayer.PlayerType) || board.CheckHorizontal(currentPlayer.PlayerType) || board.CheckVertical(currentPlayer.PlayerType))
            {
                return true;
            }

            SwitchPlayer();
            return false;
        }

        // Eraser
        else if (command == 'E' || command == 'e')
        {
            if (currentPlayer.EraserCount <= 0)
            {
                Console.WriteLine($"No erasers left for {currentPlayer.PlayerType}");
                return false;
            }

            bool success = board.EraseStone(row, col, currentPlayer.PlayerType);

            if (!success)
            {
                return false;
            }

            currentPlayer.UseEraser();
            SwitchPlayer();
            return false; // erasing a stone can never complete a 5-in-a-row for the eraser's owner
        }

        else
        {
            Console.WriteLine($"Invalid command: {input}");
        }

        return false;
    }

    public void RunAutomated(string arguments)
    {
        string[] commands = arguments.Split(',');

        foreach (string command in commands)
        {
            Console.WriteLine($"Executing: {command}");

            bool won = GetExecuted(command);

            if (won)
            {
                Console.WriteLine($"{currentPlayer.PlayerType} wins!");
                break;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Final board:");
        board.DisplayBoard();
    }


    public void ShowHelp()
    {
        Console.WriteLine("===GomokuPlus===");
        Console.WriteLine("O3:4 for Ordinary Stone.");
        Console.WriteLine("H2:4 for Heavy Stone.");
        Console.WriteLine("S or s to save game.");
        Console.WriteLine("L or l to load save game.");
        Console.WriteLine("Q to quit the game.");
        Console.WriteLine("Help for help.");
    }
}