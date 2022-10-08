using CardStorageService.DAL;

namespace CardStorageService.Services
{
    public interface IAccountRepositoryService : IRepository<Account,int>
    {
    }
}
