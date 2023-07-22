using Chess.Game.DataStorage;
using Chess.Game.PieceSetup;

namespace Chess.Game.PieceSetup.Pieces;

class Pawn : APiece
{
    private static int BlackCount;
    private static int WhiteCount;
    public APiece? Promoted;

    public Pawn(ColorEnum color, int x, int y) : base(color, x, y) 
    {
        Id = color == ColorEnum.White ? ++WhiteCount : ++BlackCount;
        Name = PieceEnum.Pawn;
    }
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