using System;
using ChessCore.PieceSetup;

namespace ChessCore.PieceSetup.Pieces;

public class Queen : APiece
{
    private static int BlackCount;
    private static int WhiteCount;
    public Pawn? PromotedFrom;

    public Queen(ColorEnum color, int x, int y) : base(color, x, y) 
    {
        Id = color == ColorEnum.White ? ++WhiteCount : ++BlackCount;
        Name = PieceEnum.Queen;
    }
    
    public Queen(int color, int x, int y) : this((ColorEnum)color, x, y) {}
    public Queen(ColorEnum color) : this(color, 0, 0) {}
    
    public Queen(int color) : this((ColorEnum)color, 0, 0) {}
    public override bool IsMove(int x, int y) 
    {
        if(x > 7 || x < 0 || y < 0 || y > 7 || (X == x && Y == y)) return false;

        int n1 = Math.Abs(x-X), n2 = Math.Abs(y-Y);

        return((X == x && Y != y) || (X != x && Y == y) || (n1 == n2)); 
        
    }
    public void Transform(Pawn p) 
    {
        PromotedFrom = p;
    }
}