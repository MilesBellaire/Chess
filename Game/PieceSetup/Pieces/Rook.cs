using Chess.Game.PieceSetup;

namespace Chess.Game.PieceSetup.Pieces;

class Rook : APiece
{
    private static int BlackCount;
    private static int WhiteCount;
    public Pawn? PromotedFrom;

    public Rook(ColorEnum color, int x, int y) : base(color, x, y) 
    {
        Id = color == ColorEnum.White ? ++WhiteCount : ++BlackCount;
        Name = PieceEnum.Rook;
    }
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