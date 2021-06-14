using AutoMapper;
using ShareRecipe.Services.KweetService.API.Application.Models;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;

namespace ShareRecipe.Services.KweetService.API.Application.Mappings
{
    public class KweetsMapping : Profile
    {
        public KweetsMapping()
        {
            CreateMap<KweetModel, ProfileAggregate>();
            CreateMap<ProfileAggregate, KweetModel>();
        }
    }
}