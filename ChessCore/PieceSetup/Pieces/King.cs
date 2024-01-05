using ChessCore.PieceSetup;

namespace ChessCore.PieceSetup.Pieces;

public class King : APiece
{
    private static int BlackCount;
    private static int WhiteCount;

    public King(ColorEnum color, int x, int y) : base(color, x, y) 
    {
        Id = color == ColorEnum.White ? ++WhiteCount : ++BlackCount;
        Name = PieceEnum.King;
    }
    public King(int color, int x, int y) : this((ColorEnum)color, x, y) {}
    public King(ColorEnum color) : this(color, 0, 0) {}
    
    public King(int color) : this((ColorEnum)color, 0, 0) {}
    public override bool IsMove(int x, int y) 
    {
        if(x > 7 || x < 0 || y < 0 || y > 7) return false;

        if(PosCount == 1 && Math.Abs(x-X) == 2 && y == Y) return true;

        int n1 = Math.Abs(x-X), n2 = Math.Abs(y-Y);
        return(n1 + n2 == 1 || (n1 == 1 && n1 == n2)); 
        
    }
}