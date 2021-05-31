using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShareRecipe.Services.Common.API.CQRS;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;

namespace ShareRecipe.Services.KweetService.API.Application.Commands
{
    public sealed class CreateKweetCommandHandler : IRequestHandler<CreateKweetCommand, CommandResponse>
    {
        private readonly IKweetRepository _kweetRepository;

        public CreateKweetCommandHandler(IKweetRepository kweetRepository)
        {
            _kweetRepository =
                kweetRepository ?? throw new ArgumentNullException(nameof(kweetRepository));
        }

        public async Task<CommandResponse> Handle(CreateKweetCommand request, CancellationToken cancellationToken)
        {
            ProfileAggregate aggregate = await _kweetRepository.GetAsync(request.UserId, cancellationToken);
            aggregate.CreateKweetAsync(request.Message);

            var success = await _kweetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            return new CommandResponse
            {
                Success = success
            };
        }
    }
}