using ChessCore.PieceSetup;
using ChessCore.PieceSetup.Pieces;
using hf = ChessCore.Tests.HelperFunctions;

namespace ChessCore.Tests.GameTests.IndividualPieces;

public class BishopTests
{
    Dictionary<string, APiece?[,]> BoardInputs;
    Dictionary<string, APiece?[,]> BoardOutputs;
    Dictionary<string, bool[,,]> AttackedSquaresOutputs;
    [Test]
    public void Bishop1()
    {
        // Assign
        
        Game game = new(BoardInputs["Bishop1"]);
        // Act
        
        // Assert
        Assert.True(hf.CompareBoards(game.Board, BoardOutputs["Bishop1"]), 
            hf.BoardDiffs(game.Board, BoardOutputs["Bishop1"]));
        
        Assert.True(hf.Comparebools(game.GetAttackedSpaces(), AttackedSquaresOutputs["Bishop1"]), 
            hf.BoolDiffs(game.GetAttackedSpaces(), AttackedSquaresOutputs["Bishop1"]));
    }

    [Test]
    public void Bishop2()
    {
        // Assign
        
        Game game = new(BoardInputs["Bishop2"]);
        // Act
        
        // Assert
        Assert.True(hf.CompareBoards(game.Board, BoardOutputs["Bishop2"]), 
            hf.BoardDiffs(game.Board, BoardOutputs["Bishop2"]));
        
        Assert.True(hf.Comparebools(game.GetAttackedSpaces(), AttackedSquaresOutputs["Bishop2"]), 
            hf.BoolDiffs(game.GetAttackedSpaces(), AttackedSquaresOutputs["Bishop2"]));
    }

    [Test]
    public void Bishop3()
    {
        // Assign
        Game game = new(BoardInputs["Bishop3"]);
        
        // Act
        
        // Assert
        Assert.True(hf.CompareBoards(game.Board, BoardOutputs["Bishop3"]), 
            hf.BoardDiffs(game.Board, BoardOutputs["Bishop3"]));
        
        Assert.True(hf.Comparebools(game.GetAttackedSpaces(), AttackedSquaresOutputs["Bishop3"]), 
            hf.BoolDiffs(game.GetAttackedSpaces(), AttackedSquaresOutputs["Bishop3"]));
    }
    
    [Test]
    public void Bishop4()
    {
        // Assign
        Game game = new(BoardInputs["Bishop4"]);
        
        // Act
        List<bool> movesHandled = new();
        movesHandled.Add(game.Go(game.Board[3, 3], 0, 0));
        movesHandled.Add(game.Go(game.Board[0, 0], 7, 7));
        movesHandled.Add(game.Go(game.Board[7, 7], 4, 4));
        
        // Assert
        Assert.True(movesHandled.All(x => x), hf.PrintList(movesHandled));
        Assert.True(hf.CompareBoards(game.Board, BoardOutputs["Bishop4"]), 
            hf.BoardDiffs(game.Board, BoardOutputs["Bishop4"]));
    }

    [SetUp]
    public void Setup()
    {
        BoardInputs = new();
        BoardOutputs = new();
        AttackedSquaresOutputs = new();

        // Bishop1
        BoardInputs["Bishop1"] = new APiece?[,] {
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, new Bishop(0), null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null }
        };
        BoardOutputs["Bishop1"] = BoardInputs["Bishop1"];
        AttackedSquaresOutputs["Bishop1"] = new bool[,,] {
        {
            { true,  false, false, false, false, false, true,  false },
            { false, true,  false, false, false, true,  false, false },
            { false, false, true,  false, true,  false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, true,  false, true,  false, false, false },
            { false, true,  false, false, false, true,  false, false },
            { true,  false, false, false, false, false, true,  false },
            { false, false, false, false, false, false, false, true  }
        },
        {
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false }
        }
    };

        // Bishop2
        BoardInputs["Bishop2"] = new APiece?[,] {
            { new Bishop(0), null, null, null,        null, null, null, null },
            { null,          null, null, null,        null, null, null, null },
            { null,          null, null, null,        null, null, null, null },
            { null,          null, null, new King(0), null, null, null, null },
            { null,          null, null, null,        null, null, null, null },
            { null,          null, null, null,        null, null, null, null },
            { null,          null, null, null,        null, null, null, null },
            { null,          null, null, null,        null, null, null, null }
        };
        BoardOutputs["Bishop2"] = BoardInputs["Bishop2"];
        AttackedSquaresOutputs["Bishop2"] = new bool[,,] {
            {
                { false, false, false, false, false, false, false, false },
                { false, true,  false, false, false, false, false, false },
                { false, false, true,  true,  true,  false, false, false },
                { false, false, true,  false, true,  false, false, false },
                { false, false, true,  true,  true,  false, false, false },
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false }
            },
            {
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false }
            }
        };
        
        
        // Bishop2
        BoardInputs["Bishop3"] = new APiece?[,] {
            { new Bishop(0), null, null, null,                      null, null, null, null },
            { null,          null, null, null,                      null, null, null, null },
            { null,          null, null, null,                      null, null, null, null },
            { null,          null, null, new Rook(ColorEnum.White), null, null, null, null },
            { null,          null, null, null,                      null, null, null, null },
            { null,          null, null, null,                      null, null, null, null },
            { null,          null, null, null,                      null, null, null, null },
            { null,          null, null, null,                      null, null, null, null }
        };
        BoardOutputs["Bishop3"] = BoardInputs["Bishop3"];
        AttackedSquaresOutputs["Bishop3"] = new bool[,,] {
            {
                { false, false, false, false, false, false, false, false },
                { false, true, false,  false, false, false, false, false },
                { false, false, true,  false, false, false, false, false },
                { false, false, false, true,  false, false, false, false },
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false }
            },
            {
                { false, false, false, true,  false, false, false, false },
                { false, false, false, true,  false, false, false, false },
                { false, false, false, true,  false, false, false, false },
                { true,  true,  true,  false, true,  true,  true,  true  },
                { false, false, false, true,  false, false, false, false },
                { false, false, false, true,  false, false, false, false },
                { false, false, false, true,  false, false, false, false },
                { false, false, false, true,  false, false, false, false }
            }
        };
        
        
        // Bishop4
        BoardInputs["Bishop4"] = new APiece?[,] {
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, new Bishop(0), null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null }
        };
        BoardOutputs["Bishop4"] = new APiece?[,] {
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, new Bishop(0,4,4), null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null }
        };
    }
}