using System;

namespace Cours.Services
{
    public static class MenuService
    {
        public static void Menu()
        {
            bool validInput = false;
            while (!validInput)
            {
                Console.Clear();
                Console.WriteLine("Bienvenue au démineur!");
                Console.WriteLine("1. Nouvelle partie");
                Console.WriteLine("2. Charger une partie sauvegardée");
                Console.WriteLine("3. Voir le leaderboard");
                Console.WriteLine("4. Quitter");
                Console.WriteLine("Entrez votre choix (1-4):");

                if (int.TryParse(Console.ReadLine(), out int choix) && choix >= 1 && choix <= 4)
                {
                    validInput = true;
                    switch (choix)
                    {
                        case 1:
                            StartNewGame();
                            break;
                        case 2:
                            SaveService.LoadSaveGame();
                            break;
                        case 3:
                            LeaderBoardService.LeaderBoardShow();
                            break;
                        case 4:
                            Console.WriteLine("Merci d'avoir joué. À bientôt!");
                            return;  // Quitter l'application
                    }
                }
                else
                {
                    Console.WriteLine("Choix invalide, veuillez réessayer.");
                }
            }
        }

        public static void StartNewGame()
        {
            string niveau = AskForDifficulty("Choisissez le niveau de difficulté (Facile, Moyen, Difficile):");
            int taille = CalculateGridSize(niveau);

            GameService.Start(taille, niveau);
        }

        private static string AskForDifficulty(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                string result = Console.ReadLine()?.ToLower();
                if (result == "facile" || result == "moyen" || result == "difficile")
                {
                    return result;
                }
                Console.WriteLine("Entrée invalide, veuillez entrer 'Facile', 'Moyen' ou 'Difficile'.");
            }
        }

        private static int CalculateGridSize(string niveau)
        {
            return niveau switch
            {
                "facile" => 10,
                "moyen" => 20,
                "difficile" => 26,
                _ => throw new ArgumentOutOfRangeException(nameof(niveau), "Niveau de difficulté invalide.")
            };
        }
    }
}
