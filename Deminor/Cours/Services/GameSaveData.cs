using System;
using System.Collections.Generic;

namespace Cours.@class
{
    public class GameSaveData
    {
        public int Taille { get; set; }
        public List<List<char>> Grid { get; set; }
        public int CasesRestantes { get; set; }
        public DateTime StartTime { get; set; }
    }
}