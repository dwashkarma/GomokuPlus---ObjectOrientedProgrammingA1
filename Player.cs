public class Player{


private PlayerTypes playerType;
private int heavyStonesCount;
private int eraserCount;

    public Player(PlayerTypes playerType){
        this.playerType=playerType;
        this.heavyStonesCount=2;
        this.eraserCount=2;
        
    }

    public int HeavyStonesCount{
        get{
            return heavyStonesCount;
        }

    } 

    public int EraserCount{
        get{
            return eraserCount;
        }
    }

    public PlayerTypes PlayerType{
        get{
            return playerType;
        }
    }

// it reduce the number of heavy stones / action but doesnot return anything
    public void useHeavyStone(){
        if (heavyStonesCount > 0){
            heavyStonesCount--;
        }
        else{
            Console.WriteLine($"No heavy stones left for {playerType}");
        }
    }
}