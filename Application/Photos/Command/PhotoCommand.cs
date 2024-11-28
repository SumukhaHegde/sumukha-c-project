using Core.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Photos.Command
{
    public record PhotoCommand(IFormFile file) : IRequest<Result<Photo>> { }


}
