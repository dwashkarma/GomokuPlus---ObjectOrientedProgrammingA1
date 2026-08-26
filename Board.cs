public class Board
{

    private Stone?[,] board;
    public Board(){
        board=new Stone?[10,10];
        
    }


    public void DisplayBoard(){
        Console.WriteLine("    1   2   3   4   5   6   7   8   9   10");
        Console.WriteLine("+---+---+---+---+---+---+---+---+---+---+---+");
        for(int i=1;i<=10;i++){
            Console.Write($"{i} ");
            for (int col=1;col<=10;col++){
                Console.Write("|   ");
                
                }
            Console.WriteLine("|");
            Console.WriteLine("+---+---+---+---+---+---+---+---+---+---+---+");
        }

    }
}