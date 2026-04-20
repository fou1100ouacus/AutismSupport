// using Infrastructure.Abstracts;
// using Microsoft.AspNetCore.Http;
// using System.Security.Claims;

// namespace Infrastructure.Repositories
// {
//     public class CurrentUserService : ICurrentUserService
//     {
//         private readonly IHttpContextAccessor _httpContextAccessor;

//         public CurrentUserService(IHttpContextAccessor httpContextAccessor)
//         {
//             _httpContextAccessor = httpContextAccessor;
//         }

//         public int UserId
//         {
//             get
//             {
//                 var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//                 if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
//                 {
//                     throw new System.UnauthorizedAccessException("User ID not found or invalid in JWT token");
//                 }
//                 return userId;
//             }
//         }

//         public string UserName => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

//         public string Email => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

//         public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
//     }
// }