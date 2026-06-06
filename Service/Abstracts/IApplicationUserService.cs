using Data.Entities.Identity;

namespace Service.Abstracts
{
    public interface IApplicationUserService
    {
        public Task<string> AddUserAsync(User user, string password);
    }
}
