using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Core.Bases;
using Core.Features.ApplicationUser.Queries.Models;
using Core.Features.ApplicationUser.Queries.Results;
using Core.Resources;
using Core.Wrappers;
using Data.Entities.Identity;
using Service.AuthServices.Interfaces;
using Service.Abstracts;

namespace Core.Features.ApplicationUser.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
         IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserPaginationReponse>>,
         IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>,
         IRequestHandler<GetMotherProfileQuery, Response<GetMotherProfileResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IChildService _childService;
        #endregion

        #region Constructors
        public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<User> userManager,
                                  ICurrentUserService currentUserService,
                                  IChildService childService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _userManager= userManager;
            _currentUserService = currentUserService;
            _childService = childService;
        }
        #endregion

        #region Handle Functions
        public async Task<PaginatedResult<GetUserPaginationReponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedList = await _mapper.ProjectTo<GetUserPaginationReponse>(users)
                                            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            //var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id==request.Id);
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user==null) return NotFound<GetUserByIdResponse>(_sharedResources[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetUserByIdResponse>(user);
            return Success(result);
        }

        public async Task<Response<GetMotherProfileResponse>> Handle(GetMotherProfileQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserId();
            var user = await _userManager.FindByIdAsync(currentUserId.ToString());
            
            if (user == null)
                return NotFound<GetMotherProfileResponse>(_sharedResources[SharedResourcesKeys.NotFound]);

            var response = new GetMotherProfileResponse
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Address = user.Address,
                Country = user.Country
            };

            // Check if user has child profile using service
            var childProfile = await _childService.GetProfileByMotherIdAsync(currentUserId);
            
            if (childProfile != null)
            {
                response.ChildNickname = childProfile.Nickname;
                response.Message = null;
            }
            else
            {
                response.ChildNickname = null;
                response.Message = "start create you child profile";
            }

            return Success(response);
        }
        #endregion
    }
}
