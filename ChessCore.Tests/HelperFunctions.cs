using ChessCore.PieceSetup;
using ChessCore.PieceSetup.Pieces;

namespace ChessCore.Tests;

public static class HelperFunctions
{
    private static bool BoardEquals(APiece? p1, APiece? p2) => (
        (p1 is null && p2 is null) || 
        (p1 is not null && p2 is not null && p1.ToString() == p2.ToString())
    );
    public static bool CompareBoards(APiece?[,] board1, APiece?[,] board2)
    {
        
        for(int y = 0; y < 8; y++)
            for(int x = 0; x < 8; x++)
                if(!BoardEquals(board1[x,y], board2[x,y]))
                    return false;
        
        return true;
    }

    public static bool Comparebools(bool[,,] board1, bool[,,] board2)
    {
        for(int y = 0; y < 8; y++)
            for(int x = 0; x < 8; x++)
                for(int z = 0; z < 2; z++)
                    if(board1[z,y,x] != board2[z,y,x])
                        return false;
        return true;
    
    }

    public static string BoardDiffs(APiece?[,] board1, APiece?[,] board2)
    {
        string ret = "Board Diffs: \n";
        string append = "";
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (!BoardEquals(board1[y, x], board2[y, x]))
                {
                    ret += "[X] ";
                    append += $"{board1[y, x]} != {board2[y, x]}\n";
                }
                else
                {
                    ret += "[ ] ";
                }
            }
            ret += '\n';
        }
        ret += append;
        ret += '\n';

        return ret;
    }
    
    public static string BoolDiffs(bool[,,] board1, bool[,,] board2)
    {
        string ret = "Bool Diffs: \n";
        for (int z = 0; z < 2; z++)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    ret += $"[{(board1[z, y, x] == board2[z, y, x] ? ' ' : 'X')}] ";
                }
                ret += '\n';
            }
            ret += '\n';
        }
        ret += '\n';

        return ret;
    }

    public static string PrintList<K>(List<K> list)
    {
        string ret = "";
        foreach (var item in list)
        {
            ret += $"[{item}] ";
        }
        ret += '\n';
        
        return ret;
    }
}