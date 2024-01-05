using System;
using ChessCore.PieceSetup;

namespace ChessCore.PieceSetup.Pieces;

public class Bishop : APiece 
{
    private static int BlackCount;
    private static int WhiteCount;
    public Pawn? PromotedFrom;

    public Bishop(ColorEnum color, int x, int y) : base(color, x, y) 
    {
        Id = color == ColorEnum.White ? ++WhiteCount : ++BlackCount;
        Name = PieceEnum.Bishop;
    }
    public Bishop(int color, int x, int y) : this((ColorEnum)color, x, y) {}
    
    public Bishop(ColorEnum color) : this(color, 0, 0) {}
    
    public Bishop(int color) : this((ColorEnum)color, 0, 0) {}
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