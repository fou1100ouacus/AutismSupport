using Data.Entities.Identity;
using Infrustructure.InfrastructureBases;

namespace Infrustructure.Abstracts
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {

    }
}
