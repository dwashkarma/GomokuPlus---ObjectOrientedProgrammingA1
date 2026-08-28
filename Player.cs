public class Player
{


    private PlayerTypes playerType;
    private int heavyStonesCount;
    private int eraserCount;

    public Player(PlayerTypes playerType)
    {
        this.playerType = playerType;
        this.heavyStonesCount = 2;
        this.eraserCount = 2;

    }

    public int HeavyStonesCount
    {
        get
        {
            return heavyStonesCount;
        }

    }

    public int EraserCount
    {
        get
        {
            return eraserCount;
        }
    }

    public PlayerTypes PlayerType
    {
        get
        {
            return playerType;
        }
    }

    // it reduce the number of heavy stones / action but doesnot return anything
    public void UseHeavyStone()
    {
        if (heavyStonesCount > 0)
        {
            heavyStonesCount--;
        }
        else
        {
            Console.WriteLine($"No heavy stones left for {playerType}");
        }
    }

    // reduce the eraser count but does not return anything
    public void UseEraser()
    {
        if (eraserCount > 0)
        {
            eraserCount--;
        }
        else
        {
            Console.WriteLine($"No erasers left for {playerType}");
        }
    }

    public void SetHeavyStoneCount(int count)
    {
        heavyStonesCount = count;
    }
    public void SetEraserCount(int count)
    {
        eraserCount = count;
    }
}