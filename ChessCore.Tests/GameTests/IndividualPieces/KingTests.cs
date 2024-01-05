using ChessCore.PieceSetup;
using ChessCore.PieceSetup.Pieces;

namespace ChessCore.Tests.GameTests.IndividualPieces;

public class KingTests
{
    [Test]
    public void King1()
    {
        // Assign
        APiece?[,] board = {
            { new King(ColorEnum.White), null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null }
        };
        
        // Act
        Game game = new(board);
        
        // Assert
        Assert.Pass("King was taken as input");
    }
    
    [Test]
    public void King2()
    {
        // Assign
        
        // Act
        
        // Assert
        Assert.Pass();
    }
}