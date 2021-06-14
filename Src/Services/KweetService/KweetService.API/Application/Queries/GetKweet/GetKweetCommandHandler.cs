using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShareRecipe.Services.KweetService.API.Application.Models;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;

namespace ShareRecipe.Services.KweetService.API.Application.Queries.GetKweet
{
    public sealed class GetKweetCommandHandler : IRequestHandler<GetKweetCommand, List<KweetModel>>
    {
        private readonly IKweetRepository _kweetRepository;

        public GetKweetCommandHandler(IKweetRepository kweetRepository)
        {
            _kweetRepository =
                kweetRepository ?? throw new ArgumentNullException(nameof(kweetRepository));
        }

        public async Task<List<KweetModel>> Handle(GetKweetCommand request, CancellationToken cancellationToken)
        {
            List<ProfileAggregate> aggregate =
                await _kweetRepository.GetAllAsync(request.size, request.page, cancellationToken);

            List<KweetModel> kweetModels = new List<KweetModel>();
            foreach (var profile in aggregate)
            {
                foreach (var kweet in profile.Kweets)
                {
                    kweetModels.Add(new KweetModel
                    {
                        Id = kweet.Id,
                        Image = profile.ProfilePictureUrl,
                        DisplayName = profile.DisplayName,
                        Kweet = kweet,
                        UserId = profile.Id
                    });
                }
            }

            return kweetModels;
        }
    }
}