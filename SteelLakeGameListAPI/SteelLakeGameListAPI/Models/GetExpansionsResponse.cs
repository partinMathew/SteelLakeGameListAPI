﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Models
{
    public class GetExpansionsResponse : HttpCollection<ExpansionSummaryItem>
    {
        public int TotalExpansions { get; set; }
        public bool HasExpansions { get; set; }
    }

    public class ExpansionSummaryItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? MinNumberOfPlayers { get; set; }
        public int? MaxNumberOfPlayers { get; set; }
        public int? RecommendedNumberOfPlayers { get; set; }
        public string GameLength { get; set; }
        public double Price { get; set; }
    }
}
