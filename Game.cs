public class Game{

    private Board board;
    private Player player1;
    private Player player2;
    private Player currentPlayer;

    public Game(){
        this.board = new Board();
        this.player1 = new Player(PlayerTypes.Player1);
        this.player2 = new Player(PlayerTypes.Player2);
        this.currentPlayer = this.player1;
    }

    private void SwitchPlayer(){
        if (currentPlayer.PlayerType==PlayerTypes.Player1){
            currentPlayer=player2;
        }
        else{
            currentPlayer=player1;
        }
    }

    public void Start(){

        while(true){
        board.DisplayBoard();
        Console.WriteLine($"Current player: {currentPlayer.PlayerType}");
        Console.WriteLine("Enter row and column to place stone (e.g: O3:4):");
        string? input=Console.ReadLine();


        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid command.");
            continue;
        }


        char command=input[0];
        string coordinates=input.Substring(1);
        string[] parts=coordinates.Split(":");

        if (parts.Length != 2)
        {
            Console.WriteLine("Invalid command format.");
            continue;
        }

        
        int row=int.Parse(parts[0]);
        int col=int.Parse(parts[1]);






        
        }
    }

}