using Chess.Game.DataStorage;

namespace Chess.Game.PieceSetup;

class APiece {
    public int Id;
    public ColorEnum Color;
    public PieceEnum Name;
    public bool Active;
    public int X;
    public int Y;
    public List<Move> Positions;
    public int PosCount;


    public APiece(ColorEnum color, int x, int y) {
        Positions = new List<Move>();
        Positions.Add(new Move(0, x, y));
        PosCount = 1;
        Color = color;
        Active = true;
        X = x;
        Y = y;
    }

    public void Taken() {

        Active = false;

    }

    public virtual bool IsMove(int x, int y) {

        return !(x > 7 || x < 0 || y < 0 || y > 7);
        
    }

    public virtual void Move(Move move) {
        Positions.Add(move);
        PosCount++;

        X = move.X;
        Y = move.Y;

    }

}