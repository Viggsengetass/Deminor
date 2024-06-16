using System;
using System.Collections.Generic;

namespace Cours.Services
{
    public static class MineService
    {
        public static List<List<char>> InitializeGridWithMines(int taille, int niveau)
        {
            List<List<char>> grid = new List<List<char>>();
            for (int i = 0; i < taille; i++)
            {
                grid.Add(new List<char>(new char[taille]));
                for (int j = 0; j < taille; j++)
                {
                    grid[i][j] = '-';  
                }
            }

            int numberOfMines = CalculateNumberOfMines(taille, niveau);
            PlaceMines(grid, taille, numberOfMines);

            return grid;
        }

        public static int CalculateNumberOfMines(int taille, int niveau)
        {
           
            return (taille * taille * niveau) / 10;  
        }

        private static void PlaceMines(List<List<char>> grid, int taille, int numberOfMines)
        {
            Random rand = new Random();
            while (numberOfMines > 0)
            {
                int row = rand.Next(taille);
                int col = rand.Next(taille);
                if (grid[row][col] != 'M')
                {
                    grid[row][col] = 'M';
                    numberOfMines--;
                }
            }
        }
    }
}