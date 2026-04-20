using Data.Entities.Identity;
using Infrastructure.InfrastructureBases;

namespace Infrastructure.Abstracts
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {

    }
}
