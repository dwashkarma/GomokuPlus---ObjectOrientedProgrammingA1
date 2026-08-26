public enum StoneTypes{
    Ordinary,
    Heavy
}

public enum PlayerTypes{
    Player1,
    Player2
}

public class Stone{
    private StoneTypes type;
    private PlayerTypes player;

    public Stone (StoneTypes type, PlayerTypes player){
        this.type = type;
        this.player = player;
    }

    public PlayerTypes Player{
        get{
            return player;
        }
    } 
    public StoneTypes Type{
        get{
            return type;
        }
    }

    public char Symbol{
        get{
            if (type==StoneTypes.Ordinary){
               if(player==PlayerTypes.Player1){
                return 'X';
               }
               else{
                return 'O';
               }
            }
            else {
                if(player==PlayerTypes.Player1){
                    return '@';
                }
                else{
                    return '#';
                }
            }

        }
    }
}