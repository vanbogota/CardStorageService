using CardStorageService.DAL;

namespace CardStorageService.Services
{
    public interface ICardRepositoryService : IRepository<Card, string>
    {
        public IList<Card> GetByClientId(int id);
    }
}
