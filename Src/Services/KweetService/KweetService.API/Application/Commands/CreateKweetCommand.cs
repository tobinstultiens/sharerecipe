using System;
using MediatR;
using ShareRecipe.Services.Common.API.CQRS;

namespace ShareRecipe.Services.KweetService.API.Application.Commands
{
    public class CreateKweetCommand : IRequest<CommandResponse>
    {
        public Guid UserId { get; init; }
        public string Message { get; init; }
    }
}