using System;
using System.Collections.Generic;
using Cours.Services;

namespace Cours.Services
{
    public static class InputService
    {
        public static void WaitInput(List<List<char>> grid, int taille, DateTime startTime, ref int casesRestantes)
        {
            while (true)
            {
                Console.WriteLine("Choisissez une case (sous format A1) ou tapez 'SAVE' pour sauvegarder, 'WIN' pour déclarer victoire, 'MENU' pour retourner au menu principal, 'LOOSE' pour perdre:");
                string? choixCase = Console.ReadLine()?.ToUpper();

                if (string.IsNullOrWhiteSpace(choixCase))
                {
                    Console.WriteLine("Entrée invalide. Veuillez réessayer.");
                    continue;
                }

                switch (choixCase)
                {
                    case "SAVE":
                        SaveService.SaveGame(grid, taille, casesRestantes, startTime);
                        AskToReturnToMenu();
                        continue;
                    case "WIN":
                        HandleWin(taille, startTime);
                        return;
                    case "MENU":
                        MenuService.Menu();
                        return;
                    case "LOOSE":
                        HandleLoose(grid, taille);
                        return;
                    default:
                        if (ProcessInput(choixCase, grid, taille, startTime, ref casesRestantes))
                            return;
                        break;
                }
            }
        }

        private static bool ProcessInput(string choixCase, List<List<char>> grid, int taille, DateTime startTime, ref int casesRestantes)
        {
            if (choixCase.Length < 2 || choixCase.Length > 3)
            {
                Console.WriteLine("Format invalide. Veuillez réessayer.");
                return false;
            }

            int ligne = choixCase[0] - 'A';
            if (int.TryParse(choixCase.Substring(1), out int colonne) && IsValidPosition(ligne, colonne - 1, taille))
            {
                return HandleCellSelection(grid, ligne, colonne - 1, taille, startTime, ref casesRestantes);
            }
            else
            {
                Console.WriteLine("Coordonnées invalides !");
                return false;
            }
        }

        private static bool IsValidPosition(int ligne, int colonne, int taille)
        {
            return ligne >= 0 && ligne < taille && colonne >= 0 && colonne < taille;
        }

        private static bool HandleCellSelection(List<List<char>> grid, int ligne, int colonne, int taille, DateTime startTime, ref int casesRestantes)
        {
            Console.Clear();
            if (grid[ligne][colonne] == 'M')
            {
                RevealAllMines(grid, taille); 
                GridService.DisplayGrid(grid, taille, true); 
                Console.WriteLine("Vous avez perdu la partie car vous avez fait exploser une mine.");
                AskToReturnToMenu();
                return true;
            }
            else
            {
                if (grid[ligne][colonne] == '-')
                {
                    int minesAround = CountMinesAround(grid, ligne, colonne, taille);
                    grid[ligne][colonne] = char.Parse(minesAround.ToString()); 
                    casesRestantes--;
                }
                GridService.DisplayGrid(grid, taille);
                if (casesRestantes == 0)
                {
                    Console.WriteLine("Félicitations! Vous avez trouvé toutes les cases sans mines.");
                    HandleWin(taille, startTime);
                    return true;
                }
                return false;
            }
        }

        private static void RevealAllMines(List<List<char>> grid, int taille)
        {
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (grid[i][j] == 'M')
                    {
                        grid[i][j] = 'M'; 
                    }
                }
            }
        }

        private static int CountMinesAround(List<List<char>> grid, int ligne, int colonne, int taille)
        {
            int count = 0;
            for (int i = ligne - 1; i <= ligne + 1; i++)
            {
                for (int j = colonne - 1; j <= colonne + 1; j++)
                {
                    if (i >= 0 && i < taille && j >= 0 && j < taille && grid[i][j] == 'M')
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private static void HandleWin(int taille, DateTime startTime)
        {
            Console.WriteLine("Félicitations! Vous avez gagné!");
            LeaderBoardService.LeaderBoardAdd(taille, startTime);
            MenuService.Menu();
        }

        private static void HandleLoose(List<List<char>> grid, int taille)
        {
            Console.Clear();
            RevealAllMines(grid, taille); 
            GridService.DisplayGrid(grid, taille, true); 
            Console.WriteLine("Vous avez perdu la partie car vous avez fait exploser une mine.");
            AskToReturnToMenu();
        }

        private static void AskToReturnToMenu()
        {
            Console.WriteLine("Voulez-vous retourner au menu principal ? (O/N)");
            string? response = Console.ReadLine()?.ToUpper();
            if (response == "O")
            {
                MenuService.Menu();
            }
            else
            {
                Console.WriteLine("Merci d'avoir joué. À bientôt!");
                Environment.Exit(0); 
            }
        }
    }
}
