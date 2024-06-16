using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Cours.Models;

namespace Cours.Services
{
    public static class LeaderBoardService
    {
        private static List<LeaderboardEntryModel> leaderboardList = new List<LeaderboardEntryModel>();

        static LeaderBoardService()
        {
            LoadLeaderboard();
        }

        public static void LeaderBoardAdd(int taille, DateTime startTime)
        {
            DateTime endTime = DateTime.Now;
            Console.WriteLine("Entrez votre nom pour le leaderboard : ");
            string? nom = Console.ReadLine();
            var leaderboardEntry = new LeaderboardEntryModel
            {
                Nom = nom,
                duree = (int)(endTime - startTime).TotalSeconds,
                dificulty = CalculateDifficulty(taille)
            };

            leaderboardList.Add(leaderboardEntry);
            SaveLeaderboard();  
            Console.WriteLine("Leaderboard mis à jour.");
            MenuService.Menu();
        }

        public static void LeaderBoardShow()
        {
            Console.WriteLine("Affichage du leaderboard...");

            var facile = new List<LeaderboardEntryModel>();
            var moyen = new List<LeaderboardEntryModel>();
            var difficile = new List<LeaderboardEntryModel>();

            foreach (var leader in leaderboardList)
            {
                switch (leader.dificulty)
                {
                    case 1:
                        facile.Add(leader);
                        break;
                    case 2:
                        moyen.Add(leader);
                        break;
                    case 3:
                        difficile.Add(leader);
                        break;
                    default:
                        break;
                }
            }

            facile.Sort((x, y) => x.duree.CompareTo(y.duree));
            moyen.Sort((x, y) => x.duree.CompareTo(y.duree));
            difficile.Sort((x, y) => x.duree.CompareTo(y.duree));

            Console.WriteLine("┌───────────────────────────┬───────────────────────────┬───────────────────────────┐");
            Console.WriteLine("│ Facile                    │ Moyen                     │ Difficile                 │");
            Console.WriteLine("├───────────────────────────┼───────────────────────────┼───────────────────────────┤");

            int maxRows = Math.Max(Math.Max(facile.Count, moyen.Count), difficile.Count);

            for (int i = 0; i < maxRows; i++)
            {
                string facileEntry = i < facile.Count ? $"Nom : {facile[i].Nom} | Durée : {facile[i].duree}" : "";
                string moyenEntry = i < moyen.Count ? $"Nom : {moyen[i].Nom} | Durée : {moyen[i].duree}" : "";
                string difficileEntry = i < difficile.Count ? $"Nom : {difficile[i].Nom} | Durée : {difficile[i].duree}" : "";
                Console.WriteLine($"│ {facileEntry,-25} │ {moyenEntry,-25} │ {difficileEntry,-25} │");
            }

            Console.WriteLine("└───────────────────────────┴───────────────────────────┴───────────────────────────┘");

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            MenuService.Menu();
        }

        private static void LoadLeaderboard()
        {
            string filePath = "leaderboard.json";
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                if (string.IsNullOrEmpty(jsonString))
                {
                    leaderboardList = new List<LeaderboardEntryModel>();
                }
                else
                {
                    leaderboardList = JsonSerializer.Deserialize<List<LeaderboardEntryModel>>(jsonString) ?? new List<LeaderboardEntryModel>();
                }
            }
            else
            {
                leaderboardList = new List<LeaderboardEntryModel>();
            }
        }

        private static void SaveLeaderboard()
        {
            string jsonString = JsonSerializer.Serialize(leaderboardList);
            File.WriteAllText("leaderboard.json", jsonString);
        }

        private static int CalculateDifficulty(int taille)
        {
            switch (taille)
            {
                case 10:
                    return 1;
                case 20:
                    return 2;
                case 26:
                    return 3;
                default:
                    return 0;
            }
        }
    }
}
