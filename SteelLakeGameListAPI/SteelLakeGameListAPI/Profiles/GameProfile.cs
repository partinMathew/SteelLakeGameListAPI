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
            CreateMap<Game, GetAGameResponse>();
            CreateMap<Game, GameSummaryItem>();
            CreateMap<PostGameRequest, Game>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateGameRequest, Game>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}