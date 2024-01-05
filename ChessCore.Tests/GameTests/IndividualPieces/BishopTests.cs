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
    public void TestPlacePiece()
    {
        // Assign
        Game game = new(BoardInputs["TestPlacePiece"]);
        
        // Act
        
        // Assert
        Assert.True(hf.CompareBoards(game.Board, BoardOutputs["TestPlacePiece"]), 
            hf.BoardDiffs(game.Board, BoardOutputs["TestPlacePiece"]));
        
        Assert.True(hf.Comparebools(game.GetAttackedSpaces(), AttackedSquaresOutputs["TestPlacePiece"]), 
            hf.BoolDiffs(game.GetAttackedSpaces(), AttackedSquaresOutputs["TestPlacePiece"]));
    }

    [Test]
    public void TestBlockVision()
    {
        // Assign
        Game game = new(BoardInputs["TestBlockVision"]);
        
        // Act
        
        // Assert
        Assert.True(hf.CompareBoards(game.Board, BoardOutputs["TestBlockVision"]), 
            hf.BoardDiffs(game.Board, BoardOutputs["TestBlockVision"]));
        
        Assert.True(hf.Comparebools(game.GetAttackedSpaces(), AttackedSquaresOutputs["TestBlockVision"]), 
            hf.BoolDiffs(game.GetAttackedSpaces(), AttackedSquaresOutputs["TestBlockVision"]));
    }

    [Test]
    public void TestAttackVision()
    {
        // Assign
        Game game = new(BoardInputs["TestAttackVision"]);
        
        // Act
        
        // Assert
        Assert.True(hf.CompareBoards(game.Board, BoardOutputs["TestAttackVision"]), 
            hf.BoardDiffs(game.Board, BoardOutputs["TestAttackVision"]));
        
        Assert.True(hf.Comparebools(game.GetAttackedSpaces(), AttackedSquaresOutputs["TestAttackVision"]), 
            hf.BoolDiffs(game.GetAttackedSpaces(), AttackedSquaresOutputs["TestAttackVision"]));
    }
    
    [Test]
    public void TestMoving()
    {
        // Assign
        Game game = new(BoardInputs["TestMoving"]);
        List<bool> movesHandledOutcome = [
            true, true, true, true, true, true, false, false, false, false, false, false
        ];
        
        // Act
        List<bool> movesHandled = new();
        movesHandled.Add(game.Go(4,3, 7,0)); // White moves to corner
        movesHandled.Add(game.Go(3,3, 0,0)); // Black moves to corner
        movesHandled.Add(game.Go(7,0, 0,7)); // White moves to other corner
        movesHandled.Add(game.Go(0,0, 7,7)); // Black moves to other corner
        movesHandled.Add(game.Go(0,7, 3,4)); // White moves to center
        movesHandled.Add(game.Go(7,7, 4,4)); // Black moves to center
        
        movesHandled.Add(game.Go(3,4, 3,3)); // White moves to invalid position
        movesHandled.Add(game.Go(4,4, 3,4)); // Black moves to invalid position
        movesHandled.Add(game.Go(3,4, 3,4)); // White moves to the same position
        movesHandled.Add(game.Go(4,4, 4,4)); // Black moves to the same position
        movesHandled.Add(game.Go(3,4, 8,0)); // White moves off the board
        movesHandled.Add(game.Go(4,4, 8,8)); // Black moves off the board
        
        // Assert
        Assert.AreEqual(movesHandledOutcome, movesHandled, 
            string.Join(", ", movesHandledOutcome.Zip(movesHandled, (a, b) => a == b ? "true" : "false")));
        Assert.True(hf.CompareBoards(game.Board, BoardOutputs["TestMoving"]), 
            hf.BoardDiffs(game.Board, BoardOutputs["TestMoving"]));
    }

    [Test]
    public void TestTakes()
    {
        // Assign
        Game game = new(BoardInputs["TestTakes"]);
        
        // Act
        List<bool> movesHandled = new();
        movesHandled.Add(game.Go(4,3, 6,1)); // White
        movesHandled.Add(game.Go(3,3, 5,1)); // Black
        movesHandled.Add(game.Go(6,1, 5,0)); // White
        movesHandled.Add(game.Go(5,1, 4,0)); // Black
        movesHandled.Add(game.Go(5,0, 4,1)); // White
        movesHandled.Add(game.Go(4,0, 7,3)); // Black
        movesHandled.Add(game.Go(4,1, 6,3)); // White
        movesHandled.Add(game.Go(7,3, 3,7)); // Black
        
        
        // Assert
        Assert.True(movesHandled.All(x => x), hf.PrintList(movesHandled));
        Assert.True(hf.CompareBoards(game.Board, BoardOutputs["TestTakes"]), hf.BoardDiffs(game.Board, BoardOutputs["TestTakes"]));
    }

    [SetUp]
    public void Setup()
    {
        BoardInputs = new();
        BoardOutputs = new();
        AttackedSquaresOutputs = new();

        // TestPlacePiece
        BoardInputs["TestPlacePiece"] = new APiece?[,] {
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, new Bishop(0), null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null }
        };
        BoardOutputs["TestPlacePiece"] = BoardInputs["TestPlacePiece"];
        AttackedSquaresOutputs["TestPlacePiece"] = new bool[,,] {
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

        // TestBlockVision
        BoardInputs["TestBlockVision"] = new APiece?[,] {
            { new Bishop(0), null, null, null,        null, null, null, null },
            { null,          null, null, null,        null, null, null, null },
            { null,          null, null, null,        null, null, null, null },
            { null,          null, null, new King(0), null, null, null, null },
            { null,          null, null, null,        null, null, null, null },
            { null,          null, null, null,        null, null, null, null },
            { null,          null, null, null,        null, null, null, null },
            { null,          null, null, null,        null, null, null, null }
        };
        BoardOutputs["TestBlockVision"] = BoardInputs["TestBlockVision"];
        AttackedSquaresOutputs["TestBlockVision"] = new bool[,,] {
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
        
        // TestAttackVision
        BoardInputs["TestAttackVision"] = new APiece?[,] {
            { new Bishop(0), null, null, null,                      null, null, null, null },
            { null,          null, null, null,                      null, null, null, null },
            { null,          null, null, null,                      null, null, null, null },
            { null,          null, null, new Rook(ColorEnum.White), null, null, null, null },
            { null,          null, null, null,                      null, null, null, null },
            { null,          null, null, null,                      null, null, null, null },
            { null,          null, null, null,                      null, null, null, null },
            { null,          null, null, null,                      null, null, null, null }
        };
        BoardOutputs["TestAttackVision"] = BoardInputs["TestAttackVision"];
        AttackedSquaresOutputs["TestAttackVision"] = new bool[,,] {
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
        
        // TestMoving
        BoardInputs["TestMoving"] = new APiece?[,] {
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, new Bishop(ColorEnum.Black), null, null, null, null },
            { null, null, null, new Bishop(ColorEnum.White), null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null }
        };
        BoardOutputs["TestMoving"] = new APiece?[,] {
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, new Bishop(ColorEnum.White, 3, 4), null, null, null },
            { null, null, null, null, new Bishop(0,4,4), null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null }
        };
        
        // TestTakes
        BoardInputs["TestTakes"] = new APiece?[,] {
            { null,        null,        null, null,          null, null, null, null        },
            { null,        null,        null, null,          null, null, null, null        },
            { null,        null,        null, null,          null, null, null, null        },
            { null,        null,        null, new Bishop(0), null, null, null, new Rook(1) },
            { new Rook(1), new Rook(0), null, new Bishop(1), null, null, null, null        },
            { new Rook(0), new Rook(1), null, null,          null, null, null, null        },
            { null,        new Rook(0), null, new Rook(0),   null, null, null, null        },
            { null,        null,        null, new Rook(1),   null, null, null, null        }
        };
        BoardOutputs["TestTakes"] = new APiece?[,] {
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, new Bishop(0, 3, 7) },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, new Bishop(1, 6, 3), null, null, null, null },
            { null, null, null, null, null, null, null, null }
        };
    }
}