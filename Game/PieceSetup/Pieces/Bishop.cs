using System;
using Chess.Game.PieceSetup;

namespace Chess.Game.PieceSetup.Pieces;

class Bishop : APiece 
{
    private static int BlackCount;
    private static int WhiteCount;
    public Pawn? PromotedFrom;

    public Bishop(ColorEnum color, int x, int y) : base(color, x, y) 
    {
        Id = color == ColorEnum.White ? ++WhiteCount : ++BlackCount;
        Name = PieceEnum.Bishop;
    }
    public override bool IsMove(int x, int y) 
    {
        if(x > 7 || x < 0 || y < 0 || y > 7 || (X == x && Y == y)) return false;
        
        return (Math.Abs(x-X) == Math.Abs(y-Y));
        
    }
    public void Transform(Pawn p) 
    {
        PromotedFrom = p;
    }
}