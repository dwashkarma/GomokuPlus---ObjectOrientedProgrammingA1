
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
            Console.WriteLine("Enter row and column to place stone (e.g: O3:4) {O = ordinary Stone , H = Heavy Stone and E = Erase Stone}:");
            string? input = Console.ReadLine();


            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid command.");
                continue;
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
                Console.WriteLine("Invalid command. Use 'O' = ordinary stone, 'H' = heavy stone, or 'E' = erase stone.");
            }





        }
    }

}