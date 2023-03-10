using CardStorageService.DAL;

namespace CardStorageService.Services
{
    public interface IClientRepositoryService : IRepository<Client, int>
    {
    }
}
