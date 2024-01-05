using ChessCore.PieceSetup;

namespace ChessCore.DataStorage;

public class Move {
    public int Turn;
    public int X;
    public int Y;
    public APiece? Takes;

    public Move(int turn, int x, int y, APiece? takes=null) {
        Turn = turn;
        X = x;
        Y = y;
        Takes = takes;
    }
}