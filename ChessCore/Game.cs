using System;
using ChessCore.PieceSetup;
using ChessCore.PieceSetup.Pieces;
using ChessCore.DataStorage;
using System.Collections.Generic;

namespace ChessCore;

public class Game {
    public APiece?[,] Board;
    private List<APiece> Pieces;
    private ColorEnum Turn;
    private int Turns;
    
    
    public Game(APiece?[,] board)
    {
        Pieces = new List<APiece>();
        Turn = ColorEnum.White;
        Turns = 0;

        Board = board;

        // BaseBoard();
        // Initialize Pieces List
        foreach(APiece? p in Board)
            if(p != null)
                Pieces.Add(p);

        int currentId = 0;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (Board[x, y] != null)
                {
                    Board[x, y]!.X = x;
                    Board[x, y]!.Y = y;

                    currentId++;
                }
            }
        }

        GetAttackedSpaces();
    }
    public Game() : this(BaseBoard()) {}
    
    public bool Go(APiece? p, int x, int y) 
    {
        if(p is null || Pieces.Contains(p) || p.Color != Turn) return false;
        // Handle conditions before moving piece
        if(!CheckMove(p, x, y)) return false;
        
        if(CheckCastle(p,x,y)) {                                                     
            int dir = x-p.X > 0 ? 1 : -1, corner = dir > 0 ? 7 : 0;
            Rook r = (Rook)Board[corner,y]!;
            Move rookMove = new(Turns, x-dir, y);

            Board[x-dir, y] = r; Board[corner, y] = null; r.Move(rookMove);     // Moves Rook to new position
        }

        if(CheckEnPassant(p,x,y)) {                                                  
            Board[x,p.Y]!.Taken(); Board[x,p.Y] = null;                         // Removes Pawn from the board
        }

        if(CheckPromotion(p,x,y)) {
            p = Promote((Pawn)p);
        }

        Move m = new Move(Turns, x, y, Board[x,y]);
        Board[x,y]?.Taken(); Board[x,y] = p; Board[p.X,p.Y] = null; p.Move(m);  // Moves Piece to new Spot

        GetAttackedSpaces();
        Turns++;

        return true;
    }
    
    public bool[,,] GetAttackedSpaces() 
    {
        bool[,,] AttackedSpaces = new bool[Enum.GetNames(typeof(ColorEnum)).Length, 8, 8];

        foreach(ColorEnum color in (ColorEnum[])Enum.GetValues(typeof(ColorEnum))) {
            for(int i = 0; i < 8; i++)
            for(int j = 0; j < 8; j++)
                AttackedSpaces[(int)color, i, j] = false;

            foreach(APiece? p in Board) 
                if(p != null && p.Color == color)
                    for(int y = 0; y < 8; y++)
                    for(int x = 0; x < 8; x++) {
                        if(CheckMove(p, x, y, false))
                            AttackedSpaces[(int)color, x, y] = true;
                            
                    }
        }
        return AttackedSpaces;

    }
    
    public void Print() 
    {
        int columnWidth = 6;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                APiece? p = Board[x, y];
                string className = (p != null) ? p.Name.ToString() : "";

                // Calculate the padding required on both sides of the class name
                int leftPadding = (columnWidth - className.Length) / 2;
                int rightPadding = columnWidth - className.Length - leftPadding;

                // Pad the class name with spaces on both sides to center it
                string centeredClassName = "[" + className.PadLeft(leftPadding + className.Length).PadRight(columnWidth) + "]";

                Console.Write(centeredClassName + " ");
            }
            Console.WriteLine();
        }
    }
    
    public string ToString() 
    {
        string s = "";
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                APiece? p = Board[x, y];
                string className = (p != null) ? p.Name.ToString() : "";
                s += className + " | ";
            }
            s += "\n";
        }
        return s;
    }
    
    private bool CheckMove(APiece p, int x, int y, bool attack=false) 
    {
        // Helper functions
        bool CheckDiag() {
            int xDif = x-p.X, yDif = y-p.Y;
            int xDir = xDif > 0 ? 1 : -1, yDir = yDif > 0 ? 1 : -1;

            for(int i = 1; i < Math.Abs(xDif); i++)
                if(Board[p.X+i*xDir,p.Y+i*yDir] != null) return false;
            return true;
        }
        bool CheckStraight() {
            if(p.X != x) {
                int dif = x-p.X;
                int dir = dif > 0 ? 1 : -1;
                for(int i = 1; i < Math.Abs(dif); i++)
                    if(Board[p.X+i*dir,p.Y] != null) return false;
            } else {
                int dif = y-p.Y;
                int dir = dif > 0 ? 1 : -1;
                for(int i = 1; i < Math.Abs(dif); i++)
                    if(Board[p.X,p.Y+i*dir] != null) return false;
            }
            return true;
        }


        int dir;
        if(!p.IsMove(x,y)) return false;
        if(Board[x,y] != null && Board[x,y]?.Color == p.Color && !attack) return false;


        switch(p.Name) {
            // Enter all cases assuming...
            // - The move is a valid move if there are no other pieces on the board
            // - The space is either null or an enemy piece
            case PieceEnum.Pawn:
                dir = p.Color == ColorEnum.Black ? 1 : -1;                              // Determine which direction the pawn is moving
                if(x != p.X) {                                                          // If the pawn is moving diagnolly
                    
                    if(attack) return true;                                             // If attacking, it doesn't matter if there's a piece there or not

                    if(Board[x,y] != null)                                              // Checks if it is taking a piece
                        return true;        
        
                    if(CheckEnPassant(p,x,y)) 
                        return true;      
                                                                                        
                    return false;       
                }       
                if(attack) return false;                                                // Pawns cannot attack straight ahead
                if(Math.Abs(y-p.Y) == 2) return Board[p.X,p.Y+dir] == null;             // If is double move, checks if there is a piece in the way
                        
                return true;                                                            // Only other move is one move forward

            case PieceEnum.Knight:
                // Knight can jump over piece so it doesn't matter the positions of anything else
                return true;

            case PieceEnum.King:
                if(IsAttacked(p.Color,x,y)) 
                    return false;

                if(Math.Abs(x-p.X) == 2) 
                    return CheckCastle(p,x,y);
                
                return true;

            case PieceEnum.Rook:
            
                return CheckStraight();

            case PieceEnum.Bishop:

                return CheckDiag();
            
            case PieceEnum.Queen:

                if(x != p.X && y != p.Y) return CheckDiag();
                return CheckStraight();

        }

        return false;
    }
    
    private bool CheckEnPassant(APiece p, int x, int y) 
    {
        if(!(p is Pawn)) return false;
        if(!p.IsMove(x,y)) return false;
        if(Board[x,y] != null && Board[x,y]?.Color == p.Color) return false;

        return  x != p.X &&                                                     // Makes sure Pawn is attacking
                Board[x,p.Y] is Pawn &&                                         // Checks space directly behind it has a pawn
                Board[x,p.Y]?.PosCount == 2 &&                                  // Checks how many moves the pawn has made
                Board[x,p.Y]?.Positions[1].Turn == Turns-1;                     // Checks if the pawn moved the previous turn
        
    }
    
    private bool CheckCastle(APiece p, int x, int y) 
    {
        if((!(p is King))   || 
           (!p.IsMove(x,y)) || 
           (Board[x,y] != null && Board[x,y]?.Color == p.Color) || 
           (Math.Abs(p.X-x) != 2))
            return false;

        int dir = (x-p.X > 0) ? 1 : -1;                                                 // Determines what direction the castle is going
        int corner = (dir == -1) ? 0 : 7;                                               // Stores the corner it is going
        if(Board[corner,y] is Rook && Board[corner, y]?.PosCount == 1) {                // Checks if corner has a Rook and that it hasn't moved
            for(int i = 1; p.X+i*dir != corner; i++)                                    // Iterates through each space between the king and the rook
                if(Board[p.X+i*dir,p.Y] != null || IsAttacked(p.Color,p.X+i*dir,p.Y))   // If there is a piece between the King and the Rook, return false
                    return false;
            return true;
        }
        
        return false;
    }
    
    private bool CheckPromotion(APiece p, int x, int y) 
    {
        if(!CheckMove(p,x,y)) return false;
        return (p is Pawn && ((p.Color == ColorEnum.Black && y == 7) || (p.Color == ColorEnum.White && y == 0)));
    }
    
    private bool IsAttacked(ColorEnum friendlyColor, int x, int y) 
    {
        foreach(APiece attacker in Pieces) {                      // Loops through AttackedSpaces to make sure King is not moving into an attacked space
            if(attacker.Color == friendlyColor) continue;         // Doesn't need to check its own attackers
            if(CheckMove(attacker,x,y)) return true;             // If space is being attacked return false
        }
        return false;
    }
    
    private APiece Promote(Pawn p) 
    {       
        //
        // Implement choosing a piece when UI is built
        //
        APiece promotedPiece = new Queen(p.Color, p.X,p.Y);
        // Also need to figure out a way for promoted Piece hold old pawn
        p.Promote(promotedPiece);

        return promotedPiece;
    }
    
    public static APiece[,] BaseBoard()
    {
        APiece?[,] ret = new APiece[8, 8];
        // Setup Pawns
        for(int i = 0; i < 8; i++) {
            ret[i,1] = new Pawn(ColorEnum.Black, i, 1);
            ret[i,6] = new Pawn(ColorEnum.White, i, 6);
        }
        
        // Black Pieces
        ret[0,0] = new Rook(ColorEnum.Black, 0, 0);
        ret[1,0] = new Knight(ColorEnum.Black, 1, 0);
        ret[2,0] = new Bishop(ColorEnum.Black, 2, 0);
        ret[3,0] = new King(ColorEnum.Black, 3, 0);
        ret[4,0] = new Queen(ColorEnum.Black, 4, 0);
        ret[5,0] = new Bishop(ColorEnum.Black, 5, 0);
        ret[6,0] = new Knight(ColorEnum.Black, 6, 0);
        ret[7,0] = new Rook(ColorEnum.Black, 7, 0);
        
        // White Pieces
        ret[0,7] = new Rook(ColorEnum.White, 0, 7);
        ret[1,7] = new Knight(ColorEnum.White, 1, 7);
        ret[2,7] = new Bishop(ColorEnum.White, 2, 7);
        ret[3,7] = new Queen(ColorEnum.White, 3, 7);
        ret[4,7] = new King(ColorEnum.White, 4, 7);
        ret[5,7] = new Bishop(ColorEnum.White, 5, 7);
        ret[6,7] = new Knight(ColorEnum.White, 6, 7);
        ret[7,7] = new Rook(ColorEnum.White, 7, 7);

        return ret;
    }

}