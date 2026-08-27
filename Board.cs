public class Board
{

    private Stone?[,] board;
    public Board()
    {
        board = new Stone?[10, 10];

    }


    public void DisplayBoard()
    {
        Console.WriteLine("    1   2   3   4   5   6   7   8   9   10");
        Console.WriteLine("  +---+---+---+---+---+---+---+---+---+---+");
        for (int i = 1; i <= 10; i++)
        {

            Console.Write($"{i} ");
            for (int col = 1; col <= 10; col++)
            {
                if (board[i - 1, col - 1] == null)
                {
                    Console.Write("|   ");
                }
                else
                {
                    Console.Write($"| {board[i - 1, col - 1].Symbol} ");
                }

            }
            Console.WriteLine("|");
            Console.WriteLine("  +---+---+---+---+---+---+---+---+---+---+");

        }
    }

    public bool IsValidCell(int row, int col)
    {
        if (row >= 1 && row < 11 && col >= 1 && col < 11)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsEmptyCell(int row, int col)
    {
        if (board[row - 1, col - 1] == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PlaceStone(int row, int col, Stone stone)
    {
        if (IsValidCell(row, col) && IsEmptyCell(row, col))
        {
            board[row - 1, col - 1] = stone;
            return true;
        }
        else
        {
            Console.WriteLine($"Cannot place stone at {row},{col}. Cell is either not valid or already assigned.");
            return false;
        }
    }

    public bool EraseStone(int row, int col, PlayerTypes currentPlayer)
    {


        if (!IsValidCell(row, col))
        {
            Console.WriteLine($"Cannot erase stone at {row},{col}. Cell is either not valid.");
            return false;
        }
        if (IsEmptyCell(row, col))
        {
            Console.WriteLine($"Cannot erase stone at {row},{col}. Cell is already empty.");
            return false;
        }


        Stone targetStone = board[row - 1, col - 1]!;

        bool IsOpponentStone = targetStone.Player != currentPlayer;

        bool IsHeavyStone = targetStone.Type == StoneTypes.Heavy;
        if (IsHeavyStone)
        {
            Console.WriteLine($"Cannot erase stone at {row},{col}. Stone is heavy.");
            return false;
        }

        if (IsOpponentStone && !IsHeavyStone)
        {
            board[row - 1, col - 1] = null;
            return true;
        }
        else
        {
            Console.WriteLine($"Cannot erase stone at {row},{col}. Cell is either not valid or already empty.");
            return false;
        }


    }


    public bool CheckHorizontal(PlayerTypes player)
    {
        for (int row = 0; row < 10; row++)
        {
            int count = 0;
            for (int col = 0; col < 10; col++)
            {
                if (board[row, col] != null && board[row, col]!.Player == player)
                {
                    count++;
                    if (count == 5)
                    {
                        return true;
                    }


                }
                else
                {
                    count = 0;

                }

            }
        }
        return false;
    }

    public bool CheckVertical(PlayerTypes player)
    {
        for (int col = 0; col < 10; col++)
        {
            int count = 0;
            for (int row = 0; row < 10; row++)
            {
                if (board[row, col] != null && board[row, col]!.Player == player)
                {
                    count++;
                    if (count == 5)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }
        }
        return false;
    }
}