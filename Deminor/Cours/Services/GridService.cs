using System;
using System.Collections.Generic;

namespace Cours.Services
{
    public static class GridService
    {
        public static void DisplayGrid(List<List<char>> grid, int taille, bool revealMines = false)
        {
            Console.WriteLine("Grille de jeu:");
            Console.Write("  ");
            for (int i = 1; i <= taille; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            
            for (int i = 0; i < taille; i++)
            {
                Console.Write((char)('A' + i) + " ");
                for (int j = 0; j < taille; j++)
                {
                    if (revealMines && grid[i][j] == 'M')
                    {
                        Console.Write("M ");
                    }
                    else
                    {
                        Console.Write(grid[i][j] == 'M' ? '-' : grid[i][j]);
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}