using AutoMapper;
using SteelLakeGameListAPI.Domain;
using SteelLakeGameListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GetAGameResponse>()
             .ForMember(dest => dest.NumberOfMods, opt => opt.MapFrom(src => src.Mods.Count))
            .ForMember(dest => dest.NumberOfExpansions, opt => opt.MapFrom(src => src.Expansions.Count));
            CreateMap<Game, GameSummaryItem>()
            .ForMember(dest => dest.NumberOfMods, opt => opt.MapFrom(src => src.Mods.Count))
            .ForMember(dest => dest.NumberOfExpansions, opt => opt.MapFrom(src => src.Expansions.Count));
            CreateMap<PostGameRequest, Game>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateGameRequest, Game>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}