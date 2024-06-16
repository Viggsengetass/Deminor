namespace Cours.Models
{
    public class GameSaveDataModel
    {
        public string Nom { get; set; }
        public int Taille { get; set; }
        public List<List<char>> Grid { get; set; }
        public int CasesRestantes { get; set; }
        public DateTime StartTime { get; set; }
    }
}