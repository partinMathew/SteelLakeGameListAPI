using AutoMapper;
using SteelLakeGameListAPI.Domain;
using SteelLakeGameListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Profiles
{
    public class ExpansionProfile : Profile
    {
        public ExpansionProfile()
        {
            CreateMap<Expansion, GetExpansionsResponse>();
            CreateMap<Expansion, ExpansionSummaryItem>();
            CreateMap<PostExpansionRequest, Expansion>();
            CreateMap<Expansion, GetAnExpansionResponse>();
            CreateMap<UpdateExpansionRequest, Expansion>();
        }
    }
}
