﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Models
{
    public class UpdateExpansionRequest
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public int MinNumberOfPlayers { get; set; }
        public int? MaxNumberOfPlayers { get; set; }
        [Required]
        public int RecommendedNumberOfPlayers { get; set; }
        [Required]
        [MaxLength(25)]
        public string GameLength { get; set; }
        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
