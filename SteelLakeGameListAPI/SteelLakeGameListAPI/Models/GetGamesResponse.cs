using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Models
{
    public class GetGamesResponse : HttpCollection<GameSummaryItem>
    {
        public int TotalGames { get; set; }
    }
    public class GameSummaryItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int MinNumberOfPlayers { get; set; }
        public int? MaxNumberOfPlayers { get; set; }
        public int RecommendedNumberOfPlayers { get; set; }
        public string GameLength { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public int NumberOfMods { get; set; }
        public int NumberOfExpansions { get; set; }
    }
}
