using System.Collections.Generic;
using MediatR;
using ShareRecipe.Services.KweetService.API.Application.Models;

namespace ShareRecipe.Services.KweetService.API.Application.Queries.GetKweet
{
    public class GetKweetCommand : IRequest<List<KweetModel>>
    {
        public int size { get; init; }
        public int page { get; init; }
    }
}