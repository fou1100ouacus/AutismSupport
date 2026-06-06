using Core.Bases;
using Data.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Features.Community.Posts.Commands.Models
{
    public class CreatePostCommand : IRequest<Response<string>>
    {
        public string? Content { get; set; }
        public IFormFile? Photo { get; set; }
    }

    public class DeletePostCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeletePostCommand(int id) => Id = id;
    }

    public class ModeratePostCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public PostStatus Status { get; set; }
        public string? Note { get; set; }
    }
}