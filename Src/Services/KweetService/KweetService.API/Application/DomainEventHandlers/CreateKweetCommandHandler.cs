using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using MediatR;
using ShareRecipe.Services.Common.API.CQRS;
using ShareRecipe.Services.KweetService.API.Application.Commands;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;

namespace ShareRecipe.Services.KweetService.API.Application.DomainEventHandlers
{
    public sealed class CreateKweetCommandHandler : IRequestHandler<CreateKweetCommand, CommandResponse>
    {
        private readonly IKweetRepository _kweetRepository;
        private readonly IBus _bus;

        public CreateKweetCommandHandler(IKweetRepository kweetRepository, IBus bus)
        {
            _kweetRepository =
                kweetRepository ?? throw new ArgumentNullException(nameof(kweetRepository));
            _bus = bus;
        }

        public async Task<CommandResponse> Handle(CreateKweetCommand request, CancellationToken cancellationToken)
        {
            List<string> directions = null;
            List<Ingredient> ingredients = null;
            using (StringReader sr = new StringReader(request.Message)) {
                string line;
                while ((line = sr.ReadLine()) != null) {
                    if (line.StartsWith("-"))
                        ingredients.Add(new Ingredient(line, null));
                    if (Regex.IsMatch(line, @"^\d\."))
                        directions.Add(line);
                }
            }
            KweetAggregate kweetAggregate =
                new(request.UserId, request.Message, ingredients, directions);

            _kweetRepository.Add(kweetAggregate);

            bool success = await _kweetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            CommandResponse commandResponse = new()
            {
                Success = success
            };

            return commandResponse; 
        }
    }
}