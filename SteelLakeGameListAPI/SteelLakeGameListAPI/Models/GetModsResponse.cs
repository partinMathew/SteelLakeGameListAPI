﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Models
{
    public class GetModsResponse : HttpCollection<ModSummaryItem>
    {
        public int TotalMods { get; set; }
        public bool HasMods { get; set; }
    }

    public class ModSummaryItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? MinNumberOfPlayers { get; set; }
        public int? MaxNumberOfPlayers { get; set; }
        public int? RecommendedNumberOfPlayers { get; set; }
        public string GameLength { get; set; }
    }
}
