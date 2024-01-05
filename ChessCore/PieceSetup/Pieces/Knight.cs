using ChessCore.PieceSetup;

namespace ChessCore.PieceSetup.Pieces;

public class Knight : APiece
{
    private static int BlackCount;
    private static int WhiteCount;
    public Pawn? PromotedFrom;

    public Knight(ColorEnum color, int x, int y) : base(color, x, y) 
    {
        Id = color == ColorEnum.White ? ++WhiteCount : ++BlackCount;
        Name = PieceEnum.Knight;
    }
    public Knight(ColorEnum color) : this(color, 0, 0) {}
    public override bool IsMove(int x, int y) 
    {
        if(x > 7 || x < 0 || y < 0 || y > 7) return false;

        return(
            (x == X+1 && (y == Y+2 || y == Y-2)) || 
            (x == X-1 && (y == Y+2 || y == Y-2)) ||
            (y == Y+1 && (x == X+2 || x == X-2)) || 
            (y == Y-1 && (x == X+2 || x == X-2))
        ); 
        
    }
    public void Transform(Pawn p) 
{
        PromotedFrom = p;
    }
}