using Core.Bases;
using Core.Features.Community.Posts.Commands.Models;
using MediatR;
using Service.Abstracts;
using Service.AuthServices.Interfaces;

public class PostCommandHandler :
        IRequestHandler<CreatePostCommand, Response<string>>,
        IRequestHandler<DeletePostCommand, Response<string>>,
        IRequestHandler<ModeratePostCommand, Response<string>>
{
    private readonly ICommunityPostService _postService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ResponseHandler _responseHandler;
    private readonly IFileService _fileService;

    public PostCommandHandler(
        ICommunityPostService postService,
        ICurrentUserService currentUserService,
        ResponseHandler responseHandler,
        IFileService fileService)
    {
        _postService = postService;
        _currentUserService = currentUserService;
        _responseHandler = responseHandler;
        _fileService = fileService;
    }

    public async Task<Response<string>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = _currentUserService.GetUserId();

            // رفع الصورة لو موجودة
            string? photoUrl = null;
            if (request.Photo != null && request.Photo.Length > 0)
            {
                photoUrl = await _fileService.UploadImage("Posts", request.Photo);
                if (photoUrl == "FailedToUploadImage" || photoUrl == "NoImage")
                    return _responseHandler.BadRequest<string>("فشل رفع الصورة");
            }

            await _postService.CreateAsync(request.Content, photoUrl, userId);
            return _responseHandler.Created<string>("تم إرسال البوست للمراجعة");
        }
        catch (InvalidOperationException ex)
        {
            return _responseHandler.BadRequest<string>(ex.Message);
        }
    }

    public async Task<Response<string>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = _currentUserService.GetUserId();
            await _postService.DeleteAsync(request.Id, userId);
            return _responseHandler.Deleted<string>("تم الحذف بنجاح");
        }
        catch (UnauthorizedAccessException)
        {
            return _responseHandler.Unauthorized<string>();
        }
    }

    public async Task<Response<string>> Handle(ModeratePostCommand request, CancellationToken cancellationToken)
    {
        var moderatorId = _currentUserService.GetUserId();
        await _postService.ModerateAsync(request.Id, request.Status, request.Note, moderatorId);
        return _responseHandler.Success<string>("تم الاعتماد بنجاح");
    }
}
