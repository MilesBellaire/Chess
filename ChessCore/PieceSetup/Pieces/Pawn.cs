using ChessCore.DataStorage;
using ChessCore.PieceSetup;

namespace ChessCore.PieceSetup.Pieces;

public class Pawn : APiece
{
    private static int BlackCount;
    private static int WhiteCount;
    public APiece? Promoted;

    public Pawn(ColorEnum color, int x, int y) : base(color, x, y) 
    {
        Id = color == ColorEnum.White ? ++WhiteCount : ++BlackCount;
        Name = PieceEnum.Pawn;
    }
    
    public Pawn(int color, int x, int y) : this((ColorEnum)color, x, y) {}
    
    public Pawn(ColorEnum color) : this(color, 0, 0) {}
    
    public Pawn(int color) : this((ColorEnum)color, 0, 0) {}
    public override bool IsMove(int x, int y) 
    {
        int dir = Color == ColorEnum.Black ? 1 : -1;
        if(x > 7 || x < 0 || y < 0 || y > 7) return false;

        return (
                (x == this.X   && y == this.Y+2*dir && this.PosCount == 1) || 
                (x == this.X+1 && y == this.Y+dir)  || 
                (x == this.X-1 && y == this.Y+dir)  || 
                (x == this.X   && y == this.Y+dir)
               );

    }
    public void Promote(APiece p)
    {

        Promoted = p;
    }
}