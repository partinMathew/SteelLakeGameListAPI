using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Models
{
    public class GetGamesResponse : HttpCollection<GameSummaryItem>
    {
        public int TotalGames { get; set; } // TODO: what do I need in this object
    }
    public class GameSummaryItem
    {
        public string Title { get; set; }
        public int MinNumberOfPlayers { get; set; }
        public int? MaxNumberOfPlayers { get; set; }
        public int RecommendedNumberOfPlayers { get; set; }
        public string GameLength { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
