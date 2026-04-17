using Infrustructure.InfrastructureBases;

namespace Infrustructure.Abstracts.Views
{
    public interface IViewRepository<T> : IGenericRepositoryAsync<T> where T : class
    {
    }
}
