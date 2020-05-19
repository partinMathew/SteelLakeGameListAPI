using AutoMapper;
using SteelLakeGameListAPI.Domain;
using SteelLakeGameListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Profiles
{
    public class ModProfile : Profile
    {
        public ModProfile()
        {
            CreateMap<Mod, GetModsResponse>();
        }
    }
}
