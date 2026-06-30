using Core.Bases;
using Core.Features.ChildProfile.Commands;
using MediatR;
using Service.Abstracts;
using Service.AuthServices.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Features.ChildProfile.Commands.Handlers
{
    public class DeleteChildProfileCommandHandler : IRequestHandler<DeleteChildProfileCommand, Response<string>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IChildService _childService;

        public DeleteChildProfileCommandHandler(ICurrentUserService currentUserService, IChildService childService)
        {
            _currentUserService = currentUserService;
            _childService = childService;
        }

        public async Task<Response<string>> Handle(DeleteChildProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            
            // Check if user has an existing profile
            var existingProfile = await _childService.GetProfileByMotherIdAsync(userId);
            if (existingProfile == null)
            {
                return new Response<string>
                {
                    Succeeded = false,
                    Message = "No child profile found to delete."
                };
            }

            try
            {
                var result = await _childService.DeleteChildProfileAsync(existingProfile);
                
                return result switch
                {
                    "Success" => new Response<string>
                    {
                        Succeeded = true,
                        Message = "Child profile deleted successfully"
                    },
                    _ => new Response<string>
                    {
                        Succeeded = false,
                        Message = "Failed to delete child profile"
                    }
                };
            }
            catch (System.Exception ex)
            {
                return new Response<string>
                {
                    Succeeded = false,
                    Message = $"Failed to delete child profile: {ex.Message}"
                };
            }
        }
    }
}
