using System;
using System.Collections.Generic;

namespace Cours.Services
{
    public static class GameService
    {
        public static void Start(int taille, string niveau)
        {
            List<List<char>> grid = MineService.InitializeGridWithMines(taille, GetDifficultyLevel(niveau));
            DateTime startTime = DateTime.Now;
            int casesRestantes = taille * taille - MineService.CalculateNumberOfMines(taille, GetDifficultyLevel(niveau));

            GridService.DisplayGrid(grid, taille);
            InputService.WaitInput(grid, taille, startTime, ref casesRestantes);
        }

        private static int GetDifficultyLevel(string niveau)
        {
            return niveau switch
            {
                "facile" => 1,
                "moyen" => 2,
                "difficile" => 3,
                _ => throw new ArgumentOutOfRangeException(nameof(niveau), "Niveau de difficulté invalide.")
            };
        }
    }
}