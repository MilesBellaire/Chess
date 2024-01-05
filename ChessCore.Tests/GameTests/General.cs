using ChessCore.PieceSetup;
using ChessCore.PieceSetup.Pieces;
using hf = ChessCore.Tests.HelperFunctions;

namespace ChessCore.Tests.GameTests;

public class General
{
    [Test]
    public void General1()
    {
        // Assign
        APiece?[,] board = {
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null }
        };

        Game game = new(board);
        
        // Act
        
        // Assert
        Assert.Pass();
        Assert.True(hf.CompareBoards(game.Board, board));
    }
}