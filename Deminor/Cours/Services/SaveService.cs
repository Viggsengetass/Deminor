using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Cours.Models;

namespace Cours.Services
{
    public static class SaveService
    {
        private static readonly string FilePath = "sauvegarde.json";

        public static void LoadSaveGame()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine("Aucune partie sauvegardée trouvée !");
                    MenuService.Menu();
                    return;
                }

                Console.WriteLine("Entrez le nom de la sauvegarde que vous souhaitez charger:");
                string saveName = Console.ReadLine();

                string jsonString = File.ReadAllText(FilePath);
                Console.WriteLine($"Contenu du fichier JSON : {jsonString}"); 

                List<GameSaveDataModel> saveList;
                try
                {
                    saveList = JsonSerializer.Deserialize<List<GameSaveDataModel>>(jsonString) ?? new List<GameSaveDataModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur de désérialisation : {ex.Message}");
                    saveList = new List<GameSaveDataModel>();
                }

                if (saveList == null || saveList.Count == 0)
                {
                    Console.WriteLine("Aucune partie sauvegardée trouvée !");
                    MenuService.Menu();
                    return;
                }

                var saveData = saveList.FirstOrDefault(s => s.Nom == saveName);

                if (saveData == null || saveData.Grid == null || saveData.Grid.Count == 0)
                {
                    Console.WriteLine("Les données de sauvegarde sont corrompues ou incomplètes.");
                    MenuService.Menu();
                    return;
                }

                int taille = saveData.Taille;
                List<List<char>> grid = saveData.Grid;
                int casesRestantes = saveData.CasesRestantes;
                DateTime startTime = saveData.StartTime;

                GridService.DisplayGrid(grid, taille);
                InputService.WaitInput(grid, taille, startTime, ref casesRestantes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement de la sauvegarde : {ex.Message}");
                MenuService.Menu();
            }
        }

        public static void SaveGame(List<List<char>> grid, int taille, int casesRestantes, DateTime startTime)
        {
            try
            {
                Console.WriteLine("Entrez le nom pour cette sauvegarde:");
                string saveName = Console.ReadLine();

                if (grid == null || grid.Count == 0 || grid.Any(g => g == null))
                {
                    Console.WriteLine("Erreur: La grille de jeu ne peut pas être vide ou contenir des lignes nulles.");
                    return;
                }

                GameSaveDataModel saveData = new GameSaveDataModel
                {
                    Nom = saveName,
                    Taille = taille,
                    Grid = grid,
                    CasesRestantes = casesRestantes,
                    StartTime = startTime
                };

                List<GameSaveDataModel> saveList;
                if (File.Exists(FilePath))
                {
                    string jsonString = File.ReadAllText(FilePath);
                    Console.WriteLine($"Lecture du fichier JSON : {jsonString}"); 
                    try
                    {
                        saveList = JsonSerializer.Deserialize<List<GameSaveDataModel>>(jsonString) ?? new List<GameSaveDataModel>();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur de désérialisation : {ex.Message}");
                        saveList = new List<GameSaveDataModel>();
                    }
                }
                else
                {
                    saveList = new List<GameSaveDataModel>();
                }

                var existingSave = saveList.FirstOrDefault(s => s.Nom == saveName);
                if (existingSave != null)
                {
                    saveList.Remove(existingSave);
                }

                saveList.Add(saveData);

                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                string updatedJsonString = JsonSerializer.Serialize(saveList, options);
                File.WriteAllText(FilePath, updatedJsonString);

                Console.WriteLine($"Écriture du fichier JSON : {updatedJsonString}"); 
                Console.WriteLine("Le jeu a été sauvegardé avec succès !");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la sauvegarde du jeu : {ex.Message}");
            }
        }
    }
}
