﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Domain
{
    public class Expansion
    {
        public Guid Id { get; set; }
        public Game Game { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? MinNumberOfPlayers { get; set; }
        public int? MaxNumberOfPlayers { get; set; }
        public int? RecommendedNumberOfPlayers { get; set; }
        public string GameLength { get; set; }
        public double Price { get; set; }
    }
}
