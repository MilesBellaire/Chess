using ChessCore.PieceSetup;

namespace ChessCore.PieceSetup.Pieces;

public class Rook : APiece
{
    private static int BlackCount;
    private static int WhiteCount;
    public Pawn? PromotedFrom;

    public Rook(ColorEnum color, int x, int y) : base(color, x, y) 
    {
        Id = color == ColorEnum.White ? ++WhiteCount : ++BlackCount;
        Name = PieceEnum.Rook;
    }
    
    public Rook(int color, int x, int y) : this((ColorEnum)color, x, y) {}
    
    public Rook(ColorEnum color) : this(color, 0, 0) {}
    
    public Rook(int color) : this((ColorEnum)color, 0, 0) {}
    public override bool IsMove(int x, int y) 
    {
        if(x > 7 || x < 0 || y < 0 || y > 7) return false;
        
        return (X == x && Y != y) || (X != x && Y == y);
        
    }
    public void Transform(Pawn p) 
{
        PromotedFrom = p;
    }
}