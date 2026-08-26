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
}