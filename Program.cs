using Chess.Game.PieceSetup;
using Chess.Game;
using System;

// See https://aka.ms/new-console-template for more information


Game g = new Game();

bool[,,] a = g.GetAttackedSpaces();

for(int i = 0; i < a.GetLength(0); i++) {
    Console.WriteLine((ColorEnum) i);
    for(int y = 0; y < a.GetLength(1); y++) {
        for(int x = 0; x < a.GetLength(2); x++) {
            Console.Write($"[{ (a[i,x,y] ? "T" : " ") }]");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

g.Print();