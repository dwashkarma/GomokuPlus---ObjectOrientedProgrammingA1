public class Game
{

    private Board board;
    private Player player1;
    private Player player2;
    private Player currentPlayer;

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

    public void Start()
    {

        while (true)
        {
            board.DisplayBoard();
            Console.WriteLine($"Current player: {currentPlayer.PlayerType}");
            Console.WriteLine("Enter row and column to place stone (e.g: O3:4) {O for ordinary Stone and H for Heavy Stone}:");
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
                    if (board.CheckHorizontal(currentPlayer.PlayerType) || board.CheckVertical(currentPlayer.PlayerType))
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
                        if (board.CheckHorizontal(currentPlayer.PlayerType) || board.CheckVertical(currentPlayer.PlayerType))
                        {
                            board.DisplayBoard();
                            Console.WriteLine($"{currentPlayer.PlayerType} wins!");
                            break;
                        }
                        currentPlayer.UseHeavyStone();
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
                Console.WriteLine("Invalid command. Use 'O' = ordinary stone, 'H' = heavy stone, or 'E' = eraser.");
            }





        }
    }

}