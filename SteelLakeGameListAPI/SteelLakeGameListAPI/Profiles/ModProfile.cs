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
            CreateMap<Mod, ModSummaryItem>();
            CreateMap<PostModRequest, Mod>();
            CreateMap<Mod, GetAModResponse>();
            CreateMap<UpdateModRequest, Mod>();
        }
    }
}
