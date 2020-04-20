﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Domain
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int MinNumberOfPlayers { get; set; }
        public int? MaxNumberOfPlayers { get; set; }
        public int RecommendedNumberOfPlayers { get; set; }
        public string GameLength { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        [ForeignKey("Expansions")]
        public virtual List<Expansion> Expansions { get; set; }
        [ForeignKey("Mods")]
        public virtual List<Mod> Mods { get; set; }
    }
}
