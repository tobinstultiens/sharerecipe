using System;
using MediatR;
using ShareRecipe.Services.Common.API.CQRS;

namespace ShareRecipe.Services.KweetService.API.Application.Commands.CreateKweet
{
    public class CreateKweetCommand : IRequest<CommandResponse>
    {
        public Guid UserId { get; set; }
        public string Message { get; init; }
    }
}